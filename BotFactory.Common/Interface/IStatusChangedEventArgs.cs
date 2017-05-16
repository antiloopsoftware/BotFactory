using System;
using System.Collections.Generic;
using System.Text;

namespace BotFactory.Common.Interface
{
	public interface IStatusChangedEventArgs
	{
		string NewStatus
		{
			get;
			set;
		}
	}
}
