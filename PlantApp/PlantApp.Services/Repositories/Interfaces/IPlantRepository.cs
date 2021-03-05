using PlantApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.Services.Repositories.Interfaces
{
    public interface IPlantRepository
    {
        void Add(Plant entity);
        void Delete(Plant entity);
        void Update(Plant entity);
        Plant GetById(int id);
        List<Plant> GetAll();
    }
}
