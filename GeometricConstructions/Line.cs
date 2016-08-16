using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows
{
    public struct Line
    {
        // We represent lines from two points on the line. This avoids divide by zero errors.
        public Point Point1 { get; }
        public Point Point2 { get; }
        
        
        public Line(Point p1, Point p2)
        {
            if(p1==p2)
            {
                throw new DegenerateInputException("Points are not distinct. Line through them is not well defined");
            }
            Point1 = p1;
            Point2 = p2;
        }
    }
}
