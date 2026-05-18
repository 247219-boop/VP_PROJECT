using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public class ExpiryDiscountService
    {
        public void Apply(Product product)
        {
            var daysLeft = (product.ExpiryDate - DateTime.Today).Days;

            if (daysLeft <= 1)
                Set(product, 60);
            else if (daysLeft <= 3)
                Set(product, 40);
            else if (daysLeft <= 7)
                Set(product, 20);
        }

        private void Set(Product product, int discount)
        {
            product.IsOnSale = true;
            product.DiscountPercentage = discount;
        }
    }
}
