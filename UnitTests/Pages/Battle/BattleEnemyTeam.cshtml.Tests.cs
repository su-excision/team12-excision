using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Battle;
using ContosoCrafts.WebSite.Services;
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
    /// Unit tests for the Battle functionality of the BattleEnemyTeamModel Page
    /// </summary>
    [TestFixture]
    public class BattleEnemyTeamModelTests
    {
        // Attributes and Objects for Test Setup
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
        private BattleEnemyTeamModel battleEnemyTeamModel;

        #region Setup
        /// <summary>
        /// Setup method to initialize the mock services and the BattleEnemyTeamModel instance.
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
            battleEnemyTeamModel = new BattleEnemyTeamModel(productService);
        }

        #endregion Setup

        #region OnGet

        /// <summary>
        /// Tests whether the OnGet method populates EnemyPokemonTeam.
        /// </summary>
        [Test]
        public void OnGet_Should_Populate_EnemyPokemonTeam()
        {
            // Act
            battleEnemyTeamModel.OnGet();

            // Assert
            Assert.IsNotNull(battleEnemyTeamModel.EnemyPokemonTeam);
            Assert.AreEqual(5, battleEnemyTeamModel.EnemyPokemonTeam.Count());
        }
        #endregion OnGet
    }
}