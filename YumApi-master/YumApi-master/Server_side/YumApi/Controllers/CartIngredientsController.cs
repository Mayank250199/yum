using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YumApi.Data;
using YumApi.Models;

namespace YumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartIngredientsController : ControllerBase
    {
        private readonly YumDbContext _context;

        public CartIngredientsController(YumDbContext context)
        {
            _context = context;
        }

        // GET: api/CartIngredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartIngredient>>> GetCartIngredient()
        {
            return await _context.CartIngredient.ToListAsync();
        }

        // GET: api/CartIngredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartIngredient>> GetCartIngredient(int id)
        {
            var cartIngredient = await _context.CartIngredient.FindAsync(id);

            if (cartIngredient == null)
            {
                return NotFound();
            }

            return cartIngredient;
        }

        // PUT: api/CartIngredients/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartIngredient(int id, CartIngredient cartIngredient)
        {
            if (id != cartIngredient.Id)
            {
                return BadRequest();
            }

            _context.Entry(cartIngredient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartIngredientExists(id))
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

        // POST: api/CartIngredients
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CartIngredient>> PostCartIngredient(CartIngredient cartIngredient)
        {
            _context.CartIngredient.Add(cartIngredient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCartIngredient", new { id = cartIngredient.Id }, cartIngredient);
        }

        // DELETE: api/CartIngredients/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CartIngredient>> DeleteCartIngredient(int id)
        {
            var cartIngredient = await _context.CartIngredient.FindAsync(id);
            if (cartIngredient == null)
            {
                return NotFound();
            }

            _context.CartIngredient.Remove(cartIngredient);
            await _context.SaveChangesAsync();

            return cartIngredient;
        }

        private bool CartIngredientExists(int id)
        {
            return _context.CartIngredient.Any(e => e.Id == id);
        }
    }
}
