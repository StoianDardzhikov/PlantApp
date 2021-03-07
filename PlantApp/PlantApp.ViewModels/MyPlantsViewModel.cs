using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.ViewModels
{
    public class MyPlantsViewModel
    {
        public IEnumerable<MyPlantsPlantCardViewModel> plantCards { get; set; }
        public string SearchText { get; set; }
    }
}
