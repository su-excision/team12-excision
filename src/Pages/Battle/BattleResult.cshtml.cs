using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Battle
{
    public class BattleResultModel : PageModel
    {
        public List<string> SelectedPokemonNames { get; set; }
        public string BattleOutcomeMessage { get; set; }

        public void OnGet()
        {
          
        }

        public void OnPost()
        {
            Random random = new Random();
            if (random.Next(2) == 0)
            {
                BattleOutcomeMessage = "Congratulations! You won the battle!";
            }
            else
            {
                BattleOutcomeMessage = "Sorry, you were utterly defeated.";
            }
        }
    }
}