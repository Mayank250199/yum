using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace YumApi.Models
{

    public class UserProfile
    {

        public UserProfile()
        {
            this.Allergies = new HashSet<Allergies>();
            this.CartIngredient = new HashSet<CartIngredient>();
            this.Order = new HashSet<Order>();
            this.Review = new HashSet<Review>();
            this.Recipe = new HashSet<Recipe>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<CartIngredient> CartIngredient { get; set; }
        public ICollection<Allergies> Allergies { get; set; }
        public ICollection<Recipe> Recipe { get; set; }
        public User User { get; set; }

        public ICollection<Review> Review { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
