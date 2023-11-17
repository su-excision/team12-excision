using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{

    /// <summary>
    /// Page Model for the Battle page of the website.
    /// </summary>
    public class BattleModel : PageModel
    {
        /// <summary>
        /// Logger used for creating logs of site operation.
        /// </summary>
        private readonly ILogger<BattleModel> _logger;

        /// <summary>
        /// Constructor for the Battle Page Model.
        /// </summary>
        /// <param name="logger">the ILogger used for the AboutUs page on creation.</param>
        public BattleModel(ILogger<BattleModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method that is called when the page is loaded (after a GET request).
        /// </summary>
        public void OnGet()
        {
        }
    }
}
