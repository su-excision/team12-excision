using System.Diagnostics;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

using NUnit.Framework;

using Moq;

using ContosoCrafts.WebSite.Pages;
using ContosoCrafts.WebSite.Services;

namespace UnitTests.Pages.Error
{
    /// <summary>
    /// Test class for the ErrorModel, representing the Razor Page model for the "Error" page.
    /// </summary>
    public class ErrorTests
    {
        #region TestSetup
        // Shared test setup variables for reuse in test methods.
        public static IUrlHelperFactory urlHelperFactory;
        public static DefaultHttpContext httpContextDefault;
        public static IWebHostEnvironment webHostEnvironment;
        public static ModelStateDictionary modelState;
        public static ActionContext actionContext;
        public static EmptyModelMetadataProvider modelMetadataProvider;
        public static ViewDataDictionary viewData;
        public static TempDataDictionary tempData;
        public static PageContext pageContext;

        public static ErrorModel pageModel;

        [SetUp]
        public void TestInitialize()
        {
            httpContextDefault = new DefaultHttpContext()
            {
                TraceIdentifier = "trace",
                //RequestServices = serviceProviderMock.Object,
            };
            httpContextDefault.HttpContext.TraceIdentifier = "trace";

            modelState = new ModelStateDictionary();

            actionContext = new ActionContext(httpContextDefault, httpContextDefault.GetRouteData(), new PageActionDescriptor(), modelState);

            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(httpContextDefault, Mock.Of<ITempDataProvider>());

            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
                HttpContext = httpContextDefault
            };

            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            var MockLoggerDirect = Mock.Of<ILogger<ErrorModel>>();
            JsonFileProductService productService;

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            pageModel = new ErrorModel(MockLoggerDirect)
            {
                PageContext = pageContext,
                TempData = tempData,
            };
        }

        #endregion TestSetup

        #region OnGet
        /// <summary>
        /// Test method to verify that the OnGet method returns a valid ModelState when called with default parameters.
        /// </summary>
        [Test]
        public void OnGet_Valid_Activity_Set_Should_Return_RequestId()
        {
            // Arrange

            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            pageModel.OnGet(string.Empty);

            // Reset
            activity.Stop();

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(activity.Id, pageModel.RequestId);
        }

        /// <summary>
        /// Test method to verify that the OnGet method returns the TraceIdentifier as RequestId when the Activity is null.
        /// </summary>
        [Test]
        public void OnGet_InValid_Activity_Null_Should_Return_TraceIdentifier()
        {
            // Arrange

            // Act
            pageModel.OnGet(string.Empty);

            // Reset

            // Assert
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual("trace", pageModel.RequestId);
            Assert.AreEqual(true, pageModel.ShowRequestId);
        }

        /// <summary>
        /// Tests that the Error Message on the Error page is correct for
        /// a 404 when the error code is 404.
        /// </summary>
        [Test]
        public void OnGet_Valid_Error404_Should_ContainErrorMessage()
        {
            // Arrange
            const string ExpectedErrorMessage = "You have attempted to navigate to a page of this site that does not exist.";

            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            pageModel.OnGet("404");

            // Reset
            activity.Stop();

            // Assert
            Assert.AreEqual(ExpectedErrorMessage, pageModel.ErrorMessage);
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(activity.Id, pageModel.RequestId);
        }

        /// <summary>
        /// Tests that the Error Message on the Error page is correct for
        /// a 404 when the error code is 404.
        /// </summary>
        [Test]
        public void OnGet_Valid_Error400_Should_ContainErrorMessage()
        {
            // Arrange
            const string ExpectedErrorMessage = "You have attempted to access data that does not exist or have otherwise caused some upset with our database.";

            Activity activity = new Activity("activity");
            activity.Start();

            // Act
            pageModel.OnGet("400");

            // Reset
            activity.Stop();

            // Assert
            Assert.AreEqual(ExpectedErrorMessage, pageModel.ErrorMessage);
            Assert.AreEqual(true, pageModel.ModelState.IsValid);
            Assert.AreEqual(activity.Id, pageModel.RequestId);
        }

        #endregion OnGet
    }
}