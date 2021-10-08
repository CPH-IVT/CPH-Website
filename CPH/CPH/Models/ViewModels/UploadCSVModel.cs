///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  SOLUTION NAME HERE
//	File Name:         UploadCSVModel.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            CSCI 2210 - Data Structures	
//	Author:           DESKTOP-FOTV38D\Joshua, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         DESKTOP-FOTV38D\Joshua, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Models.ViewModels
{
    using Microsoft.AspNetCore.Http;
    using System;

    /// <summary>
    /// Defines the <see cref="UploadCSVModel" />.
    /// </summary>
    public class UploadCSVModel
    {
        /// <summary>
        /// Gets or sets the UserIdentity.
        /// </summary>
        public string UserIdentity { get; set; }

        /// <summary>
        /// Gets or sets the File.
        /// </summary>
        public IFormFile File { get; set; }

        /// <summary>
        /// Gets or sets the OriginalFile.
        /// </summary>
        public IFormFile OriginalFile { get; set; }

        /// <summary>
        /// Gets or sets the UploadeDate.
        /// </summary>
        public DateTime UploadeDate { get; set; }
    }
}
