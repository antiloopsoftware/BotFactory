using BotFactory.Common.Interface;
using System;

namespace BotFactory.Models
{
	public abstract class ReportingUnit : BuildableUnit
	{
		public event EventHandler<IStatusChangedEventArgs> UnitStatusChanged;
        public event EventHandler FactoryStatus;

        /// <summary>
        /// see http://stackoverflow.com/questions/31787748/call-an-event-from-a-base-class (is impossible)
        /// </summary>
        /// <param name="statusChangedEventArgs"></param>

        public void OnStatusChanged(IStatusChangedEventArgs statusChangedEventArgs)
        {
            // https://codeblog.jonskeet.uk/2015/01/30/clean-event-handlers-invocation-with-c-6/

            UnitStatusChanged?.Invoke(this, (statusChangedEventArgs));
        }

        public void OnFactoryChanged(EventArgs eventArgs)
        {
            // https://codeblog.jonskeet.uk/2015/01/30/clean-event-handlers-invocation-with-c-6/
           
            FactoryStatus?.Invoke(this, (EventArgs) eventArgs);
        }
    }
}
