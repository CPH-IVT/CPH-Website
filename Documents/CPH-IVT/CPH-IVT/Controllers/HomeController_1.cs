using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CPH_IVT.Models;
using CPH_IVT.Services.MongoDB;
using MongoDB.Driver;
using CPH_IVT.Services.MongoDB.Repository;

namespace CPH_IVT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHealthIndicatorRepository _healthIndicatorRepository;

        public HomeController(ILogger<HomeController> logger, IHealthIndicatorRepository healthIndicatorRepository)
        {
            _logger = logger;
            _healthIndicatorRepository = healthIndicatorRepository;
        }

        public async Task<IActionResult> Index()
        {
            await _healthIndicatorRepository.CreateHealthIndicator(new HealthIndicator() { Name = "No changes detected", Year = "2020" });
            var x = await _healthIndicatorRepository.GetAllHealthIndicators();
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
