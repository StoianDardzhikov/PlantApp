using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlantApp.ViewModels
{
    public class AddViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int WateringPeriod { get; set; }
    }
}
