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
