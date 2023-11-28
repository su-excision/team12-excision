using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Product.Update
{
    /// <summary>
    /// Update page will return Product data such as Name and Description
    /// </summary>
    public class UpdateModel : PageModel
    {
        /// Default Constructor
        /// <param name="productService"></param>
        public UpdateModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Establishes product service for managing products
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Establishes product data to display and update
        /// [BindProperty] to ensure data is posted back to page
        /// </summary>
        [BindProperty]
        public ProductModel Product { get; set; }

        /// <summary>
        /// Available product types as a list of strings
        /// </summary>
        public List<EnergyType> AvailableTypes { get; set; }

        /// <summary>
        /// GET requests 
        /// </summary>
        /// <param name="id"></param>
        public IActionResult OnGet(string id)
        {
            Product = ProductService.GetProduct(id);

            // if the product didn't exist
            if (Product == null)
            {
                // throw a 400
                return BadRequest();
            }

            AvailableTypes = Enum.GetValues(typeof(EnergyType)).Cast<EnergyType>().ToList();

            return Page();
        }

        /// <summary>
        /// POST request
        /// Updates product data
        /// </summary>
        /// <returns></returns>
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            ProductService.UpdateData(Product);
            return RedirectToPage("./Index");
        }
    }
}



