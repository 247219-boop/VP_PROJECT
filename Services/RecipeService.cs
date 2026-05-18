using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public class RecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly ProductDbService _productService;
        private readonly string _apiKey;

        public RecipeService(
            HttpClient httpClient,
            ProductDbService productService,
            IConfiguration config)
        {
            _httpClient = httpClient;
            _productService = productService;
            _apiKey = config["Groq:ApiKey"];
        }

        public async Task<RecipeResult> GetRecipeAsync(string dishName)
        {
            if (string.IsNullOrWhiteSpace(dishName))
                throw new ArgumentException("Dish name cannot be empty");

            var requestBody = new
            {
               model = "llama-3.1-8b-instant",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = $"Provide a plain text cooking recipe for {dishName}. " +
                                  "Do not use markdown, bullets, stars, emojis, or special characters. " +
                                  "Write simple numbered steps. " +
                                  "At the end, list ingredients as a comma-separated list."
                    }
                },
                max_tokens = 1000
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.groq.com/openai/v1/chat/completions");

            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", _apiKey);
            request.Content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Groq API Error: {error}");
            }

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var rawText = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString() ?? "";

            var cleanedText = CleanAiText(rawText);

            var allProducts = _productService.GetAllProducts();
            var matchedIngredients = allProducts
                .Where(p => IsIngredientMatch(cleanedText, p.Name))
                .Distinct()
                .Take(12)
                .ToList();

            return new RecipeResult
            {
                DishName = dishName,
                Procedure = cleanedText,
                Ingredients = matchedIngredients
            };
        }

        private string CleanAiText(string text)
        {
            return text
                .Replace("*", "")
                .Replace("#", "")
                .Replace("•", "")
                .Replace("–", "-")
                .Trim();
        }

        private bool IsIngredientMatch(string recipeText, string productName)
        {
            var recipe = Normalize(recipeText);
            var product = Normalize(productName);
            return recipe.Contains(product);
        }

        private string Normalize(string input)
        {
            return input
                .ToLower()
                .Replace("kg", "")
                .Replace("g", "")
                .Replace("l", "")
                .Replace("ml", "")
                .Replace(" ", "")
                .Replace("-", "");
        }
    }
}