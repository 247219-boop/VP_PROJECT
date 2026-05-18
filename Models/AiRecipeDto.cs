//This represents exactly what AI will return
namespace BlazorApp2.Models
{
    public class AiRecipeDto
    {
        public List<string> Ingredients { get; set; } = new();
        public string Procedure { get; set; } = "";
    }
}
