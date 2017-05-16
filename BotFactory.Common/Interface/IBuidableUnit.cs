using System;
using System.Collections.Generic;
using System.Text;

namespace BotFactory.Common.Interface
{
	public interface IBuidableUnit
	{
		double BuildTime
		{
			get;
			set;
		}

		string Model
		{
			get;
		}
	}
}
