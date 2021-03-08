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
    class PlantRepositoryTests
    {
        [TestCase]
        public void PlantGetAddedToDatabase()
        {
            var mockSet = new Mock<DbSet<Plant>>();

            var mockContext = new Mock<PlantAppDbContext>();
            mockContext.Setup(m => m.Plants).Returns(mockSet.Object);

            var service = new PlantRepository(mockContext.Object);

            var plant = new Plant("plant1", 1, DateTime.UtcNow);
            service.Add(plant);

            mockSet.Verify(m => m.Add(It.IsAny<Plant>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestCase]
        public void PlantGetDeletedFromDatabase()
        {
            var data = new List<Plant>
            {
                new Plant("plant1",1,DateTime.UtcNow),
                new Plant("plant2",2,DateTime.UtcNow),
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Plant>>();
            mockSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockContext = new Mock<PlantAppDbContext>();
            mockContext.Setup(m => m.Plants).Returns(mockSet.Object);

            var service = new PlantRepository(mockContext.Object);
            service.Delete(mockContext.Object.Plants.First());
            mockSet.Verify(m => m.Remove(It.IsAny<Plant>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestCase]
        public void ReturnsCorrectPlantById()
        {
            var data = new List<Plant>
            {
                new Plant("plant1",1,DateTime.UtcNow){Id = 1 },
                new Plant("plant2",2,DateTime.UtcNow){Id = 2 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Plant>>();
            mockSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockContext = new Mock<PlantAppDbContext>();
            mockContext.Setup(m => m.Plants).Returns(mockSet.Object);

            var service = new PlantRepository(mockContext.Object);
            var plant = service.GetById(1);

            Assert.AreEqual("plant1", plant.Name);
        }

        [TestCase]
        public void ReturnsAllPlants()
        {
            var data = new List<Plant>
            {
                new Plant("plant1",1,DateTime.UtcNow){Id = 1 },
                new Plant("plant2",2,DateTime.UtcNow){Id = 2 },
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Plant>>();
            mockSet.As<IQueryable<Plant>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Plant>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Plant>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Plant>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());


            var mockContext = new Mock<PlantAppDbContext>();
            mockContext.Setup(m => m.Plants).Returns(mockSet.Object);
            var service = new PlantRepository(mockContext.Object);
            var plants = service.GetAll();

            Assert.AreEqual("plant1", plants[0].Name);
            Assert.AreEqual("plant2", plants[1].Name);
        }
    }
}
