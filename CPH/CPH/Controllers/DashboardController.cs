/// <summary>
/// To Do:
/// Comment Using Statements.
/// What is the controllers namespace for? 
/// Note above about each page.
/// Async timeouts defined and configured
/// https://stackoverflow.com/questions/4238345/asynchronously-wait-for-taskt-to-complete-with-timeout
/// 
/// </summary>

namespace CPH.Controllers
{
    using CPH.BusinessLogic.Interfaces;
    using CPH.Models.ViewModels;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="DashboardController" />.
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
        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        public async Task<IActionResult> UploadCSVAsync(UploadCSVModel form)
        {
            if (ModelState.IsValid)
            {
                // Get the hash codes of the csv files that are currently in the system directory
                var hashCodes = _csvManagement.GetCsvHashCodes();

                var file = form.File;
                var originalFile = form.OriginalFile;

                // Get the hash code of the csv the user is uploading
                var uploadingCsvHash = _csvManagement.GetFileHash(originalFile);

                // check if there are any matches
                if (hashCodes.Contains(uploadingCsvHash))
                    return View();

                // Check if the year has already been uploaded.
                var check = _csvManagement.CheckIfYearExists(file.FileName);

                // if the year has been uploaded inform the user
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
