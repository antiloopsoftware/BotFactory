using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BotFactory.Common.Interface
{
	public interface IUnitFactory
	{
        bool AddWorkableUnitToQueue(Type model, string Name, Coordinates parkingPos, Coordinates workingPos);

        event FactoryProgress FactoryProgress;

        List<IFactoryQueueElement> Queue { get; set; }

        List<ITestingUnit> Storage { get; set; }

        int QueueFreeSlots { get; set; }

        TimeSpan QueueTime { get; set; }

        int StorageFreeSlots { get; set; }

        int QueueCapacity { get; set; }

        int StorageCapacity { get; set; }
    }
}
