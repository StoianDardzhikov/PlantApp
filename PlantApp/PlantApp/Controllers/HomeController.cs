using Microsoft.AspNetCore.Mvc;
using PlantApp.Models;
using PlantApp.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using PlantApp.Services.Contracts;

namespace PlantApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlantService plantService;
        public HomeController(IPlantService plantService)
        {
            this.plantService = plantService;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return Redirect("/Identity/Account/Login");

            var plants = plantService.GetAllPlantsForWateringOfUser(User.Identity.Name);
            IEnumerable<PlantCardViewModel> plantCards = plants.Select(x => new PlantCardViewModel() { Id = x.Id, Name = x.Name });
            HomeViewModel hvm = new HomeViewModel() { plantCards = plantCards };
            return View(hvm);
        }

        public IActionResult Water(int id)
        {
            plantService.SetPlantWatered(id);
            return RedirectToAction("Index", "Home");
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
