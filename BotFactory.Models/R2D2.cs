using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using System;

namespace BotFactory.Models
{
    public class R2D2 : WorkingUnit
    {
        public R2D2() : base(null, "R2D2", 1.5, 5.5)
        {
            WorkingPos = new Coordinates(2.0, 2.0);
        }
    }
}
