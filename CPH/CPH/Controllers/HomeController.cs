///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Solution/Project:  College of Public Health (CPH) Capstone
//	File Name:         HomeController.cs
//	Description:       YOUR DESCRIPTION HERE
//	Course:            Capstone
//	Author:            Joshua Trimm, trimmj@etsu.edu
//	Created:           10/7/2021
//	Copyright:         Joshua Trimm, 2021
//
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

namespace CPH.Controllers
{
    using CPH.Models;
    using CPH.Models.ViewModels;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="HomeController" />.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Defines the _logger.
        /// </summary>
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Defines the _hostEnv.
        /// </summary>
        private readonly IWebHostEnvironment _hostEnv;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="logger">The logger<see cref="ILogger{HomeController}"/>.</param>
        /// <param name="hostEnv">The hostEnv<see cref="IWebHostEnvironment"/>.</param>
        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnv)
        {
            _logger = logger;
            _hostEnv = hostEnv;
        }

        /// <summary>
        /// The Index.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// The Chart.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Chart()
        {
            var filePath = _hostEnv.WebRootPath + "\\uploads\\";
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
        /// The Regions.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Regions()
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
                var file = form.File;
                var originalFile = form.OriginalFile;

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
                    }
                }
            }

            return View();
        }

        /// <summary>
        /// The Privacy.
        /// </summary>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        public IActionResult Privacy()
        {
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
