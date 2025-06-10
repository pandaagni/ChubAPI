using ChubAPI.Data;
using ChubAPI.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace ChubAPI.Controllers
{
    [Authorize]
    [Route("api")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("chatrooms")]
        public async Task<ActionResult<Chatroom>> CreateChatroom()
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var json = System.Text.Json.JsonDocument.Parse(body);
            var name = json.RootElement.GetProperty("name").GetString();
            var user = json.RootElement.GetProperty("user").GetString();

            if (string.IsNullOrEmpty(user) || !Guid.TryParse(user, out var userId))
            {
                return BadRequest("Valid User ID is required");
            }
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Chatroom name cannot be empty");
            }
            var chatroom = new Chatroom
            {
                Name = name,
                CreatedAt = DateTime.UtcNow
            };
            _context.Chatroom.Add(chatroom);
            _context.ChatroomUsers.Add(new ChatroomUsers
            {
                UserId = userId,
                ChatroomId = chatroom.ChatroomId,
                JoinedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();
            return Ok(chatroom);
        }

        [HttpPost]
        [Route("chatrooms/{ChatroomId}/messages")]
        public async Task<ActionResult<Messages>> SendMessage(Guid ChatroomId)
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var json = System.Text.Json.JsonDocument.Parse(body);
            var message = json.RootElement.GetProperty("message").GetString();
            var user = json.RootElement.GetProperty("user").GetString();

            bool isValidChatroom = await _context.Chatroom.AnyAsync(c => c.ChatroomId == ChatroomId);

            if (!isValidChatroom)
            {
                return NotFound("Chatroom not found");
            }

            if (string.IsNullOrEmpty(user) || !Guid.TryParse(user, out var userId))
            {
                return BadRequest("Valid User ID is required");
            }

            if(string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be empty");
            }

            var messageEntity = new Messages
            {
                Message = message,
                UserId = userId,
                ChatroomId = ChatroomId,
                SentAt = DateTime.UtcNow
            };

            _context.Messages.Add(messageEntity);

            await _context.SaveChangesAsync();
            return Ok(messageEntity);
        }

        [HttpPost]
        [Route("/chatrooms/{ChatroomId}/users")]
        public async Task<ActionResult<String>> JoinChatroom(Guid ChatroomId)
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var json = System.Text.Json.JsonDocument.Parse(body);
            var user = json.RootElement.GetProperty("user").GetString();

            if (string.IsNullOrEmpty(user) || !Guid.TryParse(user, out var userId))
            {
                return BadRequest("Valid User ID is required");
            }

            _context.ChatroomUsers.Add(new ChatroomUsers
            {
                UserId = userId,
                JoinedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return Ok("Success");
        }

        [HttpGet]
        [Route("users/{userId}/chatrooms")]
        public async Task<ActionResult> GetUserChatrooms(Guid userId)
        {
            var chatrooms = await _context.ChatroomUsers
                .Join(_context.Chatroom,
                    cu => cu.ChatroomId,
                    c => c.ChatroomId,
                    (cu, c) => new { ChatroomUser = cu, Chatroom = c }) // Corrected anonymous type property names
                .Where(joined => joined.ChatroomUser.UserId == userId) // Fixed property access
                .Select(joined => joined.Chatroom)
                .ToListAsync();
            return Ok(chatrooms);
        }
    }
}
