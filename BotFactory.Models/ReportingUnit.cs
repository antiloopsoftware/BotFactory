using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using System;

namespace BotFactory.Models
{
	public abstract class ReportingUnit : BuildableUnit
	{
        //public event EventHandler<IStatusChangedEventArgs> UnitStatusChanged;

        //or

        // need of public delegate void UnitStatusChanged(object sender, IStatusChangedEventArgs e); in BotFactory.Common.Tools
        public event UnitStatusChanged UnitStatusChanged;

        /// <summary>
        /// see http://stackoverflow.com/questions/31787748/call-an-event-from-a-base-class (is impossible)
        /// </summary>
        /// <param name="statusChangedEventArgs"></param>

        public void OnStatusChanged(object sender, IStatusChangedEventArgs statusChangedEventArgs)
        {
            // https://codeblog.jonskeet.uk/2015/01/30/clean-event-handlers-invocation-with-c-6/

            //UnitStatusChanged?.Invoke(sender, statusChangedEventArgs);

            // ou 

            UnitStatusChanged(sender, statusChangedEventArgs);
        }

        public ReportingUnit(string model = "Sans nom", double built_time = 5) : base(model, built_time)
        {
            Model = model;
            BuildTime = built_time;
        }
    }
}
