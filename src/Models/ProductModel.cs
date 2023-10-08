using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Models
{
    public class ProductModel
    {
        public string Id { get; set; }

        /// <summary>
        /// Name printed on the card. Not necessarily unique.
        /// </summary>
        public string Name { get; set; }
        public string Maker { get; set; }

        /// <summary>
        /// Value of card in USD.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Name of the Expansion Set that the card comes from.
        /// </summary>
        public string Expansion { get; set; }

        /// <summary>
        /// Energy types of the card which include:
        /// grass, lightning, darkness, fairy, fire, psychic, 
        /// metal, dragon, water, fighting and colorless.
        /// Card may have more multiple types.
        /// </summary>
        public List<string> Type { get; set; }

        [JsonPropertyName("img")]
        public string Image { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int[] Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);


    }
}