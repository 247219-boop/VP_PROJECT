using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public class ProductService
    {
        private readonly List<Product> products = new()
        {
            // Kitchen
            new Product { Id=1, Name="Cooking Oil", Price=550, ImageUrl="/images/oil.jpg", CategoryId=1, CategoryName="Kitchen" },
            new Product { Id=2, Name="Rice 5kg", Price=1200, ImageUrl="/images/rice.jpg", CategoryId=1, CategoryName="Kitchen" },

            // Bathroom
            new Product { Id=3, Name="Soap", Price=150, ImageUrl="/images/soap.jpg", CategoryId=2, CategoryName="Bathroom" },
            new Product { Id=4, Name="Shampoo", Price=420, ImageUrl="/images/shampoo.jpg", CategoryId=2, CategoryName="Bathroom" },

            // Fruits
            new Product { Id=5, Name="Apples", Price=300, ImageUrl="/images/apple.jpg", CategoryId=3, CategoryName="Fruits" },
            new Product { Id=6, Name="Bananas", Price=180, ImageUrl="/images/banana.jpg", CategoryId=3, CategoryName="Fruits" },

            // Snacks
            new Product { Id=7, Name="Chips", Price=120, ImageUrl="/images/chips.jpg", CategoryId=4, CategoryName="Snacks" },
            new Product { Id=8, Name="Biscuits", Price=200, ImageUrl="/images/biscuit.jpg", CategoryId=4, CategoryName="Snacks" },
        };

        public List<Product> GetProducts() => products;

        public Product GetProductById(int id) =>
            products.FirstOrDefault(p => p.Id == id);

        public List<Product> GetProductsByCategory(int categoryId) =>
            products.Where(p => p.CategoryId == categoryId).ToList();
    }
}
