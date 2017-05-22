using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Interface
{
	public interface IBaseUnit
	{
		Coordinates CurrentPos { get; set; }

		Task<bool> MoveAsync(Coordinates currentPos, Coordinates destPos);

		string Name	{ get; set; }
	}
}
