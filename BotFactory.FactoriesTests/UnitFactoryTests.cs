using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BotFactory.Factories;
using BotFactory.Models;
using BotFactory.Common.Tools;

namespace BotFactory.FactoriesTests
{
    [TestClass]
    public class UnitFactoryTests
    {
        Dictionary<string, int> mockA = new Dictionary<string, int>();
        Dictionary<string, string> mockB = new Dictionary<string, string>();

        UnitFactory unitFactory;

        [TestInitialize]
        public void InitialisationDesTests()
        {
            mockA.Add("QueueCapacity", 1);
            mockA.Add("StorageCapacity", 1);

           /* mockA.Add("QueueCapacity", 1);
            mockA.Add("StorageCapacity", 1);*/


            mockB.Add("QueueCapacity", "");

            unitFactory = new UnitFactory(mockA["QueueCapacity"], mockA["StorageCapacity"]);
        }
        
        [TestMethod]
        public void AddWorkableUnitToQueue_avecMockA1etMockB1_RetourneTrue()
        {
            bool result = unitFactory.AddWorkableUnitToQueue(typeof(R2D2), "TotoR2D2", null, null);

            Assert.IsTrue(result);
        }
    }
}
