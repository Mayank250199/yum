using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YumApi.Models
{
    public class CartIngredient
    {
        //public CartIngredient()
        //{
        //    this.Ingredient = new HashSet<Ingredient>();
        //}

        public int Id { get; set; }
        public string IngredientName { get; set; }
        public double Price { get; set; }
        public float Quantity { get; set; }
        public int? RecipeId { get; set; }

    }
}
