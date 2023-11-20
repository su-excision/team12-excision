using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Product.Create
{
    /// <summary>
    /// Create page will create and add a new entry data on the database
    /// </summary>
    public class CreateModel : PageModel
    {
        /// Default Constructor
        /// <param name="productService"></param>
        public CreateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Establishes product service for managing products
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Establishes a standard product data to display and update
        /// [BindProperty] to ensure data is posted back to page
        /// </summary>
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Available product types as a list of strings
        /// </summary>
        public List<string> AvailableTypes { get; set; }

        /// <summary>
        /// GET requests 
        /// </summary>
        public void OnGet()
        {
            /// Initialize the ProductModel with default values
            Product = new ProductModel();
            /// Display the list of types
            AvailableTypes = new List<string>
            {
                "grass", "lightning", "darkness", "fairy", "fire",
                "psychic", "metal", "dragon", "water", "fighting", "colorless"
            };
        }

        /// <summary>
        /// POST request
        /// Updates product data
        /// </summary>
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if ((Product.Type.Count > 0) && (Product.Type[0] == null))
                {
                    Product.Type = new List<string>();
                }
                ProductService.AddProduct(Product); // Save the updated data

                return RedirectToPage("./Index"); // Redirect to the product list page
            }
            return Page();
        }
    }
}
