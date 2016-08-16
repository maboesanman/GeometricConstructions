using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows
{
    public class Constructions
    {
        public static Point Midpoint(Point p1, Point p2)
        {
            //though this can be achieved through composition of Intersections, this is clearly more efficient
            return p1 + (p2 - p1) * .5;
        }
        public static List<Point> Intersect(Line l1, Line l2)
        {
            // return a list of intersection points. this will always be a single point, or an empty list if the lines are parallel.

            Vector v1 = l1.Point2 - l1.Point1;
            Vector v2 = l2.Point2 - l2.Point1;
            Vector v3 = l2.Point1 - l1.Point1;

            double d = v1.X * v2.Y - v2.X * v1.Y;
            if (d == 0) // the lines are parallel
                return new List<Point>();
            double t = (v2.Y * v3.X - v2.X * v3.Y) / d;
            return new List<Point> { l1.Point1 + v1 * t };
        }
        public static List<Point> Intersect(Line l, Circle c)
        {
            // return a list of intersection points. this can empty, a single point (if they are tangent) or a pair of points.

            // calculation based on the equations listed here:
            // http://mathworld.wolfram.com/Circle-LineIntersection.html
            Vector d = l.Point2 - l.Point1;
            double dr = d.Length;
            double D = (l.Point1.X - c.Center.X) * (l.Point2.Y - c.Center.Y) - (l.Point2.X - c.Center.X) * (l.Point1.Y - c.Center.Y);

            double discriminant = c.Radius * c.Radius * dr * dr - D * D;

            if(discriminant<0)
            {
                return new List<Point>();
            } else if(discriminant==0)
            {
                double x = D * d.Y / (dr * dr) + c.Center.X;
                double y = -D * d.X / (dr * dr) + c.Center.Y;

                return new List<Point> { new Point(x,y)};
            } else 
            {
                double x = D * d.Y / (dr * dr) + c.Center.X;
                double y = -D * d.X / (dr * dr) + c.Center.Y;

                double sgnDy;
                if (d.Y < 0)
                    sgnDy = -1;
                else
                    sgnDy = 1;

                double xd = sgnDy * d.X * Math.Sqrt(discriminant) / (dr * dr);
                double yd = Math.Abs(d.Y) * Math.Sqrt(discriminant) / (dr * dr);

                return new List<Point> { new Point(x + xd, y + yd), new Point(x - xd, y - yd) };
            }
        }
        public static List<Point> Intersect(Circle c, Line l)
        {
            return Intersect(l, c);
        }
        public static List<Point> Intersect(Circle c1, Circle c2)
        {
            // return a list of intersection points. this can empty, a single point (if they are tangent) or a pair of points.

            // calculation based on the equations listed here:
            // http://mathworld.wolfram.com/Circle-CircleIntersection.html

            double d = (c1.Center - c2.Center).Length;
            double r1 = c1.Radius;
            double r2 = c2.Radius;

            // x is the position along the line which passes through the centers of the circles
            double x = (d * d + r1 * r1 - r2 * r2) / (2 * d);
            
            // and p1 is the (shared) projection of the intersections to the line through the centers
            Point p1 = c1.Center + (c2.Center - c1.Center) * x / d;

            // then produce a vector perpendicular to the line through the centers
            Vector v1 = (c1.Center - c2.Center);
            v1 = new Vector(-v1.Y, v1.X); // rotate 90 degrees

            // and translate p1 by it to get p2
            Point p2 = p1 + v1;

            // finally return the intersections with one of the circles and the line throug the computed points.
            return Intersect(c1, new Line(p1, p2));
        }
    }
}
