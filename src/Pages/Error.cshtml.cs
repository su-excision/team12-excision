using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace ContosoCrafts.WebSite.Pages
{

    /// <summary>
    /// Page Model for the Error page of the website.
    /// </summary>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {

        /// <summary>
        /// The Id of the current valid Activity. If there is no valid Activity,
        /// this will be set to the trace identifier.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// True if the RequestId has been set. False otherwise.
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        /// <summary>
        /// Logger used for creating logs of site operation.
        /// </summary>
        private readonly ILogger<ErrorModel> _logger;

        /// <summary>
        /// Constructor for the Error Page Model.
        /// </summary>
        /// <param name="logger">the ILogger used for the Error page on creation.</param>
        public ErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Method that is called when the page is loaded (after a GET request). Sets
        /// the value of RequestId on execution.
        /// </summary>
        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}