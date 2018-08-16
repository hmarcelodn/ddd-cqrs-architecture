using System.Collections.Generic;
using eBeerShop.Domain.Customers;

namespace eBeerShop.Domain.Orders
{
    public class ShoppingCart
    {
        protected ShoppingCart()
        {
            Items = new List<ShoppingCartItem>();
        }

        public Customer Buyer { get; private set; }
        public List<ShoppingCartItem> Items { get; private set; }

        public static ShoppingCart CreateNewForCustomer(Customer c)
        {
            return new ShoppingCart { Buyer = c };
        }
    }
}
