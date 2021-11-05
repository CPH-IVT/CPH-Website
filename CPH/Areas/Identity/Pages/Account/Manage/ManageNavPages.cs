///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  SOLUTION NAME HERE
//	File Name:         ManageNavPages.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            CSCI 2210 - Data Structures	
//	Author:           DESKTOP-FOTV38D\Joshua, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         DESKTOP-FOTV38D\Joshua, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Areas.Identity.Pages.Account.Manage
{
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System;

    /// <summary>
    /// Defines the <see cref="ManageNavPages" />.
    /// </summary>
    public static class ManageNavPages
    {
        /// <summary>
        /// Gets the Index.
        /// </summary>
        public static string Index => "Index";

        /// <summary>
        /// Gets the Email.
        /// </summary>
        public static string Email => "Email";

        /// <summary>
        /// Gets the ChangePassword.
        /// </summary>
        public static string ChangePassword => "ChangePassword";

        /// <summary>
        /// Gets the DownloadPersonalData.
        /// </summary>
        public static string DownloadPersonalData => "DownloadPersonalData";

        /// <summary>
        /// Gets the DeletePersonalData.
        /// </summary>
        public static string DeletePersonalData => "DeletePersonalData";

        /// <summary>
        /// Gets the ExternalLogins.
        /// </summary>
        public static string ExternalLogins => "ExternalLogins";

        /// <summary>
        /// Gets the PersonalData.
        /// </summary>
        public static string PersonalData => "PersonalData";

        /// <summary>
        /// Gets the TwoFactorAuthentication.
        /// </summary>
        public static string TwoFactorAuthentication => "TwoFactorAuthentication";

        /// <summary>
        /// The IndexNavClass.
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        /// <summary>
        /// The EmailNavClass.
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

        /// <summary>
        /// The ChangePasswordNavClass.
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        /// <summary>
        /// The DownloadPersonalDataNavClass.
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string DownloadPersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DownloadPersonalData);

        /// <summary>
        /// The DeletePersonalDataNavClass.
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string DeletePersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, DeletePersonalData);

        /// <summary>
        /// The ExternalLoginsNavClass.
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ExternalLoginsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ExternalLogins);

        /// <summary>
        /// The PersonalDataNavClass.
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string PersonalDataNavClass(ViewContext viewContext) => PageNavClass(viewContext, PersonalData);

        /// <summary>
        /// The TwoFactorAuthenticationNavClass.
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string TwoFactorAuthenticationNavClass(ViewContext viewContext) => PageNavClass(viewContext, TwoFactorAuthentication);

        /// <summary>
        /// The PageNavClass.
        /// </summary>
        /// <param name="viewContext">The viewContext<see cref="ViewContext"/>.</param>
        /// <param name="page">The page<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
