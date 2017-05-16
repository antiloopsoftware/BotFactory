using BotFactory.Common.Interface;

namespace BotFactory.Models
{
	public class StatusChangedEventArgs : IStatusChangedEventArgs
    {

        private string _newStatus;

		public string NewStatus
		{
			get
			{
                return _newStatus;
			}
			set
			{
                _newStatus = value;
			}
		}
	}
}
