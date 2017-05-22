using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace BotFactory.Models
{
    public class Wall_E : WorkingUnit
    {
        public Wall_E() : base(null, "Wall_E", 2.0, 4.0)
        {
            WorkingPos = new Coordinates(5.0, 5.0);
        }
    }
}
