using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CottageAPI.Models;
using CottageApp.Models;

namespace CottageAPI.Controllers
{
    [Route("api/Cottages")]
    [ApiController]
    public class CottagesController : ControllerBase
    {
        private readonly CottageContext _context;

        public CottagesController(CottageContext context)
        {
            _context = context;
        }

        // GET: api/Cottages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cottage>>> GetCottageItems()
        {
            return await _context.CottageItems.ToListAsync();
        }

        // GET: api/Cottages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cottage>> GetCottage(long id)
        {
            var cottage = await _context.CottageItems.FindAsync(id);

            if (cottage == null)
            {
                return NotFound();
            }

            return cottage;
        }

        // PUT: api/Cottages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCottage(long id, Cottage cottage)
        {
            if (id != cottage.Id)
            {
                return BadRequest();
            }

            _context.Entry(cottage).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CottageExists(id))
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

        // POST: api/Cottages
        [HttpPost]
        public async Task<ActionResult<Cottage>> PostCottage(Cottage cottage)
        {
            _context.CottageItems.Add(cottage);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetCottage", new { id = cottage.Id }, cottage);
            return CreatedAtAction(nameof(GetCottage), new { id = cottage.Id }, cottage);
        }

        // DELETE: api/Cottages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCottage(long id)
        {
            var cottage = await _context.CottageItems.FindAsync(id);
            if (cottage == null)
            {
                return NotFound();
            }

            _context.CottageItems.Remove(cottage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CottageExists(long id)
        {
            return _context.CottageItems.Any(e => e.Id == id);
        }
    }
}
