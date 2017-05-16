using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using System;

namespace BotFactory.Factories
{
    public class FactoryQueueElement : IFactoryQueueElement
    {
        private Type _model;
        private string _name;
        private Coordinates _parkingPos;
        private Coordinates _workingPos;

        public Type Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;

            }
            set
            {
                _name = value;
            }
        }

        public Coordinates ParkingPos
        {
            get
            {
                return _parkingPos;
            }
            set
            {
                _parkingPos = value;
            }
        }

        public Coordinates WorkingPos
        {
            get
            {
                return _workingPos;
            }
            set
            {
                _workingPos = value;
            }
        }

        public FactoryQueueElement(Type model, string name, Coordinates parkingPos, Coordinates workingPos)
        {
            _model = model;

            _name = name;

            _parkingPos = parkingPos;

            _workingPos = workingPos;
        }
    }
}
