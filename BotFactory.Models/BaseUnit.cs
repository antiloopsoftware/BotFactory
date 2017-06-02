using BotFactory.Common.Interface;
using BotFactory.Common.Tools;
using System;
using System.Threading.Tasks;

namespace BotFactory.Models
{
	public abstract class BaseUnit : ReportingUnit, IBaseUnit
	{
        public string Name { get; set; }

        public double Speed { get; set; }

        public BaseUnit(string name = "SANS NOM", string model = "SANS NOM", double buildTime = 5.0, double speed = 5.0) : base(model, buildTime)
        {
            Name = name;
            Speed = speed;
            CurrentPos = new Coordinates(0, 0);
        }

        public Coordinates CurrentPos { get; set; }

		public async Task<bool> MoveAsync(Coordinates currentPos, Coordinates destPos)
		{
           CurrentPos = currentPos;

           OnStatusChanged(this, new StatusChangedEventArgs("JE ME DÉPLACE")); 

           Vector v = Vector.FromCoordinates(currentPos, destPos);
           double lenght = v.Length();
           TimeSpan time = TimeSpan.FromSeconds(lenght / Speed);
           await Task.Delay(time);
           CurrentPos.X = destPos.X; 
           CurrentPos.Y= destPos.Y;

           return true;
		}
    }
}
