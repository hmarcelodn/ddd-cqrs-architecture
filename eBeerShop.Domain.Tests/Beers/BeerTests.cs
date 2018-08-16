using eBeerShop.Domain.Beers;
using System;
using Xunit;

namespace eBeerShop.Domain.Tests.Beers
{
    public class BeerTests
    {
        [Fact]
        public void Should_HaveDescription_When_CreatedNewInstance()
        {
            // Arrange
            Beer beer = new Beer(12, "Special Honey Beer", 400);

            // Act & Assert
            Assert.NotNull(beer.Description);
            Assert.NotEmpty(beer.Description);
        }

        [Fact]
        public void Should_ThrowAnError_When_DescriptionIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => 
            {
                new Beer(12, null, 400);
            });
        }

        [Fact]
        public void Should_ThrowAnError_When_DescriptionIsEmpty()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Beer(12, string.Empty, 400);
            });
        }

        [Fact]
        public void Should_ThrowAnError_When_UnitPriceIsZero()
        {
            Assert.Throws<Exception>(() =>
            {
                new Beer(0, "Special Honey Beer", 400);
            });
        }

        [Fact]
        public void Should_ThrowAnError_When_UnitPriceLessThanZero()
        {
            Assert.Throws<Exception>(() =>
            {
                new Beer(-10, "Special Honey Beer", 400);
            });
        }

        [Fact]
        public void Should_ThrowAnError_When_LitersAvailableIsZero()
        {
            Assert.Throws<Exception>(() =>
            {
                new Beer(10, "Special Honey Beer", 0);
            });
        }

        [Fact]
        public void Should_ThrowAnError_When_LitersAvailableIsLessThanZero()
        {
            Assert.Throws<Exception>(() =>
            {
                new Beer(10, "Special Honey Beer", -1);
            });
        }

        [Fact]
        public void Should_Add10Liters_When_ChargeTankIsCalled()
        {
            // Arrange
            Beer beer = new Beer(12, "Special Honey Beer", 400);

            // Act
            beer.ChargeTank(10);

            // Assert
            Assert.Equal(410, beer.LitersAvailable);
        }

        [Fact]
        public void Should_ThrowAnError_When_ChangeTankIsCalledWithNegative()
        {
            // Arrange
            Beer beer = new Beer(12, "Special Honey Beer", 400);

            // Act
            Action act = () => beer.ChargeTank(-1);

            // Assert
            Assert.Throws<LitersChargeNegativeException>(act);
        }

        [Fact]
        public void Should_Remove10Liters_When_UnChargeTankIsCalled()
        {
            // Arrange
            Beer beer = new Beer(12, "Special Honey Beer", 400);

            // Act
            beer.UnchargeTank(10);

            // Assert
            Assert.Equal(390, beer.LitersAvailable);
        }

        [Fact]
        public void Should_Throw_When_UnChargeTankExceededAvailableLiters()
        {
            // Arrange
            Beer beer = new Beer(12, "Special Honey Beer", 400);

            // Act
            Action act = () => beer.UnchargeTank(500);

            // Asset
            Assert.Throws<LitersToRemoveExcedeedException>(act);
        }

        [Fact]
        public void Should_Throw_When_UnChargeTankIsZero()
        {
            // Arrange
            Beer beer = new Beer(12, "Special Honey Beer", 400);

            // Act
            Action act = () => beer.UnchargeTank(0);

            // Assert
            Assert.Throws<LitersToRemoveIsZeroException>(act);
        }

        [Fact]
        public void Should_UpdateDescription_When_ChangeDescriptionIsCalled()
        {
            // Arrange
            Beer beer = new Beer(12, "Special Honey Beer", 400);
            string newDescription = "IPA Beer";
            
            // Act
            beer.UpdateDescription(newDescription);

            // Assert
            Assert.Equal(newDescription, beer.Description);
        }

        [Fact]
        public void Should_IncreseIn10LiterPrice_When_IncreaseLiterPriceIsCalled()
        {
            // Arrange
            Beer beer = new Beer(12, "Special Honey Beer", 400);

            // Act
            beer.IncreaseLiterPrice(10);

            // Assert
            Assert.Equal(22, beer.LiterPrice);
        }

        [Fact]
        public void Should_DecreseIn10LiterPrice_When_DecreaseLiterPriceIsCalled()
        {
            // Arrange
            Beer beer = new Beer(12, "Special Honey Beer", 400);

            // Act
            beer.DecreaseLiterPrice(10);

            // Assert
            Assert.Equal(2, beer.LiterPrice);
        }
    }
}
