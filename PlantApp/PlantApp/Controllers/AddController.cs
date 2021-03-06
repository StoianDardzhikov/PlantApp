using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantApp.Controllers
{
    public class AddController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
