using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using BotFactory.Common.Tools.Interfaces;
using System.Threading.Tasks;
using System;

namespace BotFactory.Models
{
	public abstract class WorkingUnit : BaseUnit, ITestingUnit
	{
        public WorkingUnit(string name = "Sans nom", string model = "Sans nom", double speed = 5.0, double buildTime = 5.0) : base(name, model, speed, buildTime)
        {
            ParkingPos = new Coordinates(0, 0);
            WorkingPos = new Coordinates(0, 0);
            IsWorking = false;
        }

        event EventHandler<EventArgs> ITestingUnit.UnitStatusChanged
        {
            add
            {
                //throw new NotImplementedException();
            }

            remove
            {
                //throw new NotImplementedException();
            }
        }

        public bool IsWorking { get; set; }

		public Coordinates ParkingPos { get; set; }

        /// <summary>
        /// see http://www.e-naxos.com/Blog/post/De-la-bonne-utilisation-de-AsyncAwait-en-C.aspx
        /// </summary>
        /// <returns></returns>
		public virtual async Task<bool> WorkBeginsAsync()
		{
            var result = await MoveAsync(base.CurrentPos, WorkingPos);

            if (CurrentPos.Equals(WorkingPos))
            {
                IsWorking = true;

                OnStatusChanged(this, new StatusChangedEventArgs("Je commence à travailler"));
            }

            return result && IsWorking;
        }

		public virtual async Task<bool> WorkEndsAsync()
		{
            var result = await MoveAsync(base.CurrentPos, ParkingPos);

            if (CurrentPos.Equals(ParkingPos))
            {
                IsWorking = false;

                OnStatusChanged(this, new StatusChangedEventArgs("Je termine de travailler"));
            }

            return result && !IsWorking;
        }

		public Coordinates WorkingPos { get; set; }

        Type IFactoryQueueElement.Model
        {
            get;

            set;
        }
    }
}
