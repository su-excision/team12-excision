using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Bunit;
using NUnit.Framework;
using ContosoCrafts.WebSite.Components;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace UnitTests.Components
{
    /// <summary>
    /// Unit Tests for the ProductList Blazor component.
    /// </summary>
    public class ProductListTests : BunitTestContext
    {
        #region ProductList


        /// <summary>
        /// Tests that the ProductList builds correctly and contains the
        /// appropriate HTML content.
        /// </summary>
        [Test]
        public void ProductList_Default_Rendered_Should_ReturnContent()
        {
            // Arrange
            const string TestString = "Pineco";
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);


            // Act
            var cut = RenderComponent<ProductList>();
            var result = cut.Markup;

            // Reset

            // Assert
            Assert.AreEqual(true, result.Contains(TestString));
        }

        #endregion ProductList

        #region SelectProduct

        /// <summary>
        /// Tests to see that the More Info button brings up the modal containing the correct product data.
        /// </summary>
        [Test]
        public void SelectProduct_Valid_ValidId_Should_ReturnContent()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            const string TestButtonId = "MoreInfo_XY-088";
            const string TestDescription = "When its huge eyes waver, it sings a mysteriously soothing melody that lulls its enemies to sleep.";

            var cut = RenderComponent<ProductList>();
            var testButton = cut.FindAll("Button").First(element => element.OuterHtml.Contains(TestButtonId));

            // Act

            testButton.Click();
            var markup = cut.Markup;

            // Reset

            // Assert
            Assert.AreEqual(true, markup.Contains(TestDescription));

        }

        #endregion SelectProduct

    }
}
