using CPH.Models;
using CPH.Models.ViewModels;
using CPH.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _hostEnv;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment hostEnv)
        {
            _logger = logger;
            _hostEnv = hostEnv;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chart()
        {
            var filePath = _hostEnv.WebRootPath + "\\uploads\\";
            string[] files = Directory.GetFiles(filePath);

            string[] fileNames = new string[files.Length];

            for(var i = 0; i < files.Length; i++)
            {
                fileNames[i] = (Path.GetFileNameWithoutExtension(files[i]));
            }

            ViewData["Files"] = fileNames;

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
                var file = form.File;

                if(file != null && file.Length > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);

                    //get the wwwroot path and append the dir
                    var filePath = _hostEnv.WebRootPath + "\\uploads\\";

                    //create the dir
                    Directory.CreateDirectory(filePath);

                    //create the path for the uploaded file
                    var path = Path.Combine(filePath, fileName);

                    //copy the uploaded file to the dir
                    using (var stream = System.IO.File.Create(path))
                    {
                        await file.CopyToAsync(stream);
                    }

                }
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}
