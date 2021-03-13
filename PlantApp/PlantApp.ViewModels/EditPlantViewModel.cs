using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlantApp.ViewModels
{
    public class EditPlantViewModel
    {
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public int WateringPeriod { get; set; }
    }
}