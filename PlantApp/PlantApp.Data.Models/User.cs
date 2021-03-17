using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PlantApp.Data.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Plant> Plants { get; set; }
    }
}