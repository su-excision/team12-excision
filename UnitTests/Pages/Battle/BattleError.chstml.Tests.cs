using ContosoCrafts.WebSite.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NUnit.Framework;

namespace UnitTests.Pages.Battle
{
    /// <summary>
    /// Unit tests for the BattleErrorModel page.
    /// </summary>
    [TestFixture]
    public class BattleErrorModelTests
    {
        private BattleErrorModel _battleErrorModel;

        /// <summary>
        /// Setup method to initialize the BattleErrorModel instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _battleErrorModel = new BattleErrorModel();
        }

        /// <summary>
        /// Test whether the OnGet method returns a valid state
        /// </summary>
        [Test]
        public void OnGet_Should_Return_Valid_State()
        {
            // Act
            _battleErrorModel.OnGet();

            // Assert
            Assert.AreEqual(true, _battleErrorModel.ModelState.IsValid);
        }
    }
}
