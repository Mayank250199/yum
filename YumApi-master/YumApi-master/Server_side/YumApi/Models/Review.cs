using System.ComponentModel.DataAnnotations.Schema;

namespace YumApi.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int? UserprofileId { get; set; }

        public string Comment { get; set; }

    }
}
