using System.Text.Json;
using System.Text.Json.Serialization;

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
        ///  Value of card in USD.
        /// </summary>
        public string Value { get; set; }

        [JsonPropertyName("img")]
        public string Image { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int[] Ratings { get; set; }

        public override string ToString() => JsonSerializer.Serialize<ProductModel>(this);


    }
}