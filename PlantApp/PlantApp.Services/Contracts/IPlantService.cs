﻿using PlantApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.Services.Contracts
{
    public interface IPlantService
    {
        int Add(string name, int wateringPeriod, DateTime lastWateredOn, string username);
        void Edit(string name, int wateringPeriod, int plantId);
        void Delete(int plantId);
        void SetPlantWatered(int plantId);
        List<Plant> ListAll(string username);
        List<Plant> ListAllForWatering(string username);
        List<Plant> GetPlantsByName(string name, string username);
        Plant GetById(int plantId);
    }
}