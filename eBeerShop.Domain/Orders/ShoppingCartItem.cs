using eBeerShop.Domain.Beers;

namespace eBeerShop.Domain.Orders
{
    public class ShoppingCartItem
    {
        protected ShoppingCartItem()
        { }

        public ShoppingCartItem(int liters, Beer beer) 
            : this()
        {
            Liters = liters;
            BeerSnapshot = beer.TakeSnapshop();
        }

        public int Liters { get; private set; }

        public BeerSnapshot BeerSnapshot { get; private set; }
    }
}
