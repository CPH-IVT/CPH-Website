///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         DashboardController.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            Capstone
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/15/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Controllers
{
    using CPH.BusinessLogic.Interfaces;
    using CPH.Models;
    using CPH.Models.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// To Do:
    /// Comment Using Statements.
    /// What is the controllers namespace for? 
    /// Note above about each page.
    /// Async timeouts defined and configured
    /// https://stackoverflow.com/questions/4238345/asynchronously-wait-for-taskt-to-complete-with-timeout
    ///.
    /// </summary>
    [Authorize]
    public class DashboardController : Controller
    {

        /// <summary>
        /// References the _hostEnv..
        /// </summary>
        private readonly IWebHostEnvironment _hostEnv;

        /// <summary>
        /// References the _csvManagement..
        /// </summary>
        private readonly ICSVManagement _csvManagement;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{DashboardController}"/>.</param>
        /// <param name="hostEnv">The hostEnv<see cref="IWebHostEnvironment"/>.</param>
        /// <param name="csvManagement">The csvManagement<see cref="ICSVManagement"/>.</param>
        public DashboardController(IWebHostEnvironment hostEnv, ICSVManagement csvManagement)
        {
           
            _hostEnv = hostEnv;
            _csvManagement = csvManagement;
        }

        /// <summary>
        /// The Home.
        /// NEED TO DEFINE.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// The Chart.
        /// NEEDS TO BE DEFINED.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult CreateChart()
        {
            var filePath = _csvManagement.UploadsFolder;
            string[] files = Directory.GetFiles(filePath);

            string[] fileNames = new string[files.Length];

            for (var i = 0; i < files.Length; i++)
            {
                fileNames[i] = (Path.GetFileNameWithoutExtension(files[i]));
            }

            ViewData["Files"] = fileNames;
            return View();
        }


        /// <summary>
        /// The UploadCSV.
        /// Displays page for managing downloads of CHR data, i.e., user uploads.
        /// Note: In absence of [Route], function name must mirror file name.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult UploadCSV()
        {
            return View();
        }

        /// <summary>
        /// The ValidateAndUploadCSV.
        /// Retrieves CHR data checking its integrity. 
        /// Note: Logic should be cleaned up and refactored.
        /// </summary>
        /// <param name="form">The form<see cref="UploadCSVModel"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPost, Route("Dashboard/ValidateAndUploadCSV")]
        public async Task<IActionResult> ValidateAndUploadCSV(UploadCSVModel form)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            // Get the hash codes of the csv files that are currently in the system directory
            var hashCodes = _csvManagement.GetCsvHashCodes();

            var file = form.AlteredFile;
            var originalFile = form.OriginalFile;

            if (file == null || file.Length == 0 || originalFile == null || originalFile.Length == 0)
                return BadRequest(form);

            // Get the hash code of the csv the user is uploading
            var uploadingCsvHash = _csvManagement.GetFileHashCode(originalFile);

            Hashtable hashtable = new Hashtable();

            // check if there are any matches
            if (hashCodes.Contains(uploadingCsvHash))
            {
                hashtable.Add("HashCodeMatch", true);
                return Json(hashtable);
            }

            // if the year has been uploaded inform the user
            if (_csvManagement.CheckIfYearExists(file.FileName))
            {
                hashtable.Add("FileYearMatch", true);
                return Json(hashtable);
            }

            //copy the uploaded file to the directory
            await _csvManagement.CopyAlteredCsvToUploadsDirAsync(file);

            // copy original csv to originals folder
            await _csvManagement.CopyOriginalCsvToOriginalDirAsync(originalFile);

            hashtable.Add("UploadSuccessful", true);
            return Json(hashtable);
        }

        /// <summary>
        /// The OverrideCsvYear.
        /// Give user the option to overwrite CHR data for specified year. 
        /// </summary>
        /// <param name="form">The form<see cref="UploadCSVModel"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPost, Route("Dashboard/OverrideCsvYear")]
        public async Task<IActionResult> OverrideCsvYear(UploadCSVModel form)
        {
            var alteredFile = form.AlteredFile;
            var originalFile = form.OriginalFile;

            if (alteredFile == null || alteredFile.Length == 0 || originalFile == null || originalFile.Length == 0)
                return BadRequest(form);


            //copy the uploaded file to the directory
            await _csvManagement.CopyAlteredCsvToUploadsDirAsync(alteredFile);

            // copy original csv to originals folder
            await _csvManagement.CopyOriginalCsvToOriginalDirAsync(originalFile);

            Hashtable hashtable = new Hashtable();
            hashtable.Add("FileUploaded", true);

            return Json(hashtable);
        }

        /// <summary>
        /// The CSVYearDuplicateCheck.
        /// </summary>
        /// <param name="csvYear">The csvYear<see cref="IFormFile"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> CSVYearDuplicateCheck(IFormFile csvYear)
        {
            return Json(_csvManagement.CheckIfYearExists(csvYear.FileName));
        }

        public IActionResult CreateRegion()
        {
            var filePath = _csvManagement.UploadsFolder;
            string[] files = Directory.GetFiles(filePath);

            string[] fileNames = new string[files.Length];

            for (var i = 0; i < files.Length; i++)
            {
                fileNames[i] = (Path.GetFileNameWithoutExtension(files[i]));
            }

            ViewData["Files"] = fileNames;
            return View();
        }


        /// <summary>
        /// The Error.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
