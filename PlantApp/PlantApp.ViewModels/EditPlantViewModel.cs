using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PlantApp.ViewModels
{
    public class EditPlantViewModel
    {
        public int PlantId { get; set; }
        public string Name { get; set; }
        public int WateringPeriod { get; set; }

        public ModelStateDictionary ModelStateErrors { get; set; }
    }
}