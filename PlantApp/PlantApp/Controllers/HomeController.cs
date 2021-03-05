using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlantApp.Models;
using PlantApp.Services;
using PlantApp.ViewModels;
using System;
using PlantApp.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PlantApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PlantService plantService;
        public HomeController(ILogger<HomeController> logger, PlantAppDbContext context)
        {
            _logger = logger;
            plantService = new PlantService(context);
        }

        public IActionResult Index()
        {

            if (!User.Identity.IsAuthenticated) return Redirect("/Identity/Account/Login");
            var plants = plantService.ListAllForWatering(User.Identity.Name);
            Console.WriteLine(plants.Count);
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
