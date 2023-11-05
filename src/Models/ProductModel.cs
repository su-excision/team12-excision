using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoCrafts.WebSite.Models
{
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
        [Display(Name = "Pokémon Card Name", Prompt = "Enter the Name of the Pokémon Card.")]
        [StringLength(maximumLength: 24, MinimumLength = 3, ErrorMessage = "Card Name should be between {1} and {2} characters long.")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Value of card in USD.
        /// </summary>
        [Range(0, 1000, ErrorMessage = "Card Price must be greater than ${1} and less than ${2}.")]
        [Required]
        public float Value { get; set; }

        /// <summary>
        /// Name of the Expansion Set that the card comes from.
        /// </summary>
        public string Expansion { get; set; }


        /// <summary>
        /// Field that holds the rarity of the card
        /// </summary>
        public string Rarity { get; set; }

        /// <summary>
        /// Field that holds the availability of the card
        /// </summary>
        public int Availability { get; set; }


        /// <summary>
        /// Energy types of the card which include:
        /// grass, lightning, darkness, fairy, fire, psychic, 
        /// metal, dragon, water, fighting and colorless.
        /// Card may have more multiple types.
        /// </summary>
        public List<string> Type { get; set; }

        /// <summary>
        /// Link to an image of the associated card.
        /// </summary>
        [JsonPropertyName("img")]
        public string Image { get; set; }

        /// <summary>
        /// Detailed description of the card.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Array of ratings of the specific product. Ratings are from 1 to 5.
        /// </summary>
        public int[] Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);


    }
}