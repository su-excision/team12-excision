using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    public class BattleErrorModel : PageModel
    {
        private readonly ILogger<BattleErrorModel> _logger;

        public BattleErrorModel(ILogger<BattleErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogError("Invalid number of selected Pokémon in the battle.");
        }
    }
}