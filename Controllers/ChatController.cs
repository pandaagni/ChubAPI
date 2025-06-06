using ChubAPI.Data;
using ChubAPI.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [Route("chatrooms/{ChatroomId}/messages")]
        public async Task<ActionResult<string>> SendMessage(Guid ChatroomId)
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var json = System.Text.Json.JsonDocument.Parse(body);
            var message = json.RootElement.GetProperty("message").GetString();
            var user = json.RootElement.GetProperty("user").GetString();

            // Validate and convert user to Guid
            if (string.IsNullOrEmpty(user) || !Guid.TryParse(user, out var userId))
            {
                return BadRequest("Valid User ID is required");
            }

            if(string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be empty");
            }

            _context.Messages.Add(new Messages
            {
                Message = message,
                UserId = userId,
                SentAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            return Ok("Success");
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
    }
}
