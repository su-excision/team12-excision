using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Models
{
    internal class CommentModel
    {
        /// <summary>
        /// Tests that the CommentModel Constructor method returns a properly serialized JSON file.
        /// </summary>
        [Test]
        public void Constructor_Valid_NewInstance_Should_AddGuid()
        {
            // Arrange
            var testProduct = new TestProductBuilder().WithComment("12", "test");
            const string SerializedOutput = "{\"Id\":\"TST-000\",\"Name\":\"Test Card\",\"Value\":99.99,\"Expansion\":\"Test Expansion\",\"Rarity\":\"Unique\",\"Availability\":999,\"CardCategory\":0,\"Type\":[],\"img\":\"https://assets.pokemon.com/assets/cms2/img/cards/web/SMA/SMA_EN_SV46.png\",\"Description\":\"A Test Card used for Testing\",\"Ratings\":[],\"CommentList\":[{\"Id\":\"12\",\"Comment\":\"test\"}]}";

            // Act
            string result = testProduct.Build().ToString();

            // Assert
            Assert.AreEqual(SerializedOutput, result);
        }
    }
}
