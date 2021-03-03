using PlantApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantServiceApp.Services.Contracts
{
    public interface IPlantService
    {
        int Add(string name, int wateringPeriod, DateTime lastWateredOn, string userId);
        void Edit(string name, int wateringPeriod, int plantId);
        void Delete(int plantId);
        List<Plant> ListAll(string userId);
        List<Plant> ListAllForWatering(string userId);
    }
}