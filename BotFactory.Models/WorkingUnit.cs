using BotFactory.Common.Tools;
using BotFactory.Common.Tools.Interfaces;
using System.Threading.Tasks;

namespace BotFactory.Models
{
	public abstract class WorkingUnit : BaseUnit, IWorkingUnit
	{
        bool _isWorking;
        Coordinates _parkingPos;
        Coordinates _workingPos;

        public WorkingUnit(string name, double speed = 1) : base(name, speed)
        {
        }

        public bool IsWorking
		{
			get
			{
                return _isWorking;	
			}
			set
			{
                _isWorking = IsWorking;
			}
		}

		public Coordinates ParkingPos
		{
			get
			{
                return _parkingPos;
			}
			set
			{
                _parkingPos = value;
			}
		}

        /// <summary>
        /// see http://www.e-naxos.com/Blog/post/De-la-bonne-utilisation-de-AsyncAwait-en-C.aspx
        /// </summary>
        /// <returns></returns>
		public async virtual Task<bool> WorkBeginsAsync()
		{
            await MoveAsync(base.CurrentPos, _workingPos);
            if(CurrentPos.Equals(_workingPos))
            {
                _isWorking = true;
            }
            return _isWorking;
		}

		public async virtual Task<bool> WorkEndsAsync()
		{
            await MoveAsync(base.CurrentPos, _parkingPos);

            if (base.CurrentPos.Equals(_parkingPos))
                return true;
            else
                return false;
        }

		public Coordinates WorkingPos
		{
			get
			{
                return _workingPos;
			}
			set
			{
                _workingPos = value;
			}
		}
	}
}
