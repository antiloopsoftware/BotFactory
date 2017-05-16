using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;

namespace BotFactory.Common.Interface
{
	public interface IUnitFactory
	{
        bool AddWorkableUnitToQueue(Type model, string Name, Coordinates parkingPos, Coordinates workingPos);
        
       event EventHandler FactoryStatus;

        List<IFactoryQueueElement> Queue
        {
            get;
        }

        List<ITestingUnit> Storage
        {
            get;
        }

        int QueueFreeSlots
		{
			get;
			set;
		}

		TimeSpan QueueTime
		{
			get;
			set;
		}

		int StorageFreeSlots
		{
			get;
			set;
		}

        int QueueCapacity
        {
            get;
        }

        int StorageCapacity
        {
            get;
        }
    }
}
