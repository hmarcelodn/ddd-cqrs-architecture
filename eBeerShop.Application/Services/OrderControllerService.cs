using eBeerShop.Application.ViewModels;
using eBeerShop.Domain.Beers;
using eBeerShop.Domain.Customers;
using eBeerShop.Domain.Orders;

namespace eBeerShop.Application.Services
{
    public class OrderControllerService : IOrderControllerService
    {
        private readonly IBeerRepository beerRepository;
        private readonly ICustomerRepository customerRepository;

        public OrderControllerService()
        { }

        public OrderControllerService(IBeerRepository beerRepo, ICustomerRepository customerRepo)
        {
            beerRepository = beerRepo;
            customerRepository = customerRepo;
        }

        public ShoppingCartViewModel CreateShoppingCartForCustomer(int customerId)
        {
            var beers = beerRepository.GetCatalog();
            var customer = customerRepository.RetrieveCustomer(customerId);

            if (customer is null) throw new CustomerNotFoundException();

            return new ShoppingCartViewModel(beers, customer);
        }

        public ShoppingCartViewModel AddBeersToShoppingCart(ShoppingCartViewModel shoppingCartViewModel, int liters, int beerId)
        {
            var beer = beerRepository.RetrieveBeer(beerId);
            shoppingCartViewModel.ShoppingCart.Items.Add(new ShoppingCartItem(liters, beer));

            return shoppingCartViewModel;
        }

        public ProcessOrderViewModel ProcessOrderFromShoppingCart(ShoppingCartViewModel shoppingCartViewModel)
        {
            return new ProcessOrderViewModel();
        }
    }
}
