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
//    public class ChatroomsController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public ChatroomsController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Chatrooms
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Chatroom>>> GetChatroom()
//        {
//            return await _context.Chatroom.ToListAsync();
//        }

//        // GET: api/Chatrooms/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Chatroom>> GetChatroom(Guid id)
//        {
//            var chatroom = await _context.Chatroom.FindAsync(id);

//            if (chatroom == null)
//            {
//                return NotFound();
//            }

//            return chatroom;
//        }

//        // PUT: api/Chatrooms/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutChatroom(Guid id, Chatroom chatroom)
//        {
//            if (id != chatroom.ChatroomId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(chatroom).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ChatroomExists(id))
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

//        // POST: api/Chatrooms
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Chatroom>> PostChatroom(Chatroom chatroom)
//        {
//            _context.Chatroom.Add(chatroom);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetChatroom", new { id = chatroom.ChatroomId }, chatroom);
//        }

//        // DELETE: api/Chatrooms/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteChatroom(Guid id)
//        {
//            var chatroom = await _context.Chatroom.FindAsync(id);
//            if (chatroom == null)
//            {
//                return NotFound();
//            }

//            _context.Chatroom.Remove(chatroom);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool ChatroomExists(Guid id)
//        {
//            return _context.Chatroom.Any(e => e.ChatroomId == id);
//        }
//    }
//}
