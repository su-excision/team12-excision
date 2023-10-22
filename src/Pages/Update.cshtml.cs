using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ContosoCrafts.WebSite.Pages
{
    public class UpdateModel : PageModel
    {
        private readonly ILogger<UpdateModel> _logger;

        public UpdateModel(ILogger<UpdateModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
