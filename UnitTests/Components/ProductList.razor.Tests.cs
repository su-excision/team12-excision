using System.Linq;
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

        #region TypeFilter

        /// <summary>
        /// Tests to verify that selecting a type from the type selector
        /// will filter the list down to only cards containing that type.
        /// </summary>
        [Test]
        public void TypeFilter_Valid_OnFilter_Should_ContainValidResult()
        {
            const string TypeFilterId = "type_selector";
            const string ValidFilter = "Dark";

            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);

            // Arrange: render the component
            var cut = RenderComponent<ProductList>();

            // Act

            // Act: Select the option to filter for.
            var typeFilter = cut.FindAll("Select").First(element => element.OuterHtml.Contains(TypeFilterId));
            typeFilter.Change(ValidFilter);


            // Act: Count how many items appear on the list
            var resultCount = cut.FindAll("Div").First(element => element.ClassName == "card-deck").ChildElementCount;

            // Act: get markup for page
            var markup = cut.Markup;

            // Reset

            // Assert
            Assert.AreEqual(1, resultCount);
            Assert.AreEqual(true, markup.Contains("Alolan Raticate"));
        }

        #endregion

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

        #endregion AddRating

        #region AddComments
        [Test]
        public void AddComment_Valid_NewComment_Should_AddComment()
        {
            // Arrange
            Services.AddSingleton<JsonFileProductService>(TestHelper.ProductService);
            const string TestButtonId = "MoreInfo_AR-029";
            const string NewCommentButtonId = "new_comment_button";
            const string NewCommentInputId = "new_comment_input";
            const string AddCommentButtonId = "add_comment_button";
            const string TestComment = "This is a test comment.";

            // Arrange: Built and find the More Info button
            var cut = RenderComponent<ProductList>();
            var moreInfoButton = cut.FindAll("Button").First(element => element.OuterHtml.Contains(TestButtonId));

            // Arrange: Click button and save markup
            moreInfoButton.Click();
            var preCommentMarkup = cut.Markup;

            // Arrange: Find comment button and click
            var newCommentButton = cut.FindAll("Button").First(element => element.OuterHtml.Contains(NewCommentButtonId));
            newCommentButton.Click();

            // Act

            // Act: Find input box, add text
            var newCommentInput = cut.FindAll("Input").First(element => element.OuterHtml.Contains(NewCommentInputId));
            newCommentInput.Change(TestComment);

            // Act: Find add button and save comment
            var addCommentButton = cut.FindAll("Button").First(element => element.OuterHtml.Contains(AddCommentButtonId));
            addCommentButton.Click();

            var postCommentMarkup = cut.Markup;

            // Reset

            // Assert
            Assert.AreEqual(false, preCommentMarkup.Contains(TestComment));
            Assert.AreEqual(true, postCommentMarkup.Contains(TestComment));
        }

        #endregion AddComments
    }
}