using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PlantApp.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using PlantApp.Data;
using PlantApp.Services.Factories;

namespace PlantApp.Tests
{
    class PlantFactoryTests
    {
        [TestCase]
        public void PlantGetsCreatedCorrectly()
        {
            var service = new PlantFactory();
            var plant = service.CreateInstance("plant1", 1, new DateTime(2000, 1, 1, 1, 1, 1));
            Assert.AreEqual("plant1", plant.Name);
            Assert.AreEqual(1, plant.WateringPeriod);
            Assert.AreEqual(new DateTime(2000, 1, 1, 1, 1, 1), plant.LastWateredOn);
        }
    }
}
