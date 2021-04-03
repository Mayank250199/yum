using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YumApi.Data;
using YumApi.Models;

namespace YumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergiesController : ControllerBase
    {
        private readonly YumDbContext _context;

        public AllergiesController(YumDbContext context)
        {
            _context = context;
        }

        // GET: api/Allergies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Allergies>>> GetAllergies()
        {
            return await _context.Allergies.ToListAsync();
        }

        // GET: api/Allergies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Allergies>> GetAllergies(int id)
        {
            var allergies = await _context.Allergies.FindAsync(id);

            if (allergies == null)
            {
                return NotFound();
            }

            return allergies;
        }

        // PUT: api/Allergies/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAllergies(int id, Allergies allergies)
        {
            if (id != allergies.Id)
            {
                return BadRequest();
            }

            _context.Entry(allergies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllergiesExists(id))
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

        // POST: api/Allergies
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Allergies>> PostAllergies(Allergies allergies)
        {
            _context.Allergies.Add(allergies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAllergies", new { id = allergies.Id }, allergies);
        }

        // DELETE: api/Allergies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Allergies>> DeleteAllergies(int id)
        {
            var allergies = await _context.Allergies.FindAsync(id);
            if (allergies == null)
            {
                return NotFound();
            }

            _context.Allergies.Remove(allergies);
            await _context.SaveChangesAsync();

            return allergies;
        }

        private bool AllergiesExists(int id)
        {
            return _context.Allergies.Any(e => e.Id == id);
        }
    }
}
