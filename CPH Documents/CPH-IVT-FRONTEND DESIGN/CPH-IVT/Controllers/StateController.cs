using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CPH_IVT.Services.MongoDB.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CPH_IVT.Controllers
{
    public class StateController : Controller
    {
        private readonly IStateRepository _stateRepository;

        public StateController(IStateRepository state)
        {
            _stateRepository = state;
        }
        public async Task<IActionResult> Index()
        {
            var states = await _stateRepository.GetAllAsync();
            return View(states);
        }
    }
}