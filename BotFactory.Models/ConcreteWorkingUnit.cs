using BotFactory.Common.Interface;
using BotFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
    public class ConcreteWorkingUnit : WorkingUnit
    {
        public ConcreteWorkingUnit(string name, string model, double speed, double buildTime ) : base(name, model, speed, buildTime)
        {
            UnitStatusChanged += delegate (object sender, IStatusChangedEventArgs e) { };
        }
    }
}
