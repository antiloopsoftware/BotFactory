using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BotFactory.Common.Tools
{
    public class Vector
    {
        private double _x;
        private double _y;

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

        /// <summary>
        //see https://www.youtube.com/watch?v=gwk-XGfHR8U
        /// 
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Vector FromCoordinates(Coordinates begin, Coordinates end)
        {
            Vector v = new Vector();

            v._x = end.X - begin.X;
            v._y = end.Y - begin.Y;

            return v;
        }

        /// <summary>
        /// http://onlinemschool.com/math/library/vector/length/
        /// </summary>
        /// <returns></returns>
        public double Length()
        {
            return Math.Sqrt(Math.Pow(_x, 2) + Math.Pow(_y, 2));
        }
    }
}
