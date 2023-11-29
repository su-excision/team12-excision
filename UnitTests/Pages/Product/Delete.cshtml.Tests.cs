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

    public static DeleteModel pageModel;

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

        var MockLoggerDirect = Mock.Of<ILogger<DeleteModel>>();
        JsonFileProductService productService;

        productService = new JsonFileProductService(mockWebHostEnvironment.Object);

        pageModel = new DeleteModel(productService);
    }
    #endregion TestSetup

    #region OnGet
    /// <summary>
    /// Test that a GET request returns a valid product from datastore
    /// </summary>
    [Test]
    public void OnGet_Valid_Should_ReturnProduct()
    {
        // Arrange

        // Act
        pageModel.OnGet(TestConstants.ExpectedLastProductId);

        // 

        // Assert
        Assert.AreEqual(true, pageModel.ModelState.IsValid);
        Assert.IsNotNull(pageModel.Product);
        Assert.AreEqual(TestConstants.ExpectedLastProductId, pageModel.Product.Id);
    }

    /// <summary>
    /// Tests that when Delete tries to read a page that doesn't exist, it returns an Error Code 400.
    /// </summary>
    [Test]
    public void OnGet_Invalid_InvalidProduct_Should_ReturnBadResponse()
    {
        // arrange
        const string InvalidId = "Invalid Id";

        // act
        var result = pageModel.OnGet(InvalidId);

        // assert
        Assert.IsInstanceOf<BadRequestResult>(result);
    }
    #endregion OnGet

    #region OnPost
    /// <summary>
    /// Test to verify that ON POST request, product is removed from data store and from Index page
    /// </summary>
    [Test]
    public void OnPost_Valid_Should_DeleteProduct()
    {
        // Arrange
        pageModel.OnGet(TestConstants.ExpectedLastProductId);
        var lastProduct = pageModel.Product;

        // Act
        var result = pageModel.OnPost(TestConstants.ExpectedLastProductId);
        var isProductInList = pageModel.ProductService.GetProducts().Contains(lastProduct);
        var afterCount = pageModel.ProductService.GetProducts().Count();


        // Reset
        pageModel.ProductService.AddProduct(lastProduct);

        // Assert
        Assert.AreEqual(TestConstants.ExpectedProductCount - 1, afterCount);
        Assert.AreEqual(false, isProductInList);

        Assert.IsInstanceOf<RedirectToPageResult>(result);
        var redirectToPageResult = result as RedirectToPageResult;
        Assert.AreEqual("/Index", redirectToPageResult.PageName);

    }

    /// <summary>
    /// Tests that if it tries to delete a thing htat doesn't exist in goes to error page
    /// </summary>
    [Test]
    public void OnPost_Invalid_InvalidId_Should_ReturnError()
    {
        // arrange
        const string InvalidId = "Invalid Id";

        // act
        var result = pageModel.OnPost(InvalidId);

        // reset

        // assert
        Assert.IsInstanceOf<BadRequestResult>(result);

    }

    #endregion OnPost
}