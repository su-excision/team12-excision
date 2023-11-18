using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Pages
{
    /// <summary>
    /// Padmaja
    /// Mike Koenig
    /// Andrew Asplund
    /// Eduardo Sousa Silva
    /// Emily Bazar
    /// </summary>
  
    /// <summary>
    /// Represents the Index page model.
    /// </summary>
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Holds a logger field.
        /// </summary>
        private readonly ILogger<IndexModel> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexModel"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="productService">The JSON file product service.</param>
        public IndexModel(ILogger<IndexModel> logger,
            JsonFileProductService productService)
        {
            _logger = logger;
            ProductService = productService;
        }

        //Gets the JSON file product service.
        public JsonFileProductService ProductService { get; }

        // Gets or sets the collection of products.
        public IEnumerable<ProductModel> Products { get; private set; }

        /// <summary>
        /// Handles HTTP GET requests for the Index page.
        /// </summary>
        public void OnGet()
        {
            Products = ProductService.GetProducts();
        }
    }
}