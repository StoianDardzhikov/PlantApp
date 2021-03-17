using PlantApp.Data;
using PlantApp.Data.Models;
using System.Linq;

namespace PlantApp.Services.Repositories
{
    public class UserRepository
    {
        private readonly PlantAppDbContext context;

        public UserRepository(PlantAppDbContext context)
        {
            this.context = context;
        }

        public User GetByName(string username)
        {
            return context.Users.FirstOrDefault(p => p.UserName == username);
        }
    }
}
