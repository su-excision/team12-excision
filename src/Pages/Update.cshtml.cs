using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc.RazorPages;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages.Update
{
    /// <summary>
    /// Update page will return Product data such as Name and Description
    /// </summary>
    public class UpdateModel : PageModel
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="productService"></param>
        public UpdateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        // Data Service
        public JsonFileProductService ProductService { get; }

        // Collection of the Data
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// REST OnGet, return all data
        /// </summary>
        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}