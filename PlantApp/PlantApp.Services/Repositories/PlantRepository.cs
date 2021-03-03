﻿using PlantApp.Data;
using PlantApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantApp.Services.Repositories
{
    public class PlantRepository : IRepository<Plant>
    {
        PlantAppDbContext context;

        public PlantRepository(PlantAppDbContext context)
        {
            this.context = context;
        }

        public void Add(Plant entity)
        {
            context.Plants.Add(entity);
            context.SaveChanges();
        }

        public void Delete(Plant entity)
        {
            context.Plants.Remove(entity);
            context.SaveChanges();
        }

        public List<Plant> GetAll()
        {
            return context.Plants.ToList();
        }

        public Plant GetById(int id)
        {
            return context.Plants.FirstOrDefault(p => p.Id == id);
        }

        public void Update(Plant entity)
        {
            var item = GetById(entity.Id);
            if (item != null)
                context.Entry(item).CurrentValues.SetValues(entity);

            context.SaveChanges();
        }
    }
}
