using System;
using Xunit;
using RestAprilEducation.Domain;

namespace RestAprilEducation.DomainUnitTest
{
    public class ProductTests
    {
        [Fact]
        public void SetPrice_NegativePrice_ThrowsException()
        {
            // Arrange
            var product = new Product();

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => product.SetPrice(-0.01m));
            Assert.Equal("Fiyat alanı sıfırdan küçük olamaz", ex.Message);
        }

        [Fact]
        public void SetPrice_ZeroOrPositive_SetsPrice()
        {
            // Arrange
            var product = new Product();

            // Act
            product.SetPrice(0m);

            // Assert
            Assert.Equal(0m, product.Price);

            // Act - positive
            product.SetPrice(199.99m);

            // Assert
            Assert.Equal(199.99m, product.Price);
        }
    }
}
