using System.Collections.Generic;

namespace PlantApp.ViewModels
{
    public class MyPlantsViewModel
    {
        public IEnumerable<MyPlantsPlantCardViewModel> plantCards { get; set; }
        public string SearchText { get; set; }
    }
}
