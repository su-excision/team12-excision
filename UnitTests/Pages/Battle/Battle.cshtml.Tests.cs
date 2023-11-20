using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Pages.Battle;
using ContosoCrafts.WebSite.Services;
using System.Collections.Generic;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace UnitTests.Pages.Battle
{
    /// <summary>
    /// Unit tests for the Battle functionality of the BattleModel Page
    /// </summary>
    [TestFixture]
    public class BattleModelTests
    {
        // Attributes and objects used for test setup
        private Mock<IWebHostEnvironment> mockWebHostEnvironment;
        private Mock<ILogger<BattleModel>> mockLogger;
        private Mock<ITempDataProvider> mockTempDataProvider;
        private ModelStateDictionary modelState;
        private ActionContext actionContext;
        private EmptyModelMetadataProvider modelMetadataProvider;
        private ViewDataDictionary viewData;
        private TempDataDictionary tempData;
        private PageContext pageContext;
        private JsonFileProductService productService;
        private BattleModel battleModel;

        /// <summary>
        /// Setup method to initialize the mock services and the BattleModel instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            // Arrange
            mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            mockWebHostEnvironment.Setup(m => m.EnvironmentName).Returns("Hosting:UnitTestEnvironment");
            mockWebHostEnvironment.Setup(m => m.WebRootPath).Returns("../../../../src/bin/Debug/net7.0/wwwroot");
            mockWebHostEnvironment.Setup(m => m.ContentRootPath).Returns("./data/");

            mockLogger = new Mock<ILogger<BattleModel>>();
            mockTempDataProvider = new Mock<ITempDataProvider>();

            modelState = new ModelStateDictionary();
            actionContext = new ActionContext(new DefaultHttpContext(), new RouteData(), new PageActionDescriptor(), modelState);
            modelMetadataProvider = new EmptyModelMetadataProvider();
            viewData = new ViewDataDictionary(modelMetadataProvider, modelState);
            tempData = new TempDataDictionary(new DefaultHttpContext(), mockTempDataProvider.Object);
            pageContext = new PageContext(actionContext)
            {
                ViewData = viewData,
            };

            productService = new JsonFileProductService(mockWebHostEnvironment.Object);

            battleModel = new BattleModel(productService);
        }

        #region OnGet
        /// <summary>
        /// Tests whether the OnGet method populates PokemonGeneralCards.
        /// </summary>
        [Test]
        public void OnGet_Should_Populate_PokemonGeneralCards()
        {
            // Act
            battleModel.OnGet();

            // Assert
            Assert.IsNotNull(battleModel.PokemonGeneralCards);
        }
        #endregion OnGet

        #region OnPost
        /// <summary>
        /// Tests whether OnPost redirects to BattleEnemyTeam when SelectedPokemon count is 5.
        /// </summary>
        [Test]
        public void OnPost_Should_RedirectTo_BattleEnemyTeam_When_SelectedPokemon_Count_Is_5()
        {
            // Arrange
            var selectedPokemonIds = new StringValues(new string[] { "1", "2", "3", "4", "5" });

            // Mock HttpContext and Request
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>
            {
                { "SelectedPokemon", selectedPokemonIds }
            });

            // Set the mocked HttpContext in PageContext
            battleModel.PageContext = new PageContext(actionContext)
            {
                HttpContext = httpContext,
                ViewData = viewData,
            };

            // Act
            var result = battleModel.OnPost();

            // Assert
            Assert.IsInstanceOf<RedirectToPageResult>(result);
            var redirectToPageResult = result as RedirectToPageResult;
            Assert.AreEqual("/Battle/BattleEnemyTeam", redirectToPageResult.PageName);
        }

        /// <summary>
        /// Tests whether OnPost redirects to BattleError when SelectedPokemon count is not 5.
        /// </summary>
        [Test]
        public void OnPost_Should_RedirectTo_BattleError_When_SelectedPokemon_Count_Is_Not_5()
        {
            // Arrange
            var selectedPokemonIds = new StringValues(new string[] { "1", "2", "3", "4" });

            // Mock HttpContext and Request
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Form = new FormCollection(new Dictionary<string, StringValues>
            {
                { "SelectedPokemon", selectedPokemonIds }
            });

            // Set the mocked HttpContext in PageContext
            battleModel.PageContext = new PageContext(actionContext)
            {
                HttpContext = httpContext,
                ViewData = viewData,
            };

            // Act
            var result = battleModel.OnPost();

            // Assert
            Assert.IsInstanceOf<RedirectToPageResult>(result);
            var redirectToPageResult = result as RedirectToPageResult;
            Assert.AreEqual("/Battle/BattleError", redirectToPageResult.PageName);
        }
        #endregion
    }
}