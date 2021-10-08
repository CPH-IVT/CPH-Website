///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  SOLUTION NAME HERE
//	File Name:         Index.cshtml.cs
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
    /// Defines the <see cref="IndexModel" />.
    /// </summary>
    public partial class IndexModel : PageModel
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
        /// Initializes a new instance of the <see cref="IndexModel"/> class.
        /// </summary>
        /// <param name="userManager">The userManager<see cref="UserManager{IdentityUser}"/>.</param>
        /// <param name="signInManager">The signInManager<see cref="SignInManager{IdentityUser}"/>.</param>
        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the StatusMessage.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Gets or sets the Input.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Defines the <see cref="InputModel" />.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Gets or sets the PhoneNumber.
            /// </summary>
            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        /// <summary>
        /// The LoadAsync.
        /// </summary>
        /// <param name="user">The user<see cref="IdentityUser"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber
            };
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

            await LoadAsync(user);
            return Page();
        }

        /// <summary>
        /// The OnPostAsync.
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
