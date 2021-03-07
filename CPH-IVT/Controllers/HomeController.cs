using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CPH_IVT.Models;
using CPH_IVT.Services.MongoDB.Repository;
using System.Threading.Tasks;

namespace CPH_IVT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ICensusRegionRepository _censusRegionRepository;

        public HomeController(ICensusRegionRepository censusRegionRepository)
        {
            _censusRegionRepository = censusRegionRepository;
        }

        public IActionResult Index()
        {
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
