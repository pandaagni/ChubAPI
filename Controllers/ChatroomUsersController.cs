//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using ChubAPI.Data;
//using ChubAPI.Models.Entity;

//namespace ChubAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ChatroomUsersController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public ChatroomUsersController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/ChatroomUsers
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<ChatroomUsers>>> GetChatroomUsers()
//        {
//            return await _context.ChatroomUsers.ToListAsync();
//        }

//        // GET: api/ChatroomUsers/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<ChatroomUsers>> GetChatroomUsers(Guid id)
//        {
//            var chatroomUsers = await _context.ChatroomUsers.FindAsync(id);

//            if (chatroomUsers == null)
//            {
//                return NotFound();
//            }

//            return chatroomUsers;
//        }

//        // PUT: api/ChatroomUsers/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutChatroomUsers(Guid id, ChatroomUsers chatroomUsers)
//        {
//            if (id != chatroomUsers.ChatroomId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(chatroomUsers).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ChatroomUsersExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/ChatroomUsers
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<ChatroomUsers>> PostChatroomUsers(ChatroomUsers chatroomUsers)
//        {
//            _context.ChatroomUsers.Add(chatroomUsers);
//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateException)
//            {
//                if (ChatroomUsersExists(chatroomUsers.ChatroomId))
//                {
//                    return Conflict();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return CreatedAtAction("GetChatroomUsers", new { id = chatroomUsers.ChatroomId }, chatroomUsers);
//        }

//        // DELETE: api/ChatroomUsers/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteChatroomUsers(Guid id)
//        {
//            var chatroomUsers = await _context.ChatroomUsers.FindAsync(id);
//            if (chatroomUsers == null)
//            {
//                return NotFound();
//            }

//            _context.ChatroomUsers.Remove(chatroomUsers);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ChatroomUsersExists(Guid id)
//        {
//            return _context.ChatroomUsers.Any(e => e.ChatroomId == id);
//        }
//    }
//}
