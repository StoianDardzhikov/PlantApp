using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using PlantApp.Data;
using PlantApp.Data.Models;
using PlantApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantApp.Tests
{
    class PlantServiceTests
    {
        [TestCase]
        public void ListAll_WhenCalled_ReturnsAllPlants()
        {
            var plantData = new List<Plant>
            {
                new Plant("plant1",1,DateTime.UtcNow){Id = 1, UserId = "1" },
                new Plant("plant2",2,DateTime.UtcNow){Id = 2, UserId = "1" },
            }.AsQueryable();

            var userData = new List<User>
            {
                new User{Id = "1", UserName = "stoian", Plants = new List<Plant>{
                    plantData.ToList()[0],
                    plantData.ToList()[1],
                } },
            }.AsQueryable();

            var userMockSet = new Mock<DbSet<User>>();
            userMockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            userMockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            userMockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            userMockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

            var plantMockSet = new Mock<DbSet<Plant>>();
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(plantData.GetEnumerator());

            var mockContext = new Mock<PlantAppDbContext>();
            mockContext.Setup(m => m.Plants).Returns(plantMockSet.Object);
            mockContext.Setup(m => m.Users).Returns(userMockSet.Object);
            var service = new PlantService(mockContext.Object);
            var plants = service.ListAll("stoian");
            Assert.AreEqual("plant1", plants[0].Name);
            Assert.AreEqual("plant2", plants[1].Name);
        }

        [TestCase]
        public void ListAllForWatering_WhenCalled_ReturnsAllPlantsForWatering()
        {
            var plantData = new List<Plant>
            {
                new Plant("plant1",1,new DateTime(2000,1,1,1,1,1)){Id = 1, UserId = "1" },
                new Plant("plant2",2,DateTime.UtcNow){Id = 2, UserId = "1" },
                new Plant("plant3",3,new DateTime(2021,1,1,1,1,1)){Id = 3, UserId = "1" },
            }.AsQueryable();

            var userData = new List<User>
            {
                new User{Id = "1", UserName = "stoian", Plants = new List<Plant>{
                    plantData.ToList()[0],
                    plantData.ToList()[1],
                    plantData.ToList()[2],
                } },
            }.AsQueryable();

            var userMockSet = new Mock<DbSet<User>>();
            userMockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            userMockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            userMockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            userMockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

            var plantMockSet = new Mock<DbSet<Plant>>();
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(plantData.GetEnumerator());

            var mockContext = new Mock<PlantAppDbContext>();
            mockContext.Setup(m => m.Plants).Returns(plantMockSet.Object);
            mockContext.Setup(m => m.Users).Returns(userMockSet.Object);
            var service = new PlantService(mockContext.Object);
            var plants = service.ListAllForWatering("stoian");
            Assert.AreEqual("plant1", plants[0].Name);
            Assert.AreEqual("plant3", plants[1].Name);
        }

        [TestCase]
        public void GetPlantsByName_WhenCalled_ReturnsAllPlantsStartingWithThisName()
        {
            var plantData = new List<Plant>
            {
                new Plant("plant1",1,DateTime.UtcNow){Id = 1, UserId = "1" },
                new Plant("plant2",2,DateTime.UtcNow){Id = 2, UserId = "1" },
                new Plant("aasdas",4,DateTime.UtcNow){Id = 3, UserId = "1" },
                new Plant("plant3",3,DateTime.UtcNow){Id = 4, UserId = "1" },
            }.AsQueryable();

            var userData = new List<User>
            {
                new User{Id = "1", UserName = "stoian", Plants = new List<Plant>{
                    plantData.ToList()[0],
                    plantData.ToList()[1],
                    plantData.ToList()[2],
                    plantData.ToList()[3],
                } },
            }.AsQueryable();

            var userMockSet = new Mock<DbSet<User>>();
            userMockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(userData.Provider);
            userMockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(userData.Expression);
            userMockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(userData.ElementType);
            userMockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(userData.GetEnumerator());

            var plantMockSet = new Mock<DbSet<Plant>>();
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(plantData.Provider);
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(plantData.Expression);
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(plantData.ElementType);
            plantMockSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(plantData.GetEnumerator());

            var mockContext = new Mock<PlantAppDbContext>();
            mockContext.Setup(m => m.Plants).Returns(plantMockSet.Object);
            mockContext.Setup(m => m.Users).Returns(userMockSet.Object);
            var service = new PlantService(mockContext.Object);
            var plants = service.GetPlantsByName("plant", "stoian");
            Assert.AreEqual("plant1", plants[0].Name);
            Assert.AreEqual("plant2", plants[1].Name);
            Assert.AreEqual("plant3", plants[2].Name);
        }
    }
}
