///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         CSVManagement.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            Capstone
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.BusinessLogic
{
    using CPH.BusinessLogic.Interfaces;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="CSVManagement" />.
    /// </summary>
    public class CSVManagement : ICSVManagement
    {
        /// <summary>
        /// Gets the _uploadesFoler
        /// Defines the _folder.
        /// </summary>
        public string UploadesFolder
        {
            get
            {
                return $"{_hostEnvironment.WebRootPath}{_config.GetSection("CSVFilePaths").GetSection("Copied").Value}";
            }
        }

        /// <summary>
        /// Gets the path of the original CSV's that have been uploaded.
        /// </summary>
        public string OriginalsFolder
        {
            get
            {


                return $@"{_hostEnvironment.WebRootPath}{_config.GetSection("CSVFilePaths").GetSection("Original").Value}";
            }
        }

        /// <summary>
        /// Defines the _config.
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Defines the _hostEnvironment.
        /// </summary>
        private readonly IWebHostEnvironment _hostEnvironment;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSVManagement"/> class.
        /// </summary>
        /// <param name="hostEnvironment">The hostEnvironment<see cref="IWebHostEnvironment"/>.</param>
        /// <param name="configuration">The configuration<see cref="IConfiguration"/>.</param>
        public CSVManagement(IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {

            _config = configuration;
            _hostEnvironment = hostEnvironment;

            // Creates the directors for the CSV files to be stored if they do not exist.
            CreateCSVUploadFolder();
        }

        /// <summary>
        /// The BuildCSVUploadFolder.
        /// </summary>
        private void CreateCSVUploadFolder()
        {
            if (!Directory.Exists(UploadesFolder))
                Directory.CreateDirectory(UploadesFolder);
                Directory.CreateDirectory(OriginalsFolder);
        }

        /// <summary>
        /// The CopyCsvToDirectoryAsync.
        /// </summary>
        /// <param name="file">The file<see cref="IFormFile"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task CopyAlteredCsvToUploadsDirAsync(IFormFile file)
        {


            using (var stream = System.IO.File.Create($@"{UploadesFolder}\{file.FileName}"))
            {
                await file.CopyToAsync(stream);

            }
            
        }

        /// <summary>
        /// The CopyOriginalCsvToOriginalDirectoryAsync.
        /// </summary>
        /// <param name="file">The file<see cref="IFormFile"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task CopyOriginalCsvToOriginalDirAsync(IFormFile file)
        {

            using (var stream = System.IO.File.Create($@"{OriginalsFolder}\{file.FileName}"))
            {
                await file.CopyToAsync(stream);
                stream.Close();

            }
        }

        

        /// <summary>
        /// The CheckIfYearExists.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CheckIfYearExists(string fileName)
        {

            var files = Directory.GetFiles(UploadesFolder);

            foreach (var file in files)
            {
                var fileInDirName = Path.GetFileName(file);
                if (fileInDirName == fileName)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// The GetFileHash generates the hash code for a single file.
        /// </summary>
        /// <param name="file">The file <see cref="IFormFile"/> .</param>
        /// <returns>The an <see cref="int"/> hash code of the uploaded CSV file.</returns>
        public int GetFileHashCode(IFormFile file)
        {
            byte[] hash;
            using (var stream = file.OpenReadStream())
            {
                hash = MD5.Create().ComputeHash(stream);

            }

            return BitConverter.ToInt32(hash);
        }

        /// <summary>
        /// Generates a <see cref="List{int}"/> of hash code from the files that are located in the original CSV uploads folder (wwwroot\uploads\original).
        /// </summary>
        /// <returns>Returns a <see cref="List{int}"/> of hash codes.</returns>
        public List<int> GetCsvHashCodes()
        {

            // Get the files from the original CSV folder
            string[] files = Directory.GetFiles(OriginalsFolder);

            //Change to a dictionary to include file path to hashes. 
            List<int> hashCodes = new List<int>();

            // Check if there are files in the directory. If there are some there, proceed. 
            if (files != null)
            {
                // For each file in the directory generate the HashCode and add it to the hashCodes list. 
                foreach (var file in files)
                {
                    using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        // Convert the file into a hash code.
                        var hash = MD5.Create().ComputeHash(stream);

                        // Add the hash code to the list.
                        hashCodes.Add(BitConverter.ToInt32(hash));

                    }
                }
            }
            return hashCodes; // return the hash codes
        }
    }
}
