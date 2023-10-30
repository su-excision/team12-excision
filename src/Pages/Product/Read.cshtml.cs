using System.Linq;

using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Product
{
    /// <summary>
    /// Page Model for the Read page of the website.
    /// </summary>
    public class ReadModel : PageModel
    {
        /// <summary>
        /// ProductService for retrieving products from the datastore.
        /// </summary>
        private readonly JsonFileProductService _productService;

        /// <summary>
        /// Constructor for the Product/Read Page Model.
        /// </summary>
        /// <param name="productService">ProductService for retrieivng products
        /// from the datastore.</param>
        public ReadModel(JsonFileProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// The product data to be displayed on the Page.
        /// </summary>
        public ProductModel Product;

        /// <summary>
        /// Method that is called when the page is loaded (after a GET request). Loads
        /// the desired Product from the datastore based on the product Id passed to it.
        /// </summary>
        /// <param name="id">the Product Id for the desired Product to load.</param>
        public void OnGet(string id)
        {
            Product = _productService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
        }
    }
}