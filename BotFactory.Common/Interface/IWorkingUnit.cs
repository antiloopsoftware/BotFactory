using System.Threading.Tasks;

namespace BotFactory.Common.Tools.Interfaces
{
    public interface IWorkingUnit
    {
        bool IsWorking { get; set; }

        Coordinates ParkingPos { get; set; }

        Coordinates WorkingPos { get; set; }

        Task<bool> WorkBeginsAsync();

        Task<bool> WorkEndsAsync();
    }
}
