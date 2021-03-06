using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.ViewModels
{
    public class MyPlantsViewModel
    {
        public IEnumerable<PlantCardViewModel> plantCards { get; set; }
    }
}
