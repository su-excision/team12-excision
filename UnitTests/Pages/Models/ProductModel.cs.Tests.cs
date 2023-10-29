using NUnit.Framework;
using ContosoCrafts.WebSite.Models;
using Namespace;

namespace UnitTests.Models.ProductModel
{

    public class ProductModelTests
    {
        #region TestSetup

        [SetUp]
        public void TestInitialize()
        {
        }

        #endregion TestSetup

        #region ToString

        /// <summary>
        /// Tests that the ProductModel ToString method returns a properly serialized JSON file.
        /// </summary>
        [Test]
        public void ToString_Valid_Default_Should_ReturnSerializedJson()
        {
            // Arrange
            var testProduct = new TestProductBuilder().Build();
            const string SerializedOutput = "{\"Id\":\"TST-000\",\"Name\":\"Test Card\",\"Value\":99.99,\"Expansion\":\"Test Expansion\",\"Rarity\":\"Unique\",\"Availability\":999,\"Type\":null,\"img\":\"https://assets.pokemon.com/assets/cms2/img/cards/web/SMA/SMA_EN_SV46.png\",\"Description\":\"A Test Card used for Testing\",\"Ratings\":null}";

            // Act
            string result = testProduct.ToString();

            // Reset

            // Assert
            Assert.AreEqual(SerializedOutput, result);
        }


        #endregion ToString


    }

}
