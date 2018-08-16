using eBeerShop.Application.ViewModels;

namespace eBeerShop.Application.Services
{
    public interface IOrderControllerService
    {        
        ShoppingCartViewModel CreateShoppingCartForCustomer(int customerId);

        ShoppingCartViewModel AddBeersToShoppingCart(ShoppingCartViewModel cart, int liters, int beerId);
    }
}
