using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPH_IVT.Models;
using CPH_IVT.Services.MongoDB.Repository;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CPH_IVT.Controllers
{
    public class RegionController : Controller
    {
        private ICensusRegionRepository _censusRegionRepository;
  

        public RegionController(ICensusRegionRepository censusRegionRepository)
        {
            _censusRegionRepository = censusRegionRepository;
           
        }
        public async Task<IActionResult> Index()
        {
            var regions = await _censusRegionRepository.GetAllAsync();
            return View(regions);
        }

        public async Task<IActionResult> Indicators(string id)
        {
            var indicators = await _censusRegionRepository.GetAllHealthIndicatorsByRegionNumberAsync(id);
      
            //// TODO: OPTIMIZE THIS 
            //foreach(var indicator in indicators)
            //{
            //    indicatorNames.Add(indicator.Name);
            //}

            return View(indicators);
        }
    }
}