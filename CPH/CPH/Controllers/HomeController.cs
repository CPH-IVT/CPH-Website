using CPH.Models;
using CPH.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICSV _csv;

        public HomeController(ILogger<HomeController> logger, ICSV csv)
        {
            _logger = logger;
            _csv = csv;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Chart()
        {
            var getCSVInfo = _csv.ReadAll();

            if(getCSVInfo != null)
            {
                return View(getCSVInfo);
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
