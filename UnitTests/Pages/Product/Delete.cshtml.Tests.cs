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
using ContosoCrafts.WebSite.Pages.Product.Delete;

namespace UnitTests.Pages.Product.Delete;

/// <summary>
/// Unit tests for Delete page functionality
/// </summary>
public class DeleteTests
{
    /// <summary>
    /// Initializes objects for test enviornment 
    /// </summary>
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
    /// <summary>
    /// Test that a GET request returns a valid product from datastore
    /// </summary>
    [Test]
    public void OnGet_Valid_Should_Return_Product()
    {
        // Arrange
        var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
        mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
        mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

        var productService = new JsonFileProductService(mockWebHostEnvironment.Object);

        var deleteModel = new DeleteModel(productService);

        var lastProductId = productService.GetLastProduct().Id;

        // Act
        deleteModel.OnGet(lastProductId);

        // 

        // Assert
        Assert.AreEqual(true, deleteModel.ModelState.IsValid);
        Assert.IsNotNull(deleteModel.Product);
        Assert.AreEqual(lastProductId, deleteModel.Product.Id);
    }
    #endregion OnGet

    #region OnPost
    /// <summary>
    /// Test to verify that ON POST request, product is removed from data store and from Index page
    /// </summary>
    [Test]
    public void OnPost_Valid_Should_Delete_Product()
    {
        // Arrange
        var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
        mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
        mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

        var productService = new JsonFileProductService(mockWebHostEnvironment.Object);

        var lastProduct = productService.GetLastProduct();
        var deleteModel = new DeleteModel(productService);

        // Act
        var firstCount = productService.GetProducts().Count();
        var result = deleteModel.OnPost(lastProduct.Id);
        var isProductInList = productService.GetProduct(lastProduct.Id) != null;
        var lastCount = productService.GetProducts().Count();

        // Reset
        productService.AddProduct(lastProduct);

        // Assert
        Assert.AreEqual(firstCount - 1, lastCount);
        Assert.AreEqual(false, isProductInList);

        Assert.IsInstanceOf<RedirectToPageResult>(result);
        var redirectToPageResult = result as RedirectToPageResult;
        Assert.AreEqual("/Index", redirectToPageResult.PageName);

    }
    #endregion OnPost
}