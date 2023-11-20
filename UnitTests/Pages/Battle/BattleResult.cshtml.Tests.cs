using Moq;
using NUnit.Framework;
using ContosoCrafts.WebSite.Pages.Battle;
using System;
using System.Reflection;

namespace UnitTests.Pages.Battle
{
    /// <summary>
    /// Unit tests for the Battle functionality of the BattleResultModel Page
    /// </summary>
    [TestFixture]
    public class BattleResultModelTests
    {
        #region OnPost
        /// <summary>
        /// Tests whether OnPost sets BattleOutcomeMessage correctly when winning the battle.
        /// </summary>
        [Test]
        public void OnPost_Should_Set_BattleOutcomeMessage_Win()
        {
            // Arrange
            var battleResultModel = new BattleResultModel
            {
                Random = new Mock<Random>().Object
            };

            // Mock to return 0
            Mock.Get(battleResultModel.Random)
                .Setup(r => r.Next(It.IsAny<int>()))
                .Returns(0);

            // Act
            battleResultModel.OnPost();

            // Assert
            Assert.IsNotNull(battleResultModel.BattleOutcomeMessage);
            StringAssert.Contains("Congratulations! You won the battle!", battleResultModel.BattleOutcomeMessage);
        }

        /// <summary>
        /// Tests whether OnPost sets BattleOutcomeMessage correctly when losing the battle.
        /// </summary>
        [Test]
        public void OnPost_Should_Set_BattleOutcomeMessage_Lose()
        {
            // Arrange
            var battleResultModel = new BattleResultModel
            {
                Random = new Mock<Random>().Object
            };

            // Mock to return 1
            Mock.Get(battleResultModel.Random)
                .Setup(r => r.Next(It.IsAny<int>()))
                .Returns(1);

            // Act
            battleResultModel.OnPost();

            // Assert
            Assert.IsNotNull(battleResultModel.BattleOutcomeMessage);
            StringAssert.Contains("Sorry, you were utterly defeated.", battleResultModel.BattleOutcomeMessage);
        }
        #endregion OnPost
    }
}