///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  SOLUTION NAME HERE
//	File Name:         SetPassword.cshtml.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            CSCI 2210 - Data Structures	
//	Author:           DESKTOP-FOTV38D\Joshua, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         DESKTOP-FOTV38D\Joshua, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Areas.Identity.Pages.Account.Manage
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="SetPasswordModel" />.
    /// </summary>
    public class SetPasswordModel : PageModel
    {
        /// <summary>
        /// Defines the _userManager.
        /// </summary>
        private readonly UserManager<IdentityUser> _userManager;

        /// <summary>
        /// Defines the _signInManager.
        /// </summary>
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetPasswordModel"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{IdentityUser}"/>.</param>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{IdentityUser}"/>.</param>
        public SetPasswordModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Gets or sets the Input.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Gets or sets the StatusMessage.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Defines the <see cref="InputModel" />.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Gets or sets the NewPassword.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            /// <summary>
            /// Gets or sets the ConfirmPassword.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        /// <summary>
        /// The OnGetAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);

            if (hasPassword)
            {
                return RedirectToPage("./ChangePassword");
            }

            return Page();
        }

        /// <summary>
        /// The OnPostAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your password has been set.";

            return RedirectToPage();
        }
    }
}
