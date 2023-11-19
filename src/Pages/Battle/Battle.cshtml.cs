﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Services;
using ContosoCrafts.WebSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Battle
{
    /// <summary>
    /// Page model for managing Pokémon battles.
    /// </summary>
    public class BattleModel : PageModel
    {
        /// <summary>
        /// Service for handling JSON file-based product data.
        /// </summary>
        public JsonFileProductService ProductService { get; }

        /// <summary>
        /// Collection of Pokémon - General type cards available for battle.
        /// </summary>
        public IEnumerable<ProductModel> PokemonGeneralCards { get; private set; }

        /// <summary>
        /// Array to store the indices of selected Pokémon for battle.
        /// </summary>
        [BindProperty]
        public int[] SelectedPokemon { get; set; }

        /// <summary>
        /// Constructor for the BattleModel class.
        /// </summary>
        /// <param name="productService">The JSON file product service.</param>
        public BattleModel(JsonFileProductService productService)
        {
            ProductService = productService;
        }

        /// <summary>
        /// Handles HTTP GET requests for the battle page.
        /// </summary>
        public void OnGet()
        {
            // Retrieve all products from the service.
            var allProducts = ProductService.GetProducts();

            // Filter and store only Pokémon - General type cards for battle.
            PokemonGeneralCards = allProducts
                .Where(p => p.CardCategory == CardType.Pokemon)
                .ToList();
        }

        /// <summary>
        /// Handles HTTP POST requests when the form is submitted.
        /// </summary>
        /// <returns>Redirects to the BattleResult page if conditions are met; otherwise, redirects to the Error page.</returns>
        public IActionResult OnPost()
        {
            if (SelectedPokemon != null && SelectedPokemon.Length == 5)
            {
                List<string> selectedPokemonNames = new List<string>();

                foreach (var productId in SelectedPokemon)
                {
                    var selectedPokemon = ProductService.GetProduct(productId.ToString());

                    if (selectedPokemon != null)
                    {
                        selectedPokemonNames.Add(selectedPokemon.Name);
                    }
                }

                ViewData["SelectedPokemonNames"] = selectedPokemonNames;

                return RedirectToPage("/BattleResult");
            }
            else
            {
                return RedirectToPage("/Error");
            }
        }
    }
}