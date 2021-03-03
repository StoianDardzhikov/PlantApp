using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.Data.Models
{
    public class Plant
    {
        public Plant() { }
        public Plant(string name, int wateringPeriod, DateTime lastWateredOn)
        {
            this.Name = name;
            this.WateringPeriod = wateringPeriod;
            this.LastWateredOn = lastWateredOn;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int WateringPeriod { get; set; }
        public DateTime LastWateredOn { get; set; }
        public string UserId { get; set; }

        public virtual User User { get; set; }
    }
}
