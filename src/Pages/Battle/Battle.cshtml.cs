using Microsoft.AspNetCore.Mvc;
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
            SelectedPokemon = new int[0];

            var allProducts = ProductService.GetProducts();

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
            var selectedPokemonValues = Request.Form["SelectedPokemon"];

            if (selectedPokemonValues.Count == 5)
            {
                List<string> selectedPokemonNames = new List<string>();

                foreach (var productId in selectedPokemonValues)
                {
                    var selectedPokemon = ProductService.GetProduct(productId);

                    if (selectedPokemon != null)
                    {
                        selectedPokemonNames.Add(selectedPokemon.Name);
                    }
                }

                ViewData["SelectedPokemonNames"] = selectedPokemonNames;

                return RedirectToPage("/Battle/BattleEnemyTeam");
            }
            else
            {
                return RedirectToPage("/Battle/BattleError");
            }
        }
    }
}
