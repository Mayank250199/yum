using System.Collections.Generic;

namespace YumApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        public ICollection<CartIngredient> CartItem { get; set; }

        public double TotalPrice { get; set; }

        public int UserProfileId { get; set; }
    }
}
