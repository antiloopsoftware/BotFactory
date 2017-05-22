using BotFactory.Common.Interface;

namespace BotFactory.Models
{
	public class StatusChangedEventArgs : IStatusChangedEventArgs
    {
        public StatusChangedEventArgs(string newStatus)
        {
            NewStatus = newStatus;
        }

        public string NewStatus	{ get; set; }
	}
}
