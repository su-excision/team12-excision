using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        /// <summary>
        /// Unique ID for the card (usually in the format of <c>SET-INDEX</c>.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name printed on the card. Not necessarily unique.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Value of card in USD.
        /// </summary>
        public string Value { get; set; }

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

        // redundant; replace with Name or Description
        public string Title { get; set; }

        /// <summary>
        /// Detailed description of the card.
        /// </summary>
        public string Description { get; set; }
        public int[] Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);


    }
}