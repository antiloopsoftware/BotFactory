using BotFactory.Common.Interface;

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
