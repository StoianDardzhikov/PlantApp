﻿using PlantApp.Data.Models;
using PlantApp.Services.Factories;
using PlantApp.Services.Repositories;
using PlantApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlantApp.Data;

namespace PlantApp.Services
{
    public class PlantService : IPlantService
    {
        PlantRepository plantRepository;
        PlantFactory plantFactory;

        UserRepository userRepository;

        public PlantService(PlantAppDbContext context)
        {
            this.plantRepository = new PlantRepository(context);
            this.plantFactory = new PlantFactory();
            this.userRepository = new UserRepository(context);
        }

        public int Add(string name, int wateringPeriod, DateTime lastWateredOn, string username)
        {
            Plant plant = plantFactory.CreateInstance(name, wateringPeriod, lastWateredOn);
            plant.UserId = userRepository.GetByName(username).Id;
            plantRepository.Add(plant);

            return plant.Id;
        }

        public void Delete(int plantId)
        {
            Plant plant = plantRepository.GetById(plantId);
            plantRepository.Delete(plant);
        }

        public void Edit(string name, int wateringPeriod, int plantId)
        {
            Plant plant = plantRepository.GetById(plantId);
            plant.Name = name;
            plant.WateringPeriod = wateringPeriod;
            plantRepository.Update(plant);
        }

        public void SetPlantWatered(int plantId)
        {
            Plant plant = plantRepository.GetById(plantId);
            plant.LastWateredOn = DateTime.UtcNow;
            plantRepository.Update(plant);
        }

        public List<Plant> ListAll(string username)
        {
            User user = userRepository.GetByName(username);
            return user.Plants.ToList();
        }

        public List<Plant> ListAllForWatering(string username)
        {
            List<Plant> plantsToWater = new List<Plant>();
            List<Plant> plants = ListAll(username);

            foreach (var plant in plants)
            {
                TimeSpan interval = new TimeSpan(plant.WateringPeriod, 0, 0, 0);
                DateTime toWater = plant.LastWateredOn + interval;
                TimeSpan leftTime = toWater - DateTime.UtcNow;
                if (leftTime.Hours <= 0 && leftTime.Days <= 0)
                {
                    plantsToWater.Add(plant);
                }
            }
            return plantsToWater;
        }

        public List<Plant> GetPlantsByName(string name, string username)
        {
            return userRepository.GetByName(username).Plants.Where(p => p.Name.StartsWith(name)).ToList();
        }
    }
}