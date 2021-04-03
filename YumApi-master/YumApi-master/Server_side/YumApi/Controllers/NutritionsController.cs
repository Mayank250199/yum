using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YumApi.Data;
using YumApi.Models;

namespace YumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionsController : ControllerBase
    {
        private readonly YumDbContext _context;

        public NutritionsController(YumDbContext context)
        {
            _context = context;
        }

        // GET: api/Nutritions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Nutrition>>> GetNutrition()
        {
            return await _context.Nutrition.ToListAsync();
        }

        // GET: api/Nutritions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Nutrition>> GetNutrition(int id)
        {
            var nutrition = await _context.Nutrition.FindAsync(id);

            if (nutrition == null)
            {
                return NotFound();
            }

            return nutrition;
        }

        // PUT: api/Nutritions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNutrition(int id, Nutrition nutrition)
        {
            if (id != nutrition.Id)
            {
                return BadRequest();
            }

            _context.Entry(nutrition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NutritionExists(id))
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

        // POST: api/Nutritions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Nutrition>> PostNutrition(Nutrition nutrition)
        {
            _context.Nutrition.Add(nutrition);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNutrition", new { id = nutrition.Id }, nutrition);
        }

        // DELETE: api/Nutritions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Nutrition>> DeleteNutrition(int id)
        {
            var nutrition = await _context.Nutrition.FindAsync(id);
            if (nutrition == null)
            {
                return NotFound();
            }

            _context.Nutrition.Remove(nutrition);
            await _context.SaveChangesAsync();

            return nutrition;
        }

        private bool NutritionExists(int id)
        {
            return _context.Nutrition.Any(e => e.Id == id);
        }
    }
}
