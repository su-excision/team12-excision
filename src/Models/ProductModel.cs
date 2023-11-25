using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;

namespace ContosoCrafts.WebSite.Models
{
    /// <summary>
    /// ProductModel class has the logic for the product fields.
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Unique ID for the card (usually in the format of <c>SET-INDEX</c>.
        /// </summary>
        [Display(Name = "Pokémon Card Unique ID", Prompt = "Enter the Unique ID of the Pokémon Card.")]
        [StringLength(maximumLength: 8, MinimumLength = 6, ErrorMessage = "Unique Card ID should be between {1} and {2} characters long.")]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// Name printed on the card. Not necessarily unique.
        /// </summary>
        [Display(Name = "Card Name", Prompt = "Enter the Name of the Pokémon Card.")]
        [StringLength(maximumLength: 24, MinimumLength = 3, ErrorMessage = "Card Name should be between {1} and {2} characters long.")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Value of card in USD.
        /// </summary>
        [Display(Name = "Value of Card (USD)")]
        [Range(0, 1000, ErrorMessage = "Card Price must be greater than ${1} and less than ${2}.")]
        [Required]
        public float Value { get; set; }

        /// <summary>
        /// Name of the Expansion Set that the card comes from.
        /// </summary>
        [Display(Name = "Expansion Set", Prompt = "Enter the Expansion that the Pokémon Card comes from.")]
        [Required]
        public string Expansion { get; set; }

        /// <summary>
        /// Field that holds the rarity of the card
        /// </summary>
        [Display(Name = "Card Rarity", Prompt = "Enter the Rarity (Common, Uncommon, Rare, Ultra Rare) for the Pokémon Card.")]
        [Required]
        public string Rarity { get; set; }

        /// <summary>
        /// Field that holds the availability of the card
        /// </summary>
        [Display(Name = "Cards Available", Prompt = "Enter the number of Pokémon Cards currently availabled.")]
        [Range(0, 1000, ErrorMessage = "Card Availability must be greater than or equal to {1} and less than {2}.")]
        [Required]
        public int Availability { get; set; }

        /// <summary>
        /// Field for storing the category (or type) of card.
        /// </summary>
        [Display(Name = "Type of Card")]
        [Required]
        public CardType CardCategory { get; set; }

        /// <summary>
        /// Energy types of the card.
        /// </summary>
        [Display(Name = "Energy Types")]
        public List<EnergyType> Type { get; set; } = new List<EnergyType>();

        /// <summary>
        /// Link to an image of the associated card.
        /// </summary>
        [Display(Name = "Card Image URL", Prompt = "Enter the URL for the Pokémon Card Image")]
        [Required]
        [JsonPropertyName("img")]
        public string Image { get; set; }

        /// <summary>
        /// Detailed description of the card.
        /// </summary>
        [Display(Name = "Card Description", Prompt = "Enter the description for the Pokémon Card.")]
        [StringLength(maximumLength: 200, MinimumLength = 10, ErrorMessage = "Card Description should be between {1} and {2} characters long.")]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Array of ratings of the specific product. Ratings are from 1 to 5.
        /// </summary>
        public int[] Ratings { get; set; } = Array.Empty<int>();

        /// <summary>
        /// List of Comments made by users
        /// </summary>
        public List<CommentModel> CommentList { get; set; } = new List<CommentModel>();

        /// <summary>
        /// ToString returns a string of the Product data, serialized in JSON format.
        /// </summary>
        /// <returns>The Product data serialized in JSON format</returns>
        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);

        /// <summary>
        /// CopyTo creates a copy of the Product at the specified
        /// location.
        /// </summary>
        /// <param name="destinationProduct">the ProductModel location to be copied to</param>
        public void CopyTo(ProductModel destinationProduct)
        {
            // not sure if there is a better way to do this
            destinationProduct.Id = this.Id;
            destinationProduct.Name = this.Name;
            destinationProduct.Description = this.Description;
            destinationProduct.Expansion = this.Expansion;
            destinationProduct.Image = this.Image;
            destinationProduct.Rarity = this.Rarity;
            destinationProduct.Availability = this.Availability;
            destinationProduct.Value = this.Value;
            destinationProduct.Ratings = (int[])this.Ratings.Clone();
            destinationProduct.Type = this.Type.ToList();
            destinationProduct.CommentList = this.CommentList.ToList();
        }
    }
}