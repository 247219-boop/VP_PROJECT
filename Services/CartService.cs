using BlazorApp2.Components.Pages;
using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public class CartService
    {
        private readonly List<CartItem> cart = new();

        public List<CartItem> GetCart() => cart;

        public void AddToCart(Product product)
        {
            var item = cart.FirstOrDefault(c => c.Product.Id == product.Id);

            if (item == null)
            {
                cart.Add(new CartItem { Product = product, Quantity = 1 });
            }
            else
            {
                item.Quantity++;
            }
        }

        public void Remove(Product product)
        {
            cart.RemoveAll(c => c.Product.Id == product.Id);
        }

        public decimal GrandTotal() =>
            cart.Sum(c => c.TotalPrice);
        

        public void ClearCart() => cart.Clear();
    }
}
