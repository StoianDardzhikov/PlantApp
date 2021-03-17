using NUnit.Framework;
using System;
using PlantApp.Services.Factories;

namespace PlantApp.Tests
{
    class PlantFactoryTests
    {
        [TestCase]
        public void CreateInstance_WhenCalled_CreatesAndReturnsPlantCorrectly()
        {
            var service = new PlantFactory();
            var plant = service.CreateInstance("plant1", 1, new DateTime(2000, 1, 1, 1, 1, 1));
            Assert.AreEqual("plant1", plant.Name);
            Assert.AreEqual(1, plant.WateringPeriod);
            Assert.AreEqual(new DateTime(2000, 1, 1, 1, 1, 1), plant.LastWateredOn);
        }
    }
}
