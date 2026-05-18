namespace BlazorApp2.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string Contact { get; set; } = "";

        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int OrderStatus { get; set; }
        public string PaymentMethod { get; set; } = "CashOnDelivery";

    }
}
