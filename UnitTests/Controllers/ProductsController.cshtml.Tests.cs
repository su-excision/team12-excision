using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using ContosoCrafts.WebSite.Controllers;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using Microsoft.AspNetCore.Hosting;
using System.Linq;

namespace ContosoCrafts.WebSite.Controllers
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
            // Arrange - Create test product data directly
            var expectedProductCount = 10; // Adjust this value as needed

            // Act
            var result = productsController.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<ProductModel>>(result);

            var actualProducts = (IEnumerable<ProductModel>)result;
            Assert.AreEqual(expectedProductCount, actualProducts.Count());
        }
        #endregion Get
    }
}