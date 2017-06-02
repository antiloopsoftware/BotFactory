using BotFactory.Common.Tools;
using System.Threading.Tasks;

namespace BotFactory.Common.Interface
{
	public interface IBaseUnit
	{
		Coordinates CurrentPos { get; set; }

        string Name { get; set; }

        Task<bool> MoveAsync(Coordinates currentPos, Coordinates destPos);
	}
}
