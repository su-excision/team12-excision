using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;

namespace ContosoCrafts.WebSite.Controllers
{
    /// <summary>
    /// Controller for managing products
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Initializes a new instance of the ProductsController class
        /// </summary>
        /// <param name="productService">The JSON file product service.</param>
        public ProductsController(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Gets the product service
        /// </summary>
        /// <returns>The product service instance.</returns>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Gets a list of products
        /// </summary>
        /// <returns>An IEnumerable of ProductModel objects.</returns>
        [HttpGet]
        public IEnumerable<ProductModel> Get()
        {
            return ProductService.GetProducts();
        }

        /// <summary>
        /// Adds a rating to a product
        /// </summary>
        /// <param name="request">The rating request.</param>
        /// <returns>An IActionResult indicating the result of the operation.</returns>
        [HttpPatch]
        public ActionResult Patch([FromBody] RatingRequest request)
        {
            ProductService.AddRating(request.ProductId, request.Rating);
            
            return Ok();
        }

        /// <summary>
        /// Represents a rating request that has a ProductId and a Rating
        /// </summary>
        public class RatingRequest
        {
            public string ProductId { get; set; }
            public int Rating { get; set; }
        }
    }
}