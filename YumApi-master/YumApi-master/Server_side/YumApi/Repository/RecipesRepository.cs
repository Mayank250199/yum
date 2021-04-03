using System.Collections.Generic;
using System.Linq;
using YumApi.Data;
using YumApi.Interfaces;
using YumApi.Models;

namespace YumApi.Repository
{
    public class RecipesRepository : IRecipeRepository
    {
        private readonly YumDbContext _yumDbContext;

        public RecipesRepository(YumDbContext yumDbContext)
        {
            _yumDbContext = yumDbContext;
        }

        public ICollection<Allergies> GetAllergiesByRecipeId(int recipeId)
        {
            return _yumDbContext.Allergies.Where(Allergie => Allergie.RecipeId == recipeId).ToList();
        }

        public ICollection<Recipe> GetRecipeByCategoryId(int categoryId)
        {
            return _yumDbContext.Recipe.Where(Recipe => Recipe.CategoryId == categoryId).ToList();
        }

        public ICollection<Recipe_Nutrition> GetNutritionsByRecipeId(int recipeId)
        {
            return _yumDbContext.Recipe_Nutrition.Where(Nutrition => Nutrition.RecipeId == recipeId).ToList();
        }

        public ICollection<Recipe_Direction> GetDirectionsByRecipeId(int recipeId)
        {
            return _yumDbContext.Recipe_Direction.Where(Direction => Direction.RecipeId == recipeId).ToList();
        }

        public ICollection<Review> GetReviewsByRecipeId(int recipeId)
        {
            return _yumDbContext.Review.Where(Reviews => Reviews.RecipeId == recipeId).ToList();
        }

        public ICollection<Ingredient> GetIngredientsByRecipeId(int recipeId)
        {
            return _yumDbContext.Ingredient.Where(Ingredient => Ingredient.RecipeId == recipeId).ToList();
        }
    }
}
