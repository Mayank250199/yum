using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YumApi.Data;
using YumApi.Interfaces;
using YumApi.Models;

namespace YumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {

        private readonly IRecipeRepository _recipeRepository;

        private readonly YumDbContext _context;

        public RecipesController(YumDbContext context, IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipe()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }
            var Recipes = new List<Recipe>();
            Recipes = await _context.Recipe.ToListAsync();
            foreach (var Recipe in Recipes)
            {
                Recipe.Allergies = _recipeRepository.GetAllergiesByRecipeId(Recipe.Id);
                Recipe.Ingredient = _recipeRepository.GetIngredientsByRecipeId(Recipe.Id);
                Recipe.Directions = _recipeRepository.GetDirectionsByRecipeId(Recipe.Id);
                Recipe.Nutritions = _recipeRepository.GetNutritionsByRecipeId(Recipe.Id);
                Recipe.Reviews = _recipeRepository.GetReviewsByRecipeId(Recipe.Id);
            }

            if (Recipes == null)
            {
                return NotFound(new ApiResponse(404, $"Product not found with id {Recipes}"));
            }

            return Ok(new ApiOkResponse(Recipes));
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            var recipe = await _context.Recipe.FindAsync(id);

            if (recipe == null)
            {
                return NotFound(new ApiResponse(404, $"Recipe not found with id {id}"));
            }

            recipe.Allergies = _recipeRepository.GetAllergiesByRecipeId(id);
            recipe.Ingredient = _recipeRepository.GetIngredientsByRecipeId(id);
            recipe.Directions = _recipeRepository.GetDirectionsByRecipeId(id);
            recipe.Nutritions = _recipeRepository.GetNutritionsByRecipeId(id);
            recipe.Reviews = _recipeRepository.GetReviewsByRecipeId(id);

            return Ok(new ApiOkResponse(recipe));
        }

        
        [HttpGet("category/{id}")]
        public async Task<ActionResult<Recipe>> GetRecipeCategory(int id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            var recipe = _recipeRepository.GetRecipeByCategoryId(id);

            if (recipe == null)
            {
                return NotFound(new ApiResponse(404, $"Recipe not found with id {id}"));
            }

            foreach (var Recipe in recipe)
            {
                Recipe.Allergies = _recipeRepository.GetAllergiesByRecipeId(Recipe.Id);
                Recipe.Ingredient = _recipeRepository.GetIngredientsByRecipeId(Recipe.Id);
                Recipe.Directions = _recipeRepository.GetDirectionsByRecipeId(Recipe.Id);
                Recipe.Nutritions = _recipeRepository.GetNutritionsByRecipeId(Recipe.Id);
                Recipe.Reviews = _recipeRepository.GetReviewsByRecipeId(Recipe.Id);
            }


            return Ok(new ApiOkResponse(recipe));
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<Recipe>> PutRecipe(int id, Recipe recipe)
        {

            var ExistingAllergies = _recipeRepository.GetAllergiesByRecipeId(id);

            foreach (var Allergy in ExistingAllergies)
            {
                if (!recipe.Allergies.Any(c => c.AllergyName == Allergy.AllergyName ))
                {
                    _context.Allergies.Remove(Allergy);
                   
                }

            }

            foreach (var Allergy in recipe.Allergies)
            {
                var existingAllergy = ExistingAllergies.FirstOrDefault(x => x.AllergyName == Allergy.AllergyName);

                if (existingAllergy != null)
                {
                    Console.WriteLine("Allergy exist");
                }
                else
                {
                    // Insert child
                    var Allergys = new Allergies
                    {
                        RecipeId = recipe.Id,
                        AllergyName = Allergy.AllergyName
                    };
                    _context.Allergies.Add(Allergys);
                    Console.WriteLine("New Allergy added");
                }
            }

            var ExistingNutrition = _recipeRepository.GetNutritionsByRecipeId(id);

            foreach (var Nutrition in ExistingNutrition)
            {
                if (!recipe.Nutritions.Any(c => c.Nutrition == Nutrition.Nutrition))
                {
                    _context.Recipe_Nutrition.Remove(Nutrition);

                }

            }

            foreach (var Nutritions in recipe.Nutritions)
            {
                var existingNutrition = ExistingNutrition.FirstOrDefault(x => x.Nutrition == Nutritions.Nutrition);

                if (existingNutrition != null)
                {
                    Console.WriteLine("Nutrition exist");
                }
                else
                {
                    // Insert child
                    var Nutrition = Nutritions;
                    _context.Recipe_Nutrition.Add(Nutrition);   
                }
            }




            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiBadRequestResponse(ModelState));
            }

            if (id != recipe.Id)
            {
                return NotFound(new ApiResponse(404, $"Recipe not found with id {id}"));
            }

            _context.Entry(recipe).State = EntityState.Modified;


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound(new ApiResponse(200, $"Recipe not found with id {id}"));
                }
                else
                {
                    throw;
                }
            }

            //var data = _context.Recipe.Where(r => r.Id == recipe.Id);
            
            return Ok(new ApiResponse(404, $"Recipe Sat {id}"));
        }

        // POST: api/Recipes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            _context.Recipe.Add(recipe);
            await _context.SaveChangesAsync();
            var Recipes = new Recipe();

            return Ok(new ApiOkResponse(new { id = recipe.Id, Recipes = recipe }));
            
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recipe>> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipe.FindAsync(id);
            if (recipe == null)
            {
                return NotFound(new ApiResponse(404, $"Recipe not found with id {id}"));
            }

            _context.Recipe.Remove(recipe);
            await _context.SaveChangesAsync();

            return Ok(new ApiOkResponse(recipe));
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipe.Any(e => e.Id == id);
        }
    }
}
