using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using System;
using System.Threading.Tasks;

namespace BotFactory.Models
{
	public abstract class BaseUnit : ReportingUnit, IBaseUnit
	{
        public BaseUnit(string name = "Nameless", string model = "Nameless", double buildTime = 5.0, double speed = 5.0) : base(model, buildTime)
        {
            Name = name;
            Speed = speed;
            CurrentPos = new Coordinates(0, 0);
        }

        public Coordinates CurrentPos { get; set; }

		public async Task<bool> MoveAsync(Coordinates currentPos, Coordinates destPos)
		{
           CurrentPos = currentPos;

           OnStatusChanged(this, new StatusChangedEventArgs("Je me déplace")); 

           Vector v = Vector.FromCoordinates(currentPos, destPos);
           double lenght = v.Length();
           TimeSpan time = TimeSpan.FromSeconds(lenght / Speed);
           await Task.Delay(time);
           CurrentPos.X = destPos.X; 
           CurrentPos.Y= destPos.Y;


            return true;
		}

		public string Name { get; set; }
		
        public double Speed { get; set; }
    }
}
