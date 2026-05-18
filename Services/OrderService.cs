using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public class OrderService
    {
        private readonly List<Order> _orders = new();

        public int PlaceOrder(Order order)
        {
            order.OrderId = _orders.Count + 1;
            _orders.Add(order);
            return order.OrderId;
        }

        public Order GetOrderById(int id) =>
            _orders.FirstOrDefault(o => o.OrderId == id);

        public List<Order> GetAllOrders() => _orders;
    }
}

