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
        /// Defines the _csvPath.
        /// </summary>
        private string _csvPath;

        /// <summary>
        /// Gets the CSVPath.
        /// </summary>
        public string CSVPath
        {
            get { return _csvPath; }
        }

        /// <summary>
        /// Gets the Folder.
        /// </summary>
        public string Folder
        {
            get { return _folder; }
        }

        /// <summary>
        /// Defines the _folder.
        /// </summary>
        private string _folder;

        /// <summary>
        /// Defines the _originals.
        /// </summary>
        private string _originals;

        /// <summary>
        /// Defines the FileName.
        /// </summary>
        public readonly string FileName;

        /// <summary>
        /// Defines the File.
        /// </summary>
        public readonly IFormFile File;

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

            // 
            BuildCSVUploadFolder();

            // If the CSV directory doesn't exist it will be created here.
            CreateDirectoryForCSV();
        }

        /// <summary>
        /// The UploadCSVAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task UploadCSVAsync()
        {
            try
            {
                BuildCSVUploadFolder();
                CreateDirectoryForCSV();
                BuildCSVUploadPath();
                await CopyCsvToDirectoryAsync();

            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"The file was not found: '{e}'");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine($"The directory was not found: '{e}'");
            }
            catch (IOException e)
            {
                Console.WriteLine($"The file could not be opened: '{e}'");
            }
        }

        /// <summary>
        /// The BuildCSVUploadFolder.
        /// </summary>
        private void BuildCSVUploadFolder()
        {
            _folder = $@"{_hostEnvironment.WebRootPath}{_config.GetSection("CSVFilePaths").GetSection("Copied").Value}";
            _originals = $"{_hostEnvironment.WebRootPath}{_config.GetSection("CSVFilePaths").GetSection("Original").Value}";
        }

        /// <summary>
        /// The CreateDirectoryForCSV.
        /// </summary>
        private void CreateDirectoryForCSV()
        {
            if (Folder != null)
            {
                Directory.CreateDirectory(_folder);
            }
        }

        /// <summary>
        /// The BuildCSVUploadPath.
        /// </summary>
        public void BuildCSVUploadPath()
        {
            if (_folder != null && FileName != null)
            {
                _csvPath = Path.Combine(_folder, FileName);
            }
        }

        /// <summary>
        /// The CopyCsvToDirectoryAsync.
        /// </summary>
        /// <returns>The <see cref="Task"/>.</returns>
        public async Task CopyCsvToDirectoryAsync()
        {
            using (var stream = System.IO.File.Create(_csvPath))
            {
                await File.CopyToAsync(stream);
            }
        }

        /// <summary>
        /// The CheckIfYearExists.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CheckIfYearExists(string fileName)
        {
            var csvFilesPath = _hostEnvironment.WebRootPath + @"\uploads\";

            var files = Directory.GetFiles(csvFilesPath);

            foreach (var file in files)
            {
                var testing = Path.GetFileName(file);
                if (testing == fileName)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// The GetFileHash.
        /// </summary>
        /// <param name="file">The file<see cref="IFormFile"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetFileHash(IFormFile file)
        {
            byte[] hash;
            using (var stream = file.OpenReadStream())
            {
                hash = MD5.Create().ComputeHash(stream);

            }

            return BitConverter.ToInt32(hash);
        }

        /// <summary>
        /// Put this into the CSV Management class obj.
        /// </summary>
        /// <returns>.</returns>
        public List<int> GetCsvHashCodes()
        {
            //Put the file path in the app settings and explain what it is for.
            var originalCsvFilesPath = _hostEnvironment.WebRootPath + @"\uploads\original\";

            string[] files = Directory.GetFiles(originalCsvFilesPath);

            //Change to a dictionary to include file path to hashes. 
            List<int> hashCodes = new List<int>();

            if (files != null)
            {
                foreach (var file in files)
                {
                    using (FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var hash = MD5.Create().ComputeHash(stream);
                        hashCodes.Add(BitConverter.ToInt32(hash));


                    }
                }
            }
            return hashCodes;
        }
    }
}
