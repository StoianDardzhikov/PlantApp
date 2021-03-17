using PlantApp.Data.Models;
using PlantApp.Services.Factories;
using PlantApp.Services.Repositories;
using PlantApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using PlantApp.Data;

namespace PlantApp.Services
{
    public class PlantService : IPlantService
    {
        private readonly PlantRepository plantRepository;
        private readonly PlantFactory plantFactory;

        private readonly UserRepository userRepository;

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
            plant.LastWateredOn = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);
            plantRepository.Update(plant);
        }

        public List<Plant> GetAllPlantsOfUser(string username)
        {
            User user = userRepository.GetByName(username);
            return user.Plants.ToList();
        }

        public List<Plant> GetAllPlantsForWateringOfUser(string username)
        {
            List<Plant> plantsToWater = new List<Plant>();
            List<Plant> plants = GetAllPlantsOfUser(username);

            foreach (var plant in plants)
            {
                TimeSpan interval = new TimeSpan(plant.WateringPeriod, 0, 0, 0);
                DateTime toWater = plant.LastWateredOn + interval;
                if (DateTime.UtcNow > toWater)
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

        public Plant GetById(int plantId)
        {
            return plantRepository.GetById(plantId);
        }
    }
}