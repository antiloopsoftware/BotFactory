using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BotFactory.Factories;
using BotFactory.Models;
using BotFactory.Common.Tools;
using System.Threading.Tasks;
using BotFactory.Common.Interface;

namespace BotFactory.FactoriesTests
{
    [TestClass]
    public class UnitFactoryTests
    {
        Dictionary<string, int> mockA = new Dictionary<string, int>();
        Dictionary<string, string> mockB = new Dictionary<string, string>();

        UnitFactory unitFactory;

        Coordinates currentPos = new Coordinates(0, 0);
        Coordinates destPos = new Coordinates(1, 1);
        Coordinates parkingPos = new Coordinates(1, 1);


        ConcreteWorkingUnit cwu01;
        ConcreteWorkingUnit cwu02;
        ConcreteWorkingUnit cwu03;
        ConcreteWorkingUnit cwu04;

        [TestInitialize]
        public void InitialisationDesTests()
        {
            mockA.Add("QueueCapacity", 1);
            mockA.Add("StorageCapacity", 1);

            mockB.Add("QueueCapacity", "");

            unitFactory = new UnitFactory(mockA["QueueCapacity"], mockA["StorageCapacity"]);

            currentPos = new Coordinates(4, 4);
            parkingPos = new Coordinates(7, 7);

            cwu01 = new ConcreteWorkingUnit("cwu01", "R2D2", 3.0, 9.0);
            cwu01.CurrentPos = currentPos;
            cwu01.ParkingPos = parkingPos;
            cwu02 = new ConcreteWorkingUnit("cwu02", "HAL", 4.0, 6.0);
            cwu02.CurrentPos = currentPos;
            cwu02.ParkingPos = parkingPos;
            cwu03 = new ConcreteWorkingUnit("cwu03", "T_800", 1.5, 3.0);
            cwu03.CurrentPos = currentPos;
            cwu03.ParkingPos = parkingPos;
            cwu04 = new ConcreteWorkingUnit("cwu04", "T_800", 2.5, 4.0);
            cwu04.CurrentPos = currentPos;
            cwu04.ParkingPos = parkingPos;

            // https://docs.microsoft.com/fr-fr/dotnet/articles/csharp/programming-guide/statements-expressions-operators/anonymous-methods
            unitFactory.FactoryProgress += delegate(object sender, IStatusChangedEventArgs e){ };
        }
        
        [TestMethod]
        public void AddWorkableUnitToQueue_avecMockA1etMockB1_RetourneTrue()
        {
            bool result = unitFactory.AddWorkableUnitToQueue(typeof(R2D2), "R2D2 unit", currentPos, new Coordinates(1, 1));

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WorkingUnit_MoveAsync()
        {
            Coordinates dest = new Coordinates(5, 5);

            await cwu01.MoveAsync(currentPos, dest);

            if (!cwu01.CurrentPos.Equals(dest))
                Assert.Fail();
        }

        [TestMethod]
        public void WorkingUnit_WorkBeginsAsync()
        {
            bool result = Task.Run(async () => await cwu02.WorkBeginsAsync()).GetAwaiter().GetResult();

            if (!cwu02.CurrentPos.Equals(cwu02.WorkingPos))
                 result = false;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void WorkingUnit_WorkEndsAsync()
        {
            var t = Task.Run(async () => await cwu03.WorkEndsAsync()).GetAwaiter().GetResult();

            Assert.IsTrue(cwu03.CurrentPos.Equals(cwu03.ParkingPos));
        }
    }
}
