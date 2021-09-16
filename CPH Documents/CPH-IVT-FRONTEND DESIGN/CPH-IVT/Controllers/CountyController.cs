using CPH_IVT.Services.MongoDB.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CPH_IVT.Controllers
{
    public class CountyController : Controller
    {
        private readonly ICountyRepository _countyRepository;
        private readonly IStateRepository _stateRepository;
        private readonly IHealthIndicatorRepository _indicatorRepository;

        public CountyController(ICountyRepository countyRepository, IStateRepository stateRepository, IHealthIndicatorRepository indicatorRepository)
        {
            _stateRepository = stateRepository;
            _countyRepository = countyRepository;
            _indicatorRepository = indicatorRepository;
        }

        public async Task<IActionResult> Index()
        {
            var states = await _stateRepository.GetAllAsync();
            return View(states);
        }

        public async Task<IActionResult> Counties(string id)
        {
            var counties = await _countyRepository.GetAllCountiesByStateFIPSAsync(id);
            return View(counties);
        }

        public async Task<IActionResult> Indicators(string id)
        {
            var indicators = await _indicatorRepository.GetAllByCountyIdAsync(id);
            return View(indicators);
        }
    }
}