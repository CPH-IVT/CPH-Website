using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPH_IVT.Models;
using CPH_IVT.Services.MongoDB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CPH_IVT.Controllers
{
    public class IndicatorController : Controller
    {
        private IHealthIndicatorRepository _healthIndicatorRepository;

        public IndicatorController(IHealthIndicatorRepository healthIndicatorRepository)
        {
            _healthIndicatorRepository = healthIndicatorRepository;
        }

        public async Task<IActionResult> Index(string id)
        {
            var indicator = await _healthIndicatorRepository.GetAllAsync();
            return View(indicator);
        }

        //public async Task<IActionResult> CountyIndicators(County county, string stateFIPS)
        //{
        //    HashSet<string> names = new HashSet<string>();

        //    var indicators = await _healthIndicatorRepository.GetAllByCounty(county, stateFIPS);
        //    foreach (var indicator in indicators)
        //        names.Add(indicator.Name);
        //    return View(indicators);
        //}
    }
}