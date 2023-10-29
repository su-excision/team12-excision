using System.Linq;

using Microsoft.AspNetCore.Mvc;

using NUnit.Framework;

using ContosoCrafts.WebSite.Models;
using Namespace;

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

        #region AddProduct

        /// <summary>
        /// Tests to verify that an attempt to add a product to the database where there
        /// already exists a product with the same Product ID is not successful.
        /// </summary>
        [Test]
        public void AddProduct_Invalid_ExistingProduct_Should_ReturnFalse()
        {
            // Arrange
            const string DuplicateId = "SVI-001";
            var duplicateProduct = new TestProductBuilder().WithId(DuplicateId).Build();

            var startingCount = TestHelper.ProductService.GetProducts().Count();

            // Act
            var result = TestHelper.ProductService.AddProduct(duplicateProduct);
            var finalCount = TestHelper.ProductService.GetProducts().Count();

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(startingCount, finalCount);
        }

        /// <summary>
        /// Tests to verify that an attempt to add a product to the database where there
        /// is product with that Product ID results in adding the product to the list.
        /// </summary>
        [Test]
        public void AddProduct_Valid_Default_Should_AddToProductList()
        {
            // Arrange
            const string NewTestId = "New ID";
            var newProduct = new TestProductBuilder().WithId(NewTestId).Build();
            var startingCount = TestHelper.ProductService.GetProducts().Count();

            // Act
            var result = TestHelper.ProductService.AddProduct(newProduct);
            var isProductInList = (TestHelper.ProductService.GetProducts().FirstOrDefault(p => p.Id == NewTestId) != null);
            var finalCount = TestHelper.ProductService.GetProducts().Count();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(true, isProductInList);
            Assert.AreEqual(startingCount + 1, finalCount);


        }

        #endregion AddProduct


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
            int startingCount = TestHelper.ProductService.GetProducts().Count();


            // Act
            var result = TestHelper.ProductService.DeleteProduct(IdToDelete);
            var finalCount = TestHelper.ProductService.GetProducts().Count();

            // Assert
            Assert.AreEqual(false, result);
            Assert.AreEqual(startingCount, finalCount);
        }


        /// <summary>
        /// Tests to verify that an attempt to delete a productID for a product
        /// that does exist in the database deletes the product successfully.
        /// </summary>
        [Test]
        public void DeleteProduct_Valid_ContainsProduct_Should_RemoveItem()
        {
            // Arrange
            string idToDelete = TestHelper.ProductService.GetProducts().First<ProductModel>().Id;
            int startingCount = TestHelper.ProductService.GetProducts().Count();

            // Act
            var result = TestHelper.ProductService.DeleteProduct(idToDelete);
            var isProductInList = (TestHelper.ProductService.GetProducts().FirstOrDefault(p => p.Id == idToDelete) != null);
            var finalCount = TestHelper.ProductService.GetProducts().Count();

            // Assert
            Assert.AreEqual(true, result);
            Assert.AreEqual(false, isProductInList);
            Assert.AreEqual(startingCount - 1, finalCount);

        }

        #endregion DeleteProduct

    }
}