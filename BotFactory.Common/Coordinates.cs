using System;

namespace BotFactory.Common.Tools
{
    public class Coordinates : Object
    {
        public Coordinates(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Y { get; set; }

        public double X { get; set; }

        public override bool Equals(Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Coordinates return false.
            Coordinates c = obj as Coordinates;
            if ((Object)c == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (X == c.X) && (Y == c.Y);
        }
    }
}
