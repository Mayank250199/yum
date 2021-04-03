namespace YumApi.Models
{
    public class Recipe_Direction
    {
        public int Id { get; set; }

        public string Direction { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
