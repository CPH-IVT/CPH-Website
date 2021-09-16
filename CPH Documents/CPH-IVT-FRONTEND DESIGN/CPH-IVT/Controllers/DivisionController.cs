using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPH_IVT.Services.MongoDB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CPH_IVT.Controllers
{
    public class DivisionController : Controller
    {
        private readonly ICensusDivisionRepository _censusDivisionRepository;
        private readonly ICensusRegionRepository _censusRegionRepository;
        private readonly IHealthIndicatorRepository _indicatorRepository;

        public DivisionController(ICensusDivisionRepository censusDivision, ICensusRegionRepository censusRegion, IHealthIndicatorRepository healthIndicator)
        {
            _censusDivisionRepository = censusDivision;
            _censusRegionRepository = censusRegion;
            _indicatorRepository = healthIndicator;
        }
        public async Task<IActionResult> Index()
        {
            var regions = await _censusRegionRepository.GetAllAsync();
            return View(regions);
        }

        public async Task<IActionResult> Divisions(string id)
        {
            var divisions = await _censusDivisionRepository.GetAllDivisionsByRegionNumberAsync(id);
            return View(divisions);
        }

        public async Task<IActionResult> Indicators(string id)
        {
            var indicators = await _censusDivisionRepository.GetAllHealthIndicatorsByDivisionNumberAsync(id);
            return View(indicators);
        }
    }
}