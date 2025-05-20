using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChubAPI.Data;
using ChubAPI.Models.Entity;

namespace ChubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageReadsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MessageReadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MessageReads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessageReads>>> GetMessageReads()
        {
            return await _context.MessageReads.ToListAsync();
        }

        // GET: api/MessageReads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageReads>> GetMessageReads(Guid id)
        {
            var messageReads = await _context.MessageReads.FindAsync(id);

            if (messageReads == null)
            {
                return NotFound();
            }

            return messageReads;
        }

        // PUT: api/MessageReads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessageReads(Guid id, MessageReads messageReads)
        {
            if (id != messageReads.MessageId)
            {
                return BadRequest();
            }

            _context.Entry(messageReads).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageReadsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MessageReads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessageReads>> PostMessageReads(MessageReads messageReads)
        {
            _context.MessageReads.Add(messageReads);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MessageReadsExists(messageReads.MessageId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMessageReads", new { id = messageReads.MessageId }, messageReads);
        }

        // DELETE: api/MessageReads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessageReads(Guid id)
        {
            var messageReads = await _context.MessageReads.FindAsync(id);
            if (messageReads == null)
            {
                return NotFound();
            }

            _context.MessageReads.Remove(messageReads);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageReadsExists(Guid id)
        {
            return _context.MessageReads.Any(e => e.MessageId == id);
        }
    }
}
