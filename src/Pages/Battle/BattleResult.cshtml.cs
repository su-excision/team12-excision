using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace ContosoCrafts.WebSite.Pages.Battle
{
    /// <summary>
    /// Page model for displaying the result of a Pokémon battle.
    /// </summary>
    public class BattleResultModel : PageModel
    {
        /// <summary>
        /// List of names of selected Pokémon for the battle.
        /// </summary>
        public List<string> SelectedPokemonNames { get; set; }

        /// <summary>
        /// Message indicating the outcome of the battle.
        /// </summary>
        public string BattleOutcomeMessage { get; set; }

        /// <summary>
        /// Handles HTTP GET requests for the battle result page.
        /// </summary>
        public void OnGet()
        {
          
        }

        /// <summary>
        /// Handles HTTP POST requests when the form is submitted.
        /// </summary>
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