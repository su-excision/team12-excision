using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UnitTests.Controllers
{
    [TestFixture]
    public class ProductsControllerTests
    {
        #region Setup
        private ProductsController productsController;
        private IWebHostEnvironment testWebHostEnvironment;

        [SetUp]
        public void TestInitialize()
        {
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data");

            testWebHostEnvironment = mockWebHostEnvironment.Object;
            var productService = new JsonFileProductService(testWebHostEnvironment);
            productsController = new ProductsController(productService);
        }
        #endregion Setup

        #region Get
        [Test]
        public void Get_Valid_Test_Reading_Should_Return_IsBadImage_False()
        {
            // Arrange
            var expectedProductCount = 10;

            // Act
            var result = productsController.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<ProductModel>>(result);

            var actualProducts = (IEnumerable<ProductModel>)result;
            Assert.AreEqual(expectedProductCount, actualProducts.Count());
        }
        #endregion Get

        #region Patch
        [Test]
        public void Patch_With_ValidRequest_Should_ReturnOk()
        {
            // Arrange 
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data");

            var productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            var productsController = new ProductsController(productService);

            var request = new ProductsController.RatingRequest
            {
                ProductId = "testProductId",
                Rating = 5
            };

            // Act
            var result = productsController.Patch(request);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
        #endregion Patch
    }
}