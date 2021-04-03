using System.Collections.Generic;
using YumApi.Models;

namespace YumApi.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Allergies> GetAllergiesByRecipeId(int recipeId);

        ICollection<Recipe_Nutrition> GetNutritionsByRecipeId(int recipeId);

        ICollection<Ingredient> GetIngredientsByRecipeId(int recipeId);

        ICollection<Review> GetReviewsByRecipeId(int recipeId);

        ICollection<Recipe_Direction> GetDirectionsByRecipeId(int recipeId);

        ICollection<Recipe> GetRecipeByCategoryId(int categoryId);
    }
}
