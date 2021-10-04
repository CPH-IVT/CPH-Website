using CPH.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

/// <summary>
/// To Do:
/// Comment Using Statements.
/// What is the conrollers namespace for? 
/// Note above about each page.
/// Async timeouts defined and configured
/// https://stackoverflow.com/questions/4238345/asynchronously-wait-for-taskt-to-complete-with-timeout
/// 
/// </summary>
namespace CPH.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IWebHostEnvironment _hostEnv;
        public DashboardController(ILogger<DashboardController> logger, IWebHostEnvironment hostEnv)
        {
            _logger = logger;
            _hostEnv = hostEnv;
        }
        public IActionResult Home()
        {
            return View();
        }

        public IActionResult Chart()
        {
            return View();
        }

        public IActionResult Account()
        {
            return View();
        }

        public IActionResult UploadCSV()
        {
            return View();
        }

        [HttpPost]
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> UploadCSVAsync(UploadCSVModel form)
        {
            if (ModelState.IsValid)
            {
                // Get the hash codes of the csv files that are currently in the system directory
                var hashCodes = GetCsvHashCodes();

                var file = form.File;
                var originalFile = form.OriginalFile;

                // Get the hash code of the csv the user is uploading
                var uploadingCsvHash = GetFileHash(originalFile);

                // check if there are any matches
                if (hashCodes.Contains(uploadingCsvHash))
                    return View();

                // Check if the year has already been uploaded.
                var check = CheckIfYearExists(file.FileName);

                // if it has inform the user
                if (check)
                    return View();

                // Make sure the original file is not null
                if (file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var originalFileName = Path.GetFileName(originalFile.FileName);

                    //get the wwwroot path and append the dir
                    var filePath = _hostEnv.WebRootPath + "\\uploads\\";

                    var originalCsvFiles = _hostEnv.WebRootPath + @"\uploads\original\";

                    //create the dir
                    Directory.CreateDirectory(filePath);
                    Directory.CreateDirectory(originalCsvFiles);

                    //create the path for the uploaded file
                    var path = Path.Combine(filePath, fileName);
                    var pathToOriginal = Path.Combine(originalCsvFiles, originalFileName);

                    //copy the uploaded file to the dir
                    using (var stream = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(stream);
                    }

                    using (var streamForOriginal = System.IO.File.Create(pathToOriginal))
                    {
                        await originalFile.CopyToAsync(streamForOriginal);
                        var test = streamForOriginal.GetHashCode();
                    }
                }
            }

            return View();
        }

        public async Task<IActionResult> CSVYearDuplicateCheck(IFormFile csvYear)
        {
            var check = CheckIfYearExists(csvYear.FileName);

            return Json(check);
        }

        private bool CheckIfYearExists(string fileName)
        {
            var csvFilesPath = _hostEnv.WebRootPath + @"\uploads\";

            var files = Directory.GetFiles(csvFilesPath);

            foreach(var file in files)
            {
                var testing = Path.GetFileName(file);
                if (testing == fileName)
                    return true;
            }

            return false;
        }

        private int GetFileHash(IFormFile file)
        {
            byte[] hash;
            using ( var stream = file.OpenReadStream())
            {
                hash = MD5.Create().ComputeHash(stream);
                
            }

            return BitConverter.ToInt32(hash);
        }

        /// <summary>
        /// Put this into the CSV Managment class obj
        /// </summary>
        /// <returns></returns>
        private List<int> GetCsvHashCodes()
        {
            //Put the file path in the appsettings and explan what it is for.
            var originalCsvFilesPath = _hostEnv.WebRootPath + @"\uploads\original\";

            string[] files = Directory.GetFiles(originalCsvFilesPath);

            //Change to a dictionary to include file path to hashes. 
            List<int> hashCodes = new List<int>();

            if (files != null)
            {
                foreach(var file in files)
                {
                    using(FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
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
