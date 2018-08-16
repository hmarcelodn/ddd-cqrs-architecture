using eBeerShop.Domain.Beers;
using eBeerShop.Domain.Shared;

namespace eBeerShop.Domain.Orders
{
    public class OrderItem: 
        EntityBase
    {
        protected OrderItem()
        { }

        public OrderItem(int liters, Beer beer, Order order)
        {
            Liters = liters;
            Beer = null;
            Order = order;
        }

        public int Liters { get; private set; }

        public BeerSnapshot Beer { get; private set; }

        public Order Order { get; private set; }

        public decimal Total()
        {
            return (1 * Liters);
        }
    }
}
