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

        [HttpPost]
        public IActionResult Index(string name, int wateringPeriod)
        {
            if (string.IsNullOrEmpty(name)) ModelState.AddModelError(nameof(name), "Името на растението не може да е празно.");
            if (wateringPeriod <= 0) ModelState.AddModelError(nameof(wateringPeriod), "Периода за поливане не може да е по-малък от 1.");

            if (ModelState.IsValid) 
            {
                plantService.Add(name, wateringPeriod, DateTime.Now, User.Identity.Name);
                return RedirectToAction("Index", "MyPlants");
            }
            return View();
        }
    }
}