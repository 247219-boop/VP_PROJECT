namespace BlazorApp2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public string ImageUrl { get; set; } = "";
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = "";

        // NEW FIELDS
        public DateTime ExpiryDate { get; set; }
        public bool IsOnSale { get; set; }
        public int DiscountPercentage { get; set; }

        public bool IsSelected { get; set; }

        public decimal FinalPrice =>
            IsOnSale ? Price - (Price * DiscountPercentage / 100) : Price;
    }
}
