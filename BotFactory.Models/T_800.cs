using BotFactory.Common.Tools;

namespace BotFactory.Models
{
    public class T_800 : WorkingUnit
    {
        public T_800() : base(null, "T_800", 3.0, 10.0)
        {
            WorkingPos = new Coordinates(3.0, 3.5);
        }
    }
}
