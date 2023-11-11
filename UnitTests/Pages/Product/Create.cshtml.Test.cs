
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
using ContosoCrafts.WebSite.Pages.Product.Create;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;
using Namespace;
using System.Linq;
using System;


namespace UnitTests.Pages.Product.Create
{
    /// <summary>
    /// Tests associated with Product/Create
    /// </summary>
    internal class CreateTests
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

        /// <summary>
        /// Initialization for tests associated with Product/Create
        /// </summary>
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

        /// <summary>
        /// Test to verify that on a GET request, the page is valid and
        /// has successfully loaded the default product form the datastore.
        /// </summary>

        [Test]
        public void OnGet_Should_Populate_Product_And_AvailableTypes()
        {
            // Arrange
            var productId = "ID";
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var productService = new JsonFileProductService(mockWebHostEnvironment.Object);
            var createModel = new CreateModel(productService);

            // Act
            createModel.OnGet();

            // Assert
            Assert.IsNotNull(createModel.Product);
            Assert.AreEqual(productId, "ID");
            Assert.AreEqual(null, createModel.Product.Name);
            Assert.AreEqual(0f, createModel.Product.Value);
            Assert.AreEqual(null, createModel.Product.Expansion);
            Assert.AreEqual(null, createModel.Product.Rarity);
            Assert.AreEqual(0, createModel.Product.Availability);
            Assert.AreEqual(null, createModel.Product.Image);
            Assert.AreEqual(null, createModel.Product.Description);
            Assert.IsEmpty(createModel.Product.Ratings);
            CollectionAssert.AreEqual(
                new List<string> { "grass", "lightning", "darkness", "fairy", "fire", "psychic", "metal", "dragon", "water", "fighting", "colorless" },
                createModel.AvailableTypes);
        }
        #endregion OnGet


        #region OnPost
        /// <summary>
        /// Test to verify the POST request, the page is valid and
        /// has successfully Created product data
        /// </summary>
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
            var createModel = new CreateModel(productService);

            createModel.ModelState.AddModelError("PropertyName", "Error");

            // Act
            var result = createModel.OnPost();

            // Assert
            Assert.IsInstanceOf<PageResult>(result);
        }
        /// <summary>
        /// Test to verify the POST request, the page is valid and
        /// has successfully Created product data
        /// </summary>
        [Test]
        public void OnPost_Valid_Model_Should_Create_Product_And_Redirect()
        {
            // Arrange
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var testWebHostEnvironment = mockWebHostEnvironment.Object;
            var productService = new JsonFileProductService(testWebHostEnvironment);
            var createModel = new CreateModel(productService);
            var validProduct = new TestProductBuilder().WithId("Test Id").Build();

            createModel.ModelState.Clear();
            createModel.Product = validProduct;

            // Act
            var startCount = productService.GetProducts().Count();
            var result = createModel.OnPost();
            var newProduct = productService.GetProduct(validProduct.Id);
            var finalCount = productService.GetProducts().Count();

            // Reset
            productService.DeleteProduct(validProduct.Id);

            // Assert
            Assert.IsInstanceOf<RedirectToPageResult>(result);
            var redirectToPageResult = (RedirectToPageResult)result;
            Assert.AreEqual("./Index", redirectToPageResult.PageName);
            Assert.AreEqual(startCount + 1, finalCount);
            Assert.AreEqual(validProduct.Id, newProduct.Id);
        }
        #endregion OnPost
    }
}
