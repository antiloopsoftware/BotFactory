using BotFactory.Common.Interface;
using System;

namespace BotFactory.Models
{
    public class T_800 : WorkingUnit, ITestingUnit
    {
        string _name;
        double _speed;

        public T_800(string name, double speed = 1) : base(name, speed)
        {
            _name = name;
            _speed = speed;
        }

        Type IFactoryQueueElement.Model
        {
            get;
            set;
        }

        event EventHandler<EventArgs> ITestingUnit.UnitStatusChanged
        {
            add
            {
                
            }

            remove
            {
               
            }
        }
    }
}
