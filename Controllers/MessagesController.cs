﻿//using System;
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
//    public class MessagesController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;

//        public MessagesController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Messages
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<Messages>>> GetMessages()
//        {
//            return await _context.Messages.ToListAsync();
//        }

//        // GET: api/Messages/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<Messages>> GetMessages(Guid id)
//        {
//            var messages = await _context.Messages.FindAsync(id);

//            if (messages == null)
//            {
//                return NotFound();
//            }

//            return messages;
//        }

//        // PUT: api/Messages/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutMessages(Guid id, Messages messages)
//        {
//            if (id != messages.MessageId)
//            {
//                return BadRequest();
//            }

//            _context.Entry(messages).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!MessagesExists(id))
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

//        // POST: api/Messages
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        public async Task<ActionResult<Messages>> PostMessages(Messages messages)
//        {
//            _context.Messages.Add(messages);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetMessages", new { id = messages.MessageId }, messages);
//        }

//        // DELETE: api/Messages/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteMessages(Guid id)
//        {
//            var messages = await _context.Messages.FindAsync(id);
//            if (messages == null)
//            {
//                return NotFound();
//            }

//            _context.Messages.Remove(messages);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool MessagesExists(Guid id)
//        {
//            return _context.Messages.Any(e => e.MessageId == id);
//        }
//    }
//}
