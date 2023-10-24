using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Models;

namespace UnitTests.Pages.Product.AddRating
{
    public class JsonFileProductServiceTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region AddRating
        [Test]
        public void AddRating_InValid_RatingInvalid_Should_ReturnFalse()
        {
            // Arrange

            // Act
            var resultHigh = TestHelper.ProductService.AddRating("mike-cloud", 6);
            var resultLow = TestHelper.ProductService.AddRating("mike-cloud", -1);

            // Assert
            Assert.AreEqual(false, resultHigh);
            Assert.AreEqual(false, resultLow);
        }
        [Test]
        public void AddRating_InValid_ProductNull_Should_ReturnFalse()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }
        [Test]
        public void AddRating_InValid_ProductEmpty_Should_ReturnFalse()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void AddRating_Invalid_ProductDoesNotExist_Should_ReturnFalase()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("no-such-product", 3);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }
        [Test]
        public void AddRating_Valid_PriorRatingsExist_Should_ReturnTrue()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("SVI-001", 3);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }
        [Test]
        public void AddRating_Valid_NoRatings_Should_ReturnTrue()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("BLW-051", 3);

            // Reset

            // Assert
            Assert.AreEqual(true, result);
        }


        #endregion AddRating

        #region UpdateData

        [Test]
        public void UpdateData_Invalid_NoProduct_Should_ReturnFalse()
        {
            // Arrange

            // Act
            var invalidProduct = new ProductModel
            {
                Id = "invalid product"
            };
            var result = TestHelper.ProductService.UpdateData(invalidProduct);

            // Reset

            // Assert
            Assert.AreEqual(false, result);

        }

        [Test]
        public void UpdateData_Valid_Default_Should_ReturnTrue()
        {
            // Arrange
            const string TestDescription = "test description";

            var testProduct = TestHelper.ProductService.GetProducts().First<ProductModel>();
            testProduct.Description = TestDescription;

            // Act
            var result = TestHelper.ProductService.UpdateData(testProduct);
            var updatedProduct = TestHelper.ProductService.GetProducts().First<ProductModel>();

            // Reset

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(TestDescription, updatedProduct.Description);

        }

        #endregion UpdateData

    }
}