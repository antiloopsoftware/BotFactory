using BotFactory.Common.Tools;
using BotFactory.Factories;
using BotFactory.Models;
using System.Collections.Generic;

namespace BotFactory.Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> mockA = new Dictionary<string, int>();
            Dictionary<string, string> mockB = new Dictionary<string, string>();

            mockA.Add("QueueCapacity", 100);
            mockA.Add("StorageCapacity", 100);

            /* mockA.Add("QueueCapacity", 1);
             mockA.Add("StorageCapacity", 1);*/

            mockB.Add("QueueCapacity", "");

            UnitFactory unitFactory = new UnitFactory(mockA["QueueCapacity"], mockA["StorageCapacity"]);

           // unitFactory.CreateWorkableUnitToQueue();

            unitFactory.AddWorkableUnitToQueue(typeof(R2D2), typeof(R2D2).Name + "-" + 1, new Coordinates(1, 2), new Coordinates(3, 4));

            unitFactory.AddWorkableUnitToQueue(typeof(T_800), typeof(T_800).Name + "-" + 2, new Coordinates(5, 6), new Coordinates(7, 8));

        }
    }
}
