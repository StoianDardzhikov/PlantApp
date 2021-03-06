using Microsoft.AspNetCore.Mvc;
using PlantApp.Data;
using PlantApp.Services;
using PlantApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantApp.Controllers
{
    public class MyPlantsController : Controller
    {
        private readonly PlantService plantService;
        public MyPlantsController( PlantAppDbContext context)
        {
            plantService = new PlantService(context);
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated) return Redirect("/Identity/Account/Login");

            var plants = plantService.ListAll(User.Identity.Name);
            IEnumerable<PlantCardViewModel> plantCards = plants.Select(x => new PlantCardViewModel() { Id = x.Id, Name = x.Name });
            MyPlantsViewModel mpvw = new MyPlantsViewModel() { plantCards = plantCards };
            return View(mpvw);
        }
    }
}
