using PlantApp.Data;
using PlantApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantApp.Services.Repositories
{
    public class UserRepository
    {
        PlantAppDbContext context;

        public UserRepository(PlantAppDbContext context)
        {
            this.context = context;
        }

        public User GetById(string id)
        {
            return context.Users.FirstOrDefault(p => p.Id == id);
        }
    }
}
