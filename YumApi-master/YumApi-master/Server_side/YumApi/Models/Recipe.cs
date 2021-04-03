using System.Collections.Generic;

namespace YumApi.Models
{
    public class Recipe
    {

        public Recipe()
        {
            this.Ingredient = new HashSet<Ingredient>();
            this.Allergies = new HashSet<Allergies>();
            this.Directions = new HashSet<Recipe_Direction>();
            this.Reviews = new HashSet<Review>();
            this.Nutritions = new HashSet<Recipe_Nutrition>();
        }

        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public string Title { get; set; }
        public ICollection<Ingredient> Ingredient { get; set; }
        public int TimeToCook { get; set; }
        public ICollection<Allergies> Allergies { get; set; }
        public ICollection<Recipe_Direction> Directions { get; set; }
        public ICollection<Recipe_Nutrition> Nutritions { get; set; }
        public ICollection<Review> Reviews { get; set; }

    }

}
