using System;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        /// The Error Message that will be communicated to the user.
        /// </summary>
        public string ErrorMessage { get; set; }

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
        public void OnGet(string errorCode)
        {

            switch (errorCode)
            {
                case "400":
                    ErrorMessage = "You have attempted to access data that does not exist or have otherwise caused some upset with our database.";
                    break;
                case "404":
                    ErrorMessage = "You have attempted to navigate to a page of this site that does not exist.";
                    break;
            }

            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}