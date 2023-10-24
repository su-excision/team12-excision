using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;

using ContosoCrafts.WebSite.Pages.Product;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Pages.Product.Update;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;

namespace UnitTests.Pages.Product.Update
{
    public class UpdateTests
    {
        #region TestSetup
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static IndexModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext()
            {
                //RequestServices = serviceProviderMock.Object,
            };

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<IndexModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new IndexModel(productService)
            {
            };
        }

        #endregion TestSetup

        #region OnGet
        [Test]
        public void OnGet_Valid_Should_Return_Products()
        {
            // Arrange

            // Act
            pageModel.OnGet();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(10, pageModel.Products.ToList().Count);
        }

        [Test]
        public void OnGet_Should_Populate_Product_And_AvailableTypes()
        {
            // Arrange
            var productId = "SVI-002";
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var productService = new JsonFileProductService(mockWebHostEnvironment.Object);
            var updateModel = new UpdateModel(productService);

            // Act
            updateModel.OnGet(productId);

            // Assert
            Assert.IsNotNull(updateModel.Product);
            Assert.AreEqual(productId, updateModel.Product.Id);
            Assert.AreEqual("Heracross", updateModel.Product.Name);
            Assert.AreEqual("It gathers in forests to search for tree sap, its favorite food. It's strong enough to hurl foes.", updateModel.Product.Description);
            Assert.AreEqual("$14.99", updateModel.Product.Value);
            Assert.AreEqual("Uncommon", updateModel.Product.Rarity);
            Assert.AreEqual(8, updateModel.Product.Availability);
            CollectionAssert.AreEqual(
                new List<string> { "grass", "lightning", "darkness", "fairy", "fire", "psychic", "metal", "dragon", "water", "fighting", "colorless" },
                updateModel.AvailableTypes);
        }
        #endregion OnGet


        #region ProductService
        [Test]
        public void ProductService_Property_Should_Return_Expected_Value()
        {
            // Arrange
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var expectedProductService = new JsonFileProductService(mockWebHostEnvironment.Object);
            var updateModel = new UpdateModel(expectedProductService);

            // Act
            var actualProductService = updateModel.ProductService;

            // Assert
            Assert.AreEqual(expectedProductService, actualProductService);
        }
        #endregion ProductService

        #region ProductProperty
        [Test]
        public void ProductProperty_Get_Should_Return_Expected_Value()
        {
            // Arrange
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var expectedProductService = new JsonFileProductService(mockWebHostEnvironment.Object);
            var updateModel = new UpdateModel(expectedProductService);
            var expectedProduct = new ProductModel
            {
                Id = "SVI-002",
                Name = "Heracross",
                Description = "It gathers in forests to search for tree sap, its favorite food. It's strong enough to hurl foes.",
                Value = "$14.99",
                Rarity = "Uncommon",
                Availability = 8,
                Type = new List<string> { "Bug", "Fighting" }

            };

            // Act
            updateModel.Product = expectedProduct;
            var actualProduct = updateModel.Product;

            // Assert
            Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
            Assert.AreEqual(expectedProduct.Name, actualProduct.Name);
            Assert.AreEqual(expectedProduct.Description, actualProduct.Description);
            Assert.AreEqual(expectedProduct.Value, actualProduct.Value);
            Assert.AreEqual(expectedProduct.Rarity, actualProduct.Rarity);
            Assert.AreEqual(expectedProduct.Availability, actualProduct.Availability);
            CollectionAssert.AreEqual(expectedProduct.Type, actualProduct.Type);
        }
        #endregion ProductProperty getter

        #region ProductProperty setter
        [Test]
        public void ProductProperty_Set_Should_Set_Expected_Value()
        {
            // Arrange
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var productService = new JsonFileProductService(mockWebHostEnvironment.Object);
            var updateModel = new UpdateModel(productService);
            var expectedProduct = new ProductModel
            {
                Id = "SVI-002",
                Name = "Heracross",
                Description = "It gathers in forests to search for tree sap, its favorite food. It's strong enough to hurl foes.",
                Value = "$14.99",
                Rarity = "Uncommon",
                Availability = 8,
                Type = new List<string> { "Bug", "Fighting" }
            };

            // Act
            updateModel.Product = expectedProduct;

            // Assert
            var actualProduct = updateModel.Product;
            Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
            Assert.AreEqual(expectedProduct.Name, actualProduct.Name);
            Assert.AreEqual(expectedProduct.Description, actualProduct.Description);
            Assert.AreEqual(expectedProduct.Value, actualProduct.Value);
            Assert.AreEqual(expectedProduct.Rarity, actualProduct.Rarity);
            Assert.AreEqual(expectedProduct.Availability, actualProduct.Availability);
            CollectionAssert.AreEqual(expectedProduct.Type, actualProduct.Type);
        }
        #endregion ProductProperty setter

        #region AvailableTypesProperty Getter
        [Test]
        public void AvailableTypesProperty_Get_Should_Return_Expected_Value()
        {
            // Arrange
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var productService = new JsonFileProductService(mockWebHostEnvironment.Object);
            var updateModel = new UpdateModel(productService);

            updateModel.AvailableTypes = new List<string>
            {
                "grass", "lightning", "darkness", "fairy", "fire",
                "psychic", "metal", "dragon", "water", "fighting", "colorless"
            };
            var expectedTypes = new List<string>
            {
                "grass", "lightning", "darkness", "fairy", "fire",
                "psychic", "metal", "dragon", "water", "fighting", "colorless"
            };

            // Act

            // Assert
            CollectionAssert.AreEqual(expectedTypes, updateModel.AvailableTypes);
        }
        #endregion AvailableTypesProperty Getter

        #region AvailableTypesProperty Setter
        [Test]
        public void AvailableTypesProperty_Set_Should_Set_Expected_Value()
        {
            // Arrange
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var productService = new JsonFileProductService(mockWebHostEnvironment.Object);
            var updateModel = new UpdateModel(productService);
            var expectedTypes = new List<string>
            {
                "grass", "lightning", "darkness", "fairy", "fire",
                "psychic", "metal", "dragon", "water", "fighting", "colorless"
            };

            // Act
            updateModel.AvailableTypes = expectedTypes;

            // Assert
            CollectionAssert.AreEqual(expectedTypes, updateModel.AvailableTypes);
        }
        #endregion AvailableTypesProperty Setter

        #region OnPost
        [Test]
        public void OnPost_Valid_Model_Should_Return_Redirect_To_PageResult()
        {
            // Arrange
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var testWebHostEnvironment = mockWebHostEnvironment.Object;
            var productService = new JsonFileProductService(testWebHostEnvironment);
            var updateModel = new UpdateModel(productService);

            updateModel.ModelState.AddModelError("PropertyName", "Error");

            // Act
            var result = updateModel.OnPost();

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
        }

        [Test]
        public void OnPost_Valid_Model_Should_Update_Product_And_Redirect()
        {
            // Arrange
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var testWebHostEnvironment = mockWebHostEnvironment.Object;
            var productService = new JsonFileProductService(testWebHostEnvironment);
            var updateModel = new UpdateModel(productService);
            var validProduct = new ProductModel
            {
                Id = "SVI-002",
                Name = "Heracross",
                Description = "It gathers in forests to search for tree sap, its favorite food. It's strong enough to hurl foes.",
                Value = "$14.99",
                Rarity = "Uncommon",
                Availability = 8,
                Type = new List<string> { "Bug", "Fighting" }
            };

            updateModel.ModelState.Clear();
            updateModel.Product = validProduct;

            // Act
            var result = updateModel.OnPost();

            // Assert
            Assert.IsInstanceOf<RedirectToPageResult>(result);
            var redirectToPageResult = (RedirectToPageResult)result;
            Assert.AreEqual("./Index", redirectToPageResult.PageName);
        }
        #endregion OnPost
    }
}