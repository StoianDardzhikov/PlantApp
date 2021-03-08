using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PlantApp.Data;
using PlantApp.Data.Models;
using PlantApp.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantApp.Tests
{
    class UserRepositoryTests
    {
        [TestCase]
        public void ReturnsCorrectUserByName()
        {
            var data = new List<User>
            {
                new User{UserName = "stoian" },
                new User{UserName = "plamen"},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockContext = new Mock<PlantAppDbContext>();
            mockContext.Setup(m => m.Users).Returns(mockSet.Object);
            var service = new UserRepository(mockContext.Object);
            var user = service.GetByName("stoian");

            Assert.AreEqual("stoian", user.UserName);
        }
    }
}
