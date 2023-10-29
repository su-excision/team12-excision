using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Models;

namespace UnitTests.Services.JsonFileProductService
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

        /// <summary>
        /// Tests to verify that an attempt to add a Rating that is out
        /// of the acceptable bounds for a rating (1-5) is not successful.
        /// </summary>
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

        /// <summary>
        /// Tests to verify that an attempt to add a Rating with an invalid (null)
        /// Product ID is not successful.
        /// </summary>
        [Test]
        public void AddRating_InValid_ProductNull_Should_ReturnFalse()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating(null, 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Tests to verify that an attempt to add a Rating with an Empty String
        /// Product ID is not successful.
        /// </summary>
        [Test]
        public void AddRating_InValid_ProductEmpty_Should_ReturnFalse()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("", 1);

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Tests to verify that an attempt to add a Rating to a product that does
        /// not exist in the Products list is not successful.
        /// </summary>
        [Test]
        public void AddRating_Invalid_ProductDoesNotExist_Should_ReturnFalse()
        {
            // Arrange

            // Act
            var result = TestHelper.ProductService.AddRating("no-such-product", 3);

            // Reset

            // Assert
            Assert.AreEqual(false, result);
        }

        /// <summary>
        /// Tests to verify that an attempt to add a valid rating to a valid product that already
        /// has an existing Ratings array is successful.
        /// </summary>
        [Test]
        public void AddRating_Valid_PriorRatingsExist_Should_AddToRatingArray()
        {
            // Arrange
            const int TestRating = 3;
            const string TestID = "SVI-001";
            var preCount = TestHelper.ProductService.GetProducts().FirstOrDefault(p => p.Id == TestID).Ratings.Length;

            // Act
            var methodOutcome = TestHelper.ProductService.AddRating(TestID, TestRating);
            var testProduct = TestHelper.ProductService.GetProducts().FirstOrDefault(p => p.Id == TestID);

            // Reset

            // Assert
            Assert.AreEqual(true, methodOutcome);
            Assert.AreEqual(preCount + 1, testProduct.Ratings.Length);
            Assert.AreEqual(testProduct.Ratings.Last(), TestRating);
        }

        /// <summary>
        /// Tests to verify that an attempt to add a valid rating to a valid product that does
        /// not have an existing Ratings array is successful.
        /// </summary>
        [Test]
        public void AddRating_Valid_NoRatings_Should_CreateNewRatingsArray()
        {
            // Arrange
            const int TestRating = 3;
            const string TestID = "BLW-051";

            // Act
            var methodOutcome = TestHelper.ProductService.AddRating(TestID, TestRating);
            var testProduct = TestHelper.ProductService.GetProducts().FirstOrDefault(p => p.Id == TestID);

            // Reset

            // Assert
            Assert.AreEqual(true, methodOutcome);
            Assert.AreEqual(1, testProduct.Ratings.Length);
            Assert.AreEqual(testProduct.Ratings.Last(), TestRating);
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


        #region DeleteProduct

        /// <summary>
        /// Tests to verify that an attempt to delete a productID for a product
        /// that does not exist in the database does not delete anything.
        /// </summary>
        [Test]
        public void DeleteProduct_Invalid_NoProductId_Should_ReturnFalse()
        {
            // Arrange
            const string IdToDelete = "Invalid ID";

            // Act
            var result = TestHelper.ProductService.DeleteProduct(IdToDelete);
            var productCount = TestHelper.ProductService.GetProducts().Count();

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(10, productCount);
        }


        /// <summary>
        /// Tests to verify that an attempt to delete a productID for a product
        /// that does exist in the database deletes the product successfully.
        /// </summary>
        [Test]
        public void DeleteProduct_Valid_Default_Should_RemoveItem()
        {
            // Arrange
            const string IdToDelete = "SVI-001";


            // Act
            var result = TestHelper.ProductService.DeleteProduct(IdToDelete);
            var products = TestHelper.ProductService.GetProducts();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(null, products.FirstOrDefault(p => p.Id == IdToDelete));
            Assert.AreEqual(9, products.Count());

        }

        #endregion DeleteProduct

    }
}