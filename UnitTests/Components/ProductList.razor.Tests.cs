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


            // Reset
            var result = cut.Markup;


            // Assert
            Assert.AreEqual(true, result.Contains(TestString));
        }

        #endregion ProductList

    }
}
