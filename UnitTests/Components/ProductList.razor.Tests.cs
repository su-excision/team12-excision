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
using AngleSharp.Dom;

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
            const string TestButtonId = "MoreInfo_SVI-003";
            const string TestDescription = "A small egg-shaped Pokemon";

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

        #region TextFilter

        /// <summary>
        /// Tests that the search filter works properly for valid search field.
        /// Should return matching results.
        /// </summary>
        [Test]
        public void TextFilter_Valid_OnSearch_Should_ContainValidResult()
        {
            const string TextFilterId = "text_filter";
            const string FilterButtonId = "filter_button";

            const string ValidSearchText = "rat";

            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Arrange: render the component
            var cut = RenderComponent<ProductList>();

            // Act

            // Act: Enter the text to search for
            var inputFilter = cut.FindAll("Input").First(element => element.OuterHtml.Contains(TextFilterId));
            inputFilter.Change(ValidSearchText);

            // Act: Click the search button
            var filterButton = cut.FindAll("Button").First(element => element.OuterHtml.Contains(FilterButtonId));
            filterButton.Click();

            // Act: Count how many items appear on the list
            var resultCount = cut.FindAll("Div").First(element => element.ClassName == "card-deck").ChildElementCount;

            // Act: get markup for page
            var markup = cut.Markup;

            // Reset

            // Assert
            Assert.AreEqual(2, resultCount);
            Assert.AreEqual(true, markup.Contains("Raticate"));

        }

        /// <summary>
        /// Tests that the search filter works properly for invalid search field.
        /// Should return no matching results.
        /// </summary>
        [Test]
        public void TextFilter_Invalid_NoMatch_Should_ReturnEmpty()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            const string TextFilterId = "text_filter";
            const string FilterButtonId = "filter_button";

            const string InvalidSearchText = "invalid search";

            var cut = RenderComponent<ProductList>();

            // Act
            var inputFilter = cut.FindAll("Input").First(element => element.OuterHtml.Contains(TextFilterId));
            inputFilter.Change(InvalidSearchText);

            var filterButton = cut.FindAll("Button").First(element => element.OuterHtml.Contains(FilterButtonId));
            filterButton.Click();

            var resultsDiv = cut.FindAll("Div").First(element => element.ClassName == "card-deck");

            // Reset

            // Assert
            Assert.AreEqual(0, resultsDiv.ChildElementCount);

        }

        /// <summary>
        /// Test for Text Filter Clear button. Will conduct a search and then
        /// clear the search field, returning the original results.
        /// </summary>
        [Test]
        public void TextFilter_Valid_OnClear_Should_ReturnOriginalList()
        {
            const string TextFilterId = "text_filter";
            const string FilterButtonId = "filter_button";
            const string ClearButtonId = "clear_button";

            const string ValidSearchText = "Jigglypuff";

            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);


            // Arrange: render the component
            var cut = RenderComponent<ProductList>();

            // Arrange: count how many items appear on the list
            var initialCount = cut.FindAll("Div").First(element => element.ClassName == "card-deck").ChildElementCount;

            // Act

            // Act: Enter the text to search for
            var inputFilter = cut.FindAll("Input").First(element => element.OuterHtml.Contains(TextFilterId));
            inputFilter.Change(ValidSearchText);

            // Act: Click the search button
            var filterButton = cut.FindAll("Button").First(element => element.OuterHtml.Contains(FilterButtonId));
            filterButton.Click();

            // Act: Count how many items appear on the list
            var midCount = cut.FindAll("Div").First(element => element.ClassName == "card-deck").ChildElementCount;

            // Act: Clear the search
            var clearButton = cut.FindAll("Button").First(element => element.OuterHtml.Contains(ClearButtonId));
            clearButton.Click();

            // Act: Count how many items appear after clearing
            var finalCount = cut.FindAll("Div").First(element => element.ClassName == "card-deck").ChildElementCount;


            // Reset

            // Assert
            Assert.AreEqual(initialCount, finalCount);
            Assert.AreNotEqual(midCount, finalCount);

        }

        #endregion TextFilter

        #region AddRating

        /// <summary>
        /// Test for testing the AddRating button.
        /// </summary>
        [Test]
        public void AddRating_Valid_RatingClick_Should_ReturnNewRating()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            const string TestButtonId = "MoreInfo_AR-029";
            const string VoteButtonId = "vote_button";

            const string PreVoteString = "Be the first to vote!";
            const string PostVoteString = "1 Vote";

            // Arrange: Built and find the More Info button
            var cut = RenderComponent<ProductList>();
            var moreInfoButton = cut.FindAll("Button").First(element => element.OuterHtml.Contains(TestButtonId));

            // Arrange: Click button and save markup
            moreInfoButton.Click();
            var preVoteMarkup = cut.Markup;

            // Act

            // Act: Find voting button
            var voteButton = cut.FindAll("Img").First(element => element.OuterHtml.Contains(VoteButtonId));

            // Act: Click button and save markup
            voteButton.Click();
            var postVoteMarkup = cut.Markup;


            // Reset

            // Assert
            Assert.AreEqual(true, preVoteMarkup.Contains(PreVoteString));
            Assert.AreEqual(true, postVoteMarkup.Contains(PostVoteString));
            Assert.AreNotEqual(preVoteMarkup, postVoteMarkup);

        }

            var moreInfoButton = cut.FindAll("Button").First(element => element.OuterHtml.Contains(TestButtonId));

        }

        #endregion AddRating

    }
}
