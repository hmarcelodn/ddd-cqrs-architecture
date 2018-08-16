using eBeerShop.Domain.Beers;
using eBeerShop.Domain.Customers;
using eBeerShop.Domain.Orders;
using System.Collections.Generic;

namespace eBeerShop.Application.ViewModels
{
    public class ShoppingCartViewModel
    {
        protected ShoppingCartViewModel()
        { }

        public ShoppingCartViewModel(List<Beer> beers, Customer customer) 
            : this()
        {
            Beers = beers;
            ShoppingCart = ShoppingCart.CreateNewForCustomer(customer);
        }

        public List<Beer> Beers { get; private set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
