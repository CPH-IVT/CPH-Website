///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  SOLUTION NAME HERE
//	File Name:         Logout.cshtml.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            CSCI 2210 - Data Structures	
//	Author:           DESKTOP-FOTV38D\Joshua, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         DESKTOP-FOTV38D\Joshua, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Areas.Identity.Pages.Account
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Logging;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="LogoutModel" />.
    /// </summary>
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        /// <summary>
        /// Defines the _signInManager.
        /// </summary>
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<LogoutModel> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogoutModel"/> class.
        /// </summary>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{IdentityUser}"/>.</param>
        /// <param name="logger">The logger<see cref="ILogger{LogoutModel}"/>.</param>
        public LogoutModel(SignInManager<IdentityUser> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// The OnGet.
        /// </summary>
        public void OnGet()
        {
        }

        /// <summary>
        /// The OnPost.
        /// </summary>
        /// <param name="returnUrl">The returnUrl<see cref="string"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}
