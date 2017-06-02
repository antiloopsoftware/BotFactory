using BotFactory.Common.Tools.Interfaces;
using System;

namespace BotFactory.Common.Interface
{
	public interface ITestingUnit : IBuidableUnit, IBaseUnit, IWorkingUnit, IFactoryQueueElement
    {
        event EventHandler<EventArgs> UnitStatusChanged;
    }
}
