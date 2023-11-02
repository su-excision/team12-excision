

using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product.Delete
{
    /// <summary>
    /// Update page will return Product data such as Name and Description
    /// </summary>
    public class DeleteModel : PageModel
    {
        /// <summary>
        /// ProductService for retrieving products from the datastore.
        /// </summary>
        private readonly JsonFileProductService _productService;

        /// <summary>
        /// Constructor for the Delete Page Model.
        /// </summary>
        /// <param name="productService">ProductService for retrieivng products
        /// from the datastore.</param>
        public DeleteModel(JsonFileProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// The product data to be displayed on the Page.
        /// </summary>
        public ProductModel Product { get; set; }

        /// <summary>
        /// Method that is called when the page is loaded (after a GET request). Loads
        /// the desired Product from the datastore based on the product Id passed to it.
        /// </summary>
        /// <param name="id">the Product Id for the desired Product to load.</param>
        public void OnGet(string id)
        {
            Product = _productService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));
        }

        /// <summary>
        ///  Handles POST request to delete a product
        /// </summary>
        /// <returns> Index page if successful 
        public IActionResult OnPost(string id)
        {
            ProductModel product = _productService.GetProducts().FirstOrDefault(m => m.Id.Equals(id));   
            _productService.DeleteProduct(id);
            return RedirectToPage("/Index"); 
        }
    }
}



