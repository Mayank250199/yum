namespace YumApi.Models
{
    public class Recipe_Nutrition
    {
        public int Id { get; set; }
        public int NutritionId { get; set; }
        public Nutrition Nutrition { get; set; }
        public float Percentage { get; set; }
        public int RecipeId { get; set; }
    }
}
