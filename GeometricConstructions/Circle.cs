using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows
{
    public struct Circle
    {
        public Point Center { get; }
        public double Radius { get; }

        public Circle(Point center, double radius)
        {
            if(radius<=0)
            {
                throw new DegenerateInputException("Circles must have a positive radius.");
            }
            Center = center;
            Radius = radius;
        }
        // Alternative constructor for a circle from center and point on circle
        public Circle(Point center, Point pointOn) : this(center, (center-pointOn).Length) { }
    }
}
