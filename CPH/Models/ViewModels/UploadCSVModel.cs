

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
        public string UploadDate { get; set; }
    }
}
