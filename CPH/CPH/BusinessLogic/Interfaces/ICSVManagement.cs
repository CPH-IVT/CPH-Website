///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         ICSVManagement.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            Capstone
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.BusinessLogic.Interfaces
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="ICSVManagement" />.
    /// </summary>
    public interface ICSVManagement
    {
        /// <summary>
        /// The UploadCSVAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        Task UploadCSVAsync();

        /// <summary>
        /// The CopyCsvToDirectoryAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        Task CopyCsvToDirectoryAsync();

        /// <summary>
        /// The CheckIfYearExists.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool CheckIfYearExists(string fileName);

        /// <summary>
        /// The GetFileHash.
        /// </summary>
        /// <param name="file">The file<see cref="IFormFile"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        int GetFileHash(IFormFile file);

        /// <summary>
        /// The GetCsvHashCodes.
        /// </summary>
        /// <returns>The <see cref="List{int}"/>.</returns>
        List<int> GetCsvHashCodes();
    }
}
