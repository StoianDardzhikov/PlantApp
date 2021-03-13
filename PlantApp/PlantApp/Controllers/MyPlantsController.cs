using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantApp.Data;
using PlantApp.Data.Models;
using PlantApp.Services;
using PlantApp.Services.Contracts;
using PlantApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantApp.Controllers
{
    public class MyPlantsController : Controller
    {
        private readonly IPlantService plantService;
        public MyPlantsController(IPlantService plantService)
        {
            this.plantService = plantService;
        }

        [Authorize]
        public IActionResult Index()
        {
            var plants = plantService.ListAll(User.Identity.Name);
            IEnumerable<MyPlantsPlantCardViewModel> plantCards = plants.Select(x => new MyPlantsPlantCardViewModel() { Id = x.Id, Name = x.Name, WateringPeriod = x.WateringPeriod });
            MyPlantsViewModel mpvw = new MyPlantsViewModel() { plantCards = plantCards };
            return View(mpvw);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            plantService.Delete(id);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Search(string searchText)
        {
            if (searchText == null) return RedirectToAction("Index");
            var plants = plantService.GetPlantsByName(searchText, User.Identity.Name);
            IEnumerable<MyPlantsPlantCardViewModel> plantCards = plants.Select(x => new MyPlantsPlantCardViewModel() { Id = x.Id, Name = x.Name, WateringPeriod = x.WateringPeriod });
            MyPlantsViewModel mpvw = new MyPlantsViewModel() { plantCards = plantCards };
            return View("Index", mpvw);

        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            Plant plant = plantService.GetById(id);
            EditPlantViewModel epvm = new EditPlantViewModel() { PlantId = plant.Id, PlantName = plant.Name, WateringPeriod = plant.WateringPeriod };
            return View("Edit", epvm);
        }

        [Authorize]
        public IActionResult EditPlant(int id, string plantName, int wateringPeriod)
        {
            if (string.IsNullOrEmpty(plantName)) ModelState.AddModelError(nameof(plantName), "Името на растението не може да е празно.");
            if (wateringPeriod <= 0) ModelState.AddModelError(nameof(wateringPeriod), "Периода за поливане не може да е по-малък от 1.");
            if (ModelState.IsValid)
            {
                plantService.Edit(plantName, wateringPeriod, id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", id);
        }
    }
}
