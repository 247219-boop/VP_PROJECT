using BlazorApp2.Models;
using System.Collections.Generic;

namespace BlazorApp2.Models
{
    public class RecipeResult
    {
        public string DishName { get; set; } = "";
        public string Procedure { get; set; } = "";
        public List<Product> Ingredients { get; set; } = new List<Product>();
    }
}
