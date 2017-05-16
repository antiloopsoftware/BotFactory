using System;
using System.Collections.Generic;
using System.Text;

namespace BotFactory.Common.Tools
{
    public class Coordinates : Object
    {
        private double _x;
        private double _y;

        public Coordinates(double X, double Y)
        {
            _x = X;
            _y = Y;
        }

        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

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
            return (_x == c.X) && (_y == c.Y);
        }
    }
}
