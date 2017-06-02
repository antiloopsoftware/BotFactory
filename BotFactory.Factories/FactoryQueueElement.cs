using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using System;

namespace BotFactory.Factories
{
    public class FactoryQueueElement : IFactoryQueueElement
    {
        public Type Model { get; set; }

        public string Name { get; set; }

        public Coordinates ParkingPos { get; set; }

        public Coordinates WorkingPos { get; set; }

        public FactoryQueueElement(Type model, string name, Coordinates parkingPos, Coordinates workingPos)
        {
            Model = model;

            Name = name;

            ParkingPos = parkingPos;

            WorkingPos = workingPos;
        }
    }
}
