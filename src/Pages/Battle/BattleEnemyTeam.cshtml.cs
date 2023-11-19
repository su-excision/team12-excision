using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Battle
{
    /// <summary>
    /// Page model for managing the enemy Pokémon team in a battle.
    /// </summary>
    public class BattleEnemyTeamModel : PageModel
    {
        // Service for handling JSON file-based product data.
        private readonly JsonFileProductService productService;

        // Collection of Pokémon representing the enemy team.
        public IEnumerable<ProductModel> EnemyPokemonTeam { get; private set; }

        /// <summary>
        /// Constructor for the BattleEnemyTeamModel class.
        /// </summary>
        public BattleEnemyTeamModel(JsonFileProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Handles HTTP GET requests for the enemy team page.
        /// </summary>
        public void OnGet()
        {
            EnemyPokemonTeam = productService.GetProducts()
                .Where(p => p.CardCategory == CardType.Pokemon)
                .OrderBy(p => Guid.NewGuid())
                .Take(5)
                .ToList();
        }
    }
}