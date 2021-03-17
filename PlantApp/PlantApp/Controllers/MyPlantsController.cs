using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantApp.Data.Models;
using PlantApp.Services.Contracts;
using PlantApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

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
            var plants = plantService.GetAllPlantsOfUser(User.Identity.Name);
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
            EditPlantViewModel epvm = new EditPlantViewModel() { PlantId = plant.Id, Name = plant.Name, WateringPeriod = plant.WateringPeriod };
            return View("Edit", epvm);
        }

        [Authorize]
        public IActionResult EditPlant(int id, string name, int wateringPeriod)
        {
            if (string.IsNullOrEmpty(name)) ModelState.AddModelError(nameof(name), "Името на растението не може да е празно.");
            if (name.Length >= 50) ModelState.AddModelError(nameof(name), "Името на растението не може да е повече от 50.");
            if (wateringPeriod <= 0) ModelState.AddModelError(nameof(wateringPeriod), "Периода за поливане не може да е по-малък от 1.");
            if (ModelState.IsValid)
            {
                plantService.Edit(name, wateringPeriod, id);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Edit", id);
        }
    }
}