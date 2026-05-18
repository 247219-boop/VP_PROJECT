namespace BlazorApp2.Models
{
    public class CartItem
    {

        public Product Product { get; set; } = new Product();
        public int Quantity { get; set; } = 1;

        public decimal TotalPrice =>
            Product.Price * Quantity;
    }
}
