using BotFactory.Common.Interface;

namespace BotFactory.Models
{
	public class StatusChangedEventArgs : IStatusChangedEventArgs
    {
        public string NewStatus { get; set; }

        public StatusChangedEventArgs(string newStatus)
        {
            NewStatus = newStatus;
        }
	}
}
