using System;
using System.Collections.Generic;

namespace PlantApp.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<PlantCardViewModel> plantCards { get; set; }
    }
}
