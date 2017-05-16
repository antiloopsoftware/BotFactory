using BotFactory.Common.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFactory.Models
{
    public class HAL : WorkingUnit, ITestingUnit
    {
        string _name;
        double _speed;

        public HAL(string name, double speed = 1) : base(name, speed)
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
