using PlantApp.Data.Models;
using System;

namespace PlantApp.Services.Factories
{
    public class PlantFactory : IPlantFactory
    {
        public Plant CreateInstance(string name, int wateringPeriod, DateTime lastWateredOn)
        {
            return new Plant(name, wateringPeriod, lastWateredOn);
        }
    }
}
