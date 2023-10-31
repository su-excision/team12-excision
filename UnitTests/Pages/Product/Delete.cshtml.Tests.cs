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
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;

namespace UnitTests.Pages.Product.Delete;

public class DeleteTests
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
    public void OnGet_Valid_Should_Return_Product()
    {
        // Arrange
        var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
        mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
        mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
        mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

        var productService = new JsonFileProductService(mockWebHostEnvironment.Object);

        var existingProductId = "FLI-122";
        var deleteModel = new DeleteModel(productService);

        // Act
        deleteModel.OnGet(existingProductId);

        // Assert
        Assert.AreEqual(true, deleteModel.ModelState.IsValid);
        Assert.IsNotNull(deleteModel.Product);
        Assert.AreEqual("FLI-122", deleteModel.Product.Id);
    }
    #endregion OnGet


}