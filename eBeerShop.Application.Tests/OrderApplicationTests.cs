using AutoFixture;
using eBeerShop.Application.Services;
using eBeerShop.Application.ViewModels;
using eBeerShop.Domain.Beers;
using eBeerShop.Domain.Customers;
using eBeerShop.Domain.Orders;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace eBeerShop.Application.Tests
{
    public class OrderApplicationTests
    {
        private readonly Mock<ICustomerRepository> customerRepository;
        private readonly Mock<IBeerRepository> beerRepository;
        private readonly Customer customer1;
        private readonly Customer customer2;
        private readonly List<Beer> listBeers = new List<Beer>()
        {
            new Beer(1, "Honey", 100),
            new Beer(2, "IPA", 200)
        };

        public OrderApplicationTests()
        {
            customerRepository = new Mock<ICustomerRepository>();
            beerRepository = new Mock<IBeerRepository>();

            Fixture fixture = new Fixture();
            customer1 = fixture.Create<Customer>();
            customer2 = fixture.Create<Customer>();
            customer1.Id = 1;
            customer2.Id = 2;
            listBeers[0].Id = 1;
            listBeers[1].Id = 2;

            List<Customer> customerList = new List<Customer> { customer1, customer2 };

            customerRepository
                .Setup(o => o.RetrieveCustomer(It.IsAny<int>()))
                .Returns((int id) => {
                    return customerList.Where(c => c.Id == id).FirstOrDefault();
                });

            beerRepository.Setup(o => o.GetCatalog()).Returns(listBeers);

            beerRepository.Setup(o => o.RetrieveBeer(It.IsAny<int>())).Returns((int id) =>
            {
                return listBeers.Where(beer => beer.Id == id).FirstOrDefault();
            });
        }

        [Fact]
        public void Should_CreateShoppingCart_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);

            // Assert
            Assert.NotNull(viewModel);
        }

        [Fact]
        public void Should_CreateShoppingCartWithItems_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);

            // Assert
            Assert.IsType<List<Beer>>(viewModel.Beers);
        }

        [Fact]
        public void Should_CallBeerRepository_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);

            // Assert
            beerRepository.Verify(o => o.GetCatalog(), Times.AtLeastOnce());
        }

        [Fact]
        public void Should_Count2Items_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);

            // Assert
            Assert.Equal(2, viewModel.Beers.Count);
        }

        [Fact]
        public void Should_ShoppingCartDomainModelNotNull_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);

            // Assert
            Assert.NotNull(viewModel.ShoppingCart);
        }

        [Fact]
        public void Should_ShoppingCartDomainModelIsTypeShoppingCart_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);

            // Assert
            Assert.IsType<ShoppingCart>(viewModel.ShoppingCart);
        }

        [Fact]
        public void Should_ShoppingCartDomainModelContainsBuyer_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);

            // Assert
            Assert.NotNull(viewModel.ShoppingCart.Buyer);
        }

        [Fact]
        public void Should_ShoppingCartDomainModelIsTypeCustomer_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);

            // Assert
            Assert.IsType<Customer>(viewModel.ShoppingCart.Buyer);
        }

        [Fact]
        public void Should_ShoppingCartDomainModelCallCustomerRepo_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
                );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);

            // Assert
            customerRepository.Verify(o => o.RetrieveCustomer(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public void Should_ShoppingCartDomainModelMatchCustomer_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);

            // Assert
            Assert.Equal(viewModel.ShoppingCart.Buyer.Email, customer1.Email);
            Assert.Equal(viewModel.ShoppingCart.Buyer.FirstName, customer1.FirstName);
            Assert.Equal(viewModel.ShoppingCart.Buyer.LastName, customer1.LastName);
        }

        [Fact]
        public void Should_ShoppingCartDomainModelMatchSpecificCustomer_When_CreateOrder()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            ShoppingCartViewModel viewModel = orderApplicationService.CreateShoppingCartForCustomer(customer2.Id);

            // Assert
            Assert.Equal(viewModel.ShoppingCart.Buyer.Email, customer2.Email);
            Assert.Equal(viewModel.ShoppingCart.Buyer.FirstName, customer2.FirstName);
            Assert.Equal(viewModel.ShoppingCart.Buyer.LastName, customer2.LastName);
        }

        [Fact]
        public void Should_ThrowException_When_RetrieveCustomerDoesNotExist()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );

            // Act
            Action act = () => orderApplicationService.CreateShoppingCartForCustomer(3);

            // Assert
            Assert.Throws<CustomerNotFoundException>(act);
        }

        [Fact]
        public void Should_NotNullShoppingCartViewModel_When_AddProductToCar()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );
            int litersToAdd = 10;

            // Act
            ShoppingCartViewModel inputViewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);
            ShoppingCartViewModel resultViewModel = orderApplicationService.AddBeersToShoppingCart(inputViewModel, litersToAdd, 1);

            // Assert
            Assert.NotNull(resultViewModel);
        }

        [Fact]
        public void Should_ReturnTypeShoppingCartViewModel_When_AddProductToCar()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );
            int litersToAdd = 10;

            // Act
            ShoppingCartViewModel inputViewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);
            ShoppingCartViewModel resultViewModel = orderApplicationService.AddBeersToShoppingCart(inputViewModel, litersToAdd, 1);

            // Assert
            Assert.IsType<ShoppingCartViewModel>(resultViewModel);
        }

        [Fact]
        public void Should_CountBeerItems_When_AddProductToCar()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );
            int litersToAdd = 10;

            // Act
            ShoppingCartViewModel inputViewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);
            ShoppingCartViewModel resultViewModel = orderApplicationService.AddBeersToShoppingCart(inputViewModel, litersToAdd, listBeers.FirstOrDefault().Id);

            // Assert
            Assert.Equal(listBeers.Count, resultViewModel.Beers.Count);
        }

        [Fact]
        public void Should_AddBeerToItems_When_AddProductToCar()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );
            int litersToAdd = 10;

            // Act
            ShoppingCartViewModel inputViewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);
            ShoppingCartViewModel resultViewModel = orderApplicationService.AddBeersToShoppingCart(inputViewModel, litersToAdd, listBeers.FirstOrDefault().Id);

            // Assert
            Assert.Equal(1, resultViewModel.ShoppingCart.Items.Count);
        }

        [Fact]
        public void Should_BeersIsTypeListBeers_When_AddProductToCar()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );
            int litersToAdd = 10;

            // Act
            ShoppingCartViewModel inputViewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);
            ShoppingCartViewModel resultViewModel = orderApplicationService.AddBeersToShoppingCart(inputViewModel, litersToAdd, listBeers.FirstOrDefault().Id);

            // Assert
            Assert.IsType<List<ShoppingCartItem>>(resultViewModel.ShoppingCart.Items);
        }

        [Fact]
        public void Should_MatchLiters_When_AddProductToCar()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );
            int litersToAdd = 10;

            Beer beerToAdd = listBeers.FirstOrDefault();
            ShoppingCartItem shoppingCartItem = new ShoppingCartItem(10, listBeers[0]);

            // Act
            ShoppingCartViewModel inputViewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);
            ShoppingCartViewModel resultViewModel = orderApplicationService.AddBeersToShoppingCart(inputViewModel, litersToAdd, beerToAdd.Id);

            // Assert
            Assert.Equal(litersToAdd, resultViewModel.ShoppingCart.Items[0].Liters);
        }

        [Fact]
        public void Should_BeerSnapshotIsNotNull_When_AddProductToCar()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );
            int litersToAdd = 10;

            Beer beerToAdd = listBeers.FirstOrDefault();
            ShoppingCartItem shoppingCartItem = new ShoppingCartItem(10, listBeers[0]);

            // Act
            ShoppingCartViewModel inputViewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);
            ShoppingCartViewModel resultViewModel = orderApplicationService.AddBeersToShoppingCart(inputViewModel, litersToAdd, beerToAdd.Id);

            // Assert
            Assert.NotNull(resultViewModel.ShoppingCart.Items[0].BeerSnapshot);
        }

        [Fact]
        public void Should_BeerSnapshotMatchBeerData_When_AddProductToCar()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );
            int litersToAdd = 10;

            Beer beerToAdd = listBeers.FirstOrDefault();
            ShoppingCartItem shoppingCartItem = new ShoppingCartItem(10, listBeers[0]);

            // Act
            ShoppingCartViewModel inputViewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);
            ShoppingCartViewModel resultViewModel = orderApplicationService.AddBeersToShoppingCart(inputViewModel, litersToAdd, beerToAdd.Id);

            // Assert
            Assert.Equal(beerToAdd.Id, resultViewModel.ShoppingCart.Items[0].BeerSnapshot.Id);
            Assert.Equal(beerToAdd.Description, resultViewModel.ShoppingCart.Items[0].BeerSnapshot.Description);
            Assert.Equal(beerToAdd.LiterPrice, resultViewModel.ShoppingCart.Items[0].BeerSnapshot.LiterPrice);
        }

        [Fact]
        public void Should_BeerRepositoryRetrieveBeerVerified_When_AddProductToCar()
        {
            // Arrange
            IOrderControllerService orderApplicationService = new OrderControllerService(
                beerRepository.Object,
                customerRepository.Object
            );
            int litersToAdd = 10;

            Beer beerToAdd = listBeers.FirstOrDefault();
            ShoppingCartItem shoppingCartItem = new ShoppingCartItem(10, listBeers[0]);

            // Act
            ShoppingCartViewModel inputViewModel = orderApplicationService.CreateShoppingCartForCustomer(customer1.Id);
            ShoppingCartViewModel resultViewModel = orderApplicationService.AddBeersToShoppingCart(inputViewModel, litersToAdd, beerToAdd.Id);

            // Assert
            beerRepository.Verify(o => o.RetrieveBeer(It.IsAny<int>()), Times.Once());
        }
    }
}
