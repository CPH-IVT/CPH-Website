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
    using CPH.Models.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Collections;
    using System.Collections.Generic;
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
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<DashboardController> _logger;

        /// <summary>
        /// Defines the _hostEnv.
        /// </summary>
        private readonly IWebHostEnvironment _hostEnv;

        /// <summary>
        /// Defines the _csvManagement.
        /// </summary>
        private readonly ICSVManagement _csvManagement;

        /// <summary>
        /// Initializes a new instance of the <see cref="DashboardController"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{DashboardController}"/>.</param>
        /// <param name="hostEnv">The hostEnv<see cref="IWebHostEnvironment"/>.</param>
        /// <param name="csvManagement">The csvManagement<see cref="ICSVManagement"/>.</param>
        public DashboardController(ILogger<DashboardController> logger, IWebHostEnvironment hostEnv, ICSVManagement csvManagement)
        {
            _logger = logger;
            _hostEnv = hostEnv;
            _csvManagement = csvManagement;
        }

        /// <summary>
        /// The Home.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Home()
        {
            return View();
        }

        /// <summary>
        /// The Chart.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Chart()
        {
            return View();
        }

        /// <summary>
        /// The Account.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Account()
        {
            return View();
        }

        /// <summary>
        /// The UploadCSV.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult UploadCSV()
        {
            return View();
        }

        /// <summary>
        /// The UploadCSVAsync.
        /// </summary>
        /// <param name="form">The form<see cref="UploadCSVModel"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        [HttpPost]
        [Route("Dashboard/ValidateAndUploadCSV")]
        public async Task<IActionResult> ValidateAndUploadCSV(UploadCSVModel form)
        {
            if (ModelState.IsValid)
            {
                // Get the hash codes of the csv files that are currently in the system directory
                var hashCodes = _csvManagement.GetCsvHashCodes();



                var file = form.File;
                var originalFile = form.OriginalFile;

                // Make sure the original file is not null
                if (file != null && file.Length > 0 && originalFile != null && originalFile.Length > 0)
                {

                    // Get the hash code of the csv the user is uploading
                    var uploadingCsvHash = _csvManagement.GetFileHashCode(originalFile);

                    Hashtable hashtable = new Hashtable();

                    // check if there are any matches
                    if (hashCodes.Contains(uploadingCsvHash))
                    {

                        hashtable.Add("HashCodeMatch", true);

                        return Json(hashtable);
                    }


                    // Check if the year has already been uploaded.
                    var check = _csvManagement.CheckIfYearExists(file.FileName);

                    // if the year has been uploaded inform the user
                    if (check)
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

                return BadRequest(form);
            }

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Route("Dashboard/OverrideCsvYear")]
        public async Task<IActionResult> OverrideCsvYear(UploadCSVModel form)
        {
            var alteredFile = form.File;
            var originalFile = form.OriginalFile;

            // Make sure the original file is not null
            if (alteredFile != null && alteredFile.Length > 0 && originalFile != null && originalFile.Length > 0)
            {
                //copy the uploaded file to the directory
                await _csvManagement.CopyAlteredCsvToUploadsDirAsync(alteredFile);

                // copy original csv to originals folder
                await _csvManagement.CopyOriginalCsvToOriginalDirAsync(originalFile);

                Hashtable hashtable = new Hashtable();
                hashtable.Add("FileUploaded", true);

                return Json(hashtable);
            }

            return BadRequest(form);
        }

        /// <summary>
        /// The CSVYearDuplicateCheck.
        /// </summary>
        /// <param name="csvYear">The csvYear<see cref="IFormFile"/>.</param>
        /// <returns>The <see cref="Task{IActionResult}"/>.</returns>
        public async Task<IActionResult> CSVYearDuplicateCheck(IFormFile csvYear)
        {
            var check = _csvManagement.CheckIfYearExists(csvYear.FileName);

            return Json(check);
        }
    }
}
