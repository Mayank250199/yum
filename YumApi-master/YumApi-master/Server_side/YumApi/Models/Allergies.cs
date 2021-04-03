namespace YumApi.Models
{
    public class Allergies
    {
        public int Id { get; set; }

        public string AllergyName { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

    }
}
