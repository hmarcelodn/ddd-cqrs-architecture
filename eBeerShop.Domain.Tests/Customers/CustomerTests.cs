using eBeerShop.Domain.Customers;
using System;
using Xunit;

namespace eBeerShop.Domain.Tests.Customers
{
    public class CustomerTests
    {
        private const string FIRST_NAME = "beer";
        private const string LAST_NAME = "man";
        private const string EMAIL = "beerman@gmail.com";
        private const string ADDRESS_CITY = "";
        private const string ADDRESS_STREET = "";
        private const string ADDRESS_ZIP = "";
        private const string ADDRESS_COUNTRY = "";

        [Fact]
        public void Should_HaveDescription_When_CreatedNewInstance()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);

            // Act 
            var newCustomer = new Customer(newAddress, FIRST_NAME, LAST_NAME, EMAIL);

            // Assert
            Assert.NotNull(newCustomer.Address);
            Assert.NotEmpty(newCustomer.Email);
            Assert.NotEmpty(newCustomer.FirstName);
            Assert.NotEmpty(newCustomer.LastName);
            Assert.Equal(FIRST_NAME, newCustomer.FirstName);
            Assert.Equal(LAST_NAME, newCustomer.LastName);
            Assert.Equal(EMAIL, newCustomer.Email);
        }

        [Fact]
        public void Should_ThrowException_When_AddressIsNull()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Customer(null, FIRST_NAME, LAST_NAME, EMAIL);
            });
        }

        [Fact]
        public void Should_ThrowException_When_FistNameIsNullOrEmpty()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Customer(newAddress, string.Empty, LAST_NAME, EMAIL);
            });
        }

        [Fact]
        public void Should_ThrowException_When_LastNameIsNullOrEmpty()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Customer(newAddress, FIRST_NAME, string.Empty, EMAIL);
            });
        }

        [Fact]
        public void Should_ThrowException_When_EmailAddressIsNullOrEmpty()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Customer(newAddress, FIRST_NAME, LAST_NAME, string.Empty);
            });
        }

        [Fact]
        public void Should_ChangeEmailAddress_When_SetEmailAddress()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);
            var newCustomer = new Customer(newAddress, FIRST_NAME, LAST_NAME, EMAIL);
            var newEmailAddress = "beerman2@gmail.com";

            // Act
            newCustomer.SetEmailAddress(newEmailAddress);

            // Assert
            Assert.Equal(newEmailAddress, newCustomer.Email);
        }

        [Fact]
        public void Should_ChangeFirstName_When_SetFirstName()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);
            var newCustomer = new Customer(newAddress, FIRST_NAME, LAST_NAME, EMAIL);
            var newFirstName = "beer2";

            // Act
            newCustomer.SetFirstName(newFirstName);

            // Assert
            Assert.Equal(newFirstName, newCustomer.FirstName);
        }

        [Fact]
        public void Should_ChangeLastName_When_SetLastName()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);
            var newCustomer = new Customer(newAddress, FIRST_NAME, LAST_NAME, EMAIL);
            var newLastName = "man2";

            // Act
            newCustomer.SetFirstName(newLastName);

            // Assert
            Assert.Equal(newLastName, newCustomer.FirstName);
        }

        [Fact]
        public void Should_ThrowException_When_SetFirstNameIsEmpty()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);
            var newCustomer = new Customer(newAddress, FIRST_NAME, LAST_NAME, EMAIL);

            // Act
            Action act = () => newCustomer.SetFirstName(string.Empty);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void Should_ThrowException_When_SetLastNameIsEmpty()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);
            var newCustomer = new Customer(newAddress, FIRST_NAME, LAST_NAME, EMAIL);

            // Act
            Action act = () => newCustomer.SetLastName(string.Empty);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Fact]
        public void Should_ThrowException_When_SetEmailAddressIsEmpty()
        {
            // Arrange
            var newAddress = new Address(ADDRESS_CITY, ADDRESS_STREET, ADDRESS_ZIP, ADDRESS_COUNTRY);
            var newCustomer = new Customer(newAddress, FIRST_NAME, LAST_NAME, EMAIL);

            // Act
            Action act = () => newCustomer.SetEmailAddress(string.Empty);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}
