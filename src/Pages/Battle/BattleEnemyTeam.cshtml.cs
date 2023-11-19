using Microsoft.AspNetCore.Mvc.RazorPages;
using ContosoCrafts.WebSite.Models;
using ContosoCrafts.WebSite.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContosoCrafts.WebSite.Pages.Battle
{
    public class BattleEnemyTeamModel : PageModel
    {
        private readonly JsonFileProductService productService;

        public IEnumerable<ProductModel> EnemyPokemonTeam { get; private set; }

        public BattleEnemyTeamModel(JsonFileProductService productService)
        {
            this.productService = productService;
        }

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