using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Tools.Interfaces
{
    public interface IWorkingUnit
    {
        bool IsWorking
        {
            get;
            set;
        }

        Coordinates ParkingPos
        {
            get;
            set;
        }

        Task<bool> WorkBeginsAsync();

        Task<bool> WorkEndsAsync();

        Coordinates WorkingPos
        {
            get;
            set;
        }
    }
}
