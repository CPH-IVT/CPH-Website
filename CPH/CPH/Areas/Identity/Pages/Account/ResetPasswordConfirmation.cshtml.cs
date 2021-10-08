///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  SOLUTION NAME HERE
//	File Name:         ResetPasswordConfirmation.cshtml.cs
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
    using Microsoft.AspNetCore.Mvc.RazorPages;

    /// <summary>
    /// Defines the <see cref="ResetPasswordConfirmationModel" />.
    /// </summary>
    [AllowAnonymous]
    public class ResetPasswordConfirmationModel : PageModel
    {
        /// <summary>
        /// The OnGet.
        /// </summary>
        public void OnGet()
        {
        }
    }
}
