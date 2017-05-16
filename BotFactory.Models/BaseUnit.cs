using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Models
{
	public abstract class BaseUnit : ReportingUnit, IBaseUnit
	{
		double _speed;
        Coordinates _currentPos;
        string _name;

        public BaseUnit(string name, double speed = 1)
		{
            _name = name;
            _speed = speed;

            _currentPos = new Coordinates(0, 0);
		}

		public Coordinates CurrentPos
		{
			get
			{
                return _currentPos;
			}
			set
			{
                _currentPos = value;
			}
		}

		public async Task MoveAsync(Coordinates currentPos, Coordinates destPos)
		{
           Vector v = Vector.FromCoordinates(currentPos, destPos);
           double lenght = v.Length();
           TimeSpan time = TimeSpan.FromSeconds(lenght / _speed);
           await Task.Delay(time);
		}

		public string Name
		{
			get
			{
                return _name;
			}
			set
			{
                _name = value;
			}
		}
	}
}
