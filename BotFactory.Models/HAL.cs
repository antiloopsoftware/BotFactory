using BotFactory.Common.Tools;

namespace BotFactory.Models
{
    public class HAL : WorkingUnit
    {
        public HAL() : base(null, "HAL", 0.5, 7.0)
        {
            WorkingPos = new Coordinates(1.0, 1.0);
        }
    }
}
