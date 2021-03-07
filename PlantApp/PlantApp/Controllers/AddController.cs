using Microsoft.AspNetCore.Mvc;
using PlantApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantApp.Controllers
{
    public class AddController : Controller
    {
        private readonly IPlantService plantService;

        public AddController(IPlantService plantService)
        {
            this.plantService = plantService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(string name, int wateringPeriod)
        {
            plantService.Add(name, wateringPeriod, DateTime.UtcNow, User.Identity.Name);
            return RedirectToAction("Index", "MyPlants");
        }
    }
}