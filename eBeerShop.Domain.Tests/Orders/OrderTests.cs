using AutoFixture;
using eBeerShop.Domain.Customers;
using eBeerShop.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace eBeerShop.Domain.Tests.Orders
{
    public class OrderTests
    {
        [Fact]
        public void Should_MatchCustomerEmail_When_OrderCreated()
        {
            // Arrange
            var fixture = new Fixture();
            var customer = fixture.Create<Customer>();

            // Act
            Order o = new Order(customer);

            // Assert
            Assert.Equal(customer.Email, o.Buyer.Email);
        }

        [Fact]
        public void Should_MatchCustomerFirstName_When_OrderCreated()
        {
            // Arrange
            var fixture = new Fixture();
            var customer = fixture.Create<Customer>();

            // Act
            Order o = new Order(customer);

            // Assert
            Assert.Equal(customer.FirstName, o.Buyer.FirstName);
        }

        [Fact]
        public void Should_MatchCustomerLastName_When_OrderCreated()
        {
            // Arrange
            var fixture = new Fixture();
            var customer = fixture.Create<Customer>();

            // Act
            Order o = new Order(customer);

            // Assert
            Assert.Equal(customer.LastName, o.Buyer.LastName);
        }

        [Fact]
        public void Should_MatchCustomerId_When_OrderCreated()
        {
            // Arrange
            var fixture = new Fixture();
            var customer = fixture.Create<Customer>();

            // Act
            Order o = new Order(customer);

            // Assert
            Assert.Equal(customer.Id, o.Buyer.Id);
        }
    }
}
