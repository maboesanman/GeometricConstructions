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

            if (discriminant < 0)
            {
                return new List<Point>();
            } else if (discriminant == 0)
            {
                double x = D * d.Y / (dr * dr) + c.Center.X;
                double y = -D * d.X / (dr * dr) + c.Center.Y;

                return new List<Point> { new Point(x, y) };
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
        public static Point Project(Point p, Line l)
        {
            //project a point onto a line
            Vector v1 = p - l.Point1;
            Vector v2 = l.Point2 - l.Point1;
            return l.Point1 + v2 * (v1 * v2) / v2.LengthSquared;
        }
        public static Line Perpendicular(Line l, Point p)
        {
            // produce a line perpendicular to l through p

            Vector v = l.Point2 - l.Point1;
            v = new Vector(-v.Y, v.X); //rotate v 90 degrees
            Point q = p + v;
            return new Line(p, q);
        }
        public static Line Parallel(Line l, Point p)
        {
            // produce a line parallel to l through p
            return new Line(p, p + (l.Point2 - l.Point1));
        }
        public static Line PerpendicularBisector(Point p1, Point p2)
        {
            // produce the perpendicular bisector of the segment connecting p1 and p2
            return Perpendicular(new Line(p1, p2), Midpoint(p1, p2));
        }
        public static List<Line> AngleBisector(Line l1, Line l2)
        {
            List<Point> c = Intersect(l1, l2);
            if (c.Count == 0) //if they are parallel, return the line of points equidistant from the two lines.
                return new List<Line> { new Line(Midpoint(l1.Point1, l2.Point1), Midpoint(l1.Point1, l2.Point2)) };
            Point center = c[0];
            Vector v1 = l1.Point2 - l1.Point1;
            Vector v2 = l2.Point2 - l2.Point1;
            v1.Normalize();
            v2.Normalize();
            List<Line> result = new List<Line>();
            result.Add(new Line(center, center + v1 + v2));
            result.Add(new Line(center, center + v1 - v2));
            return result;
        }

        private static Point Barycentric(Point A, Point B, Point C, double a, double b, double c)
        {
            Point o = new Point(0, 0);
            Vector vA = A - o;
            Vector vB = B - o;
            Vector vC = C - o;
            return o + (vA * a + vB * b + vC * c) / (a + b + c);
        }

        public static List<Point> Incenter(Point p1, Point p2, Point p3)
        {
            // this returns a list containing the incenter, followed by the excenters opposite p1, p2, and p3 respectively.
            // Barycentric coordinates from http://mathworld.wolfram.com/BarycentricCoordinates.html
            double d1 = (p2 - p3).Length;
            double d2 = (p1 - p3).Length;
            double d3 = (p1 - p2).Length;
            List<Point> result = new List<Point>();
            result.Add(Barycentric(p1, p2, p3, d1, d2, d3));
            result.Add(Barycentric(p1, p2, p3, -d1, d2, d3));
            result.Add(Barycentric(p1, p2, p3, d1, -d2, d3));
            result.Add(Barycentric(p1, p2, p3, d1, d2, -d3));

            return result;
        }

        public static List<Point> Incenter(Line l1, Line l2, Line l3)
        {
            // for a triangle defined by its sides
            Point p1 = Intersect(l2, l3)[0];
            Point p2 = Intersect(l1, l3)[0];
            Point p3 = Intersect(l1, l2)[0];
            return Incenter(p1, p2, p3);
        }

        public static Point Centroid(Point p1, Point p2, Point p3)
        {
            // Barycentric coordinates from http://mathworld.wolfram.com/BarycentricCoordinates.html
            return Barycentric(p1, p2, p3, 1, 1, 1);
        }

        public static Point Centroid(Line l1, Line l2, Line l3)
        {
            // for a triangle defined by its sides
            Point p1 = Intersect(l2, l3)[0];
            Point p2 = Intersect(l1, l3)[0];
            Point p3 = Intersect(l1, l2)[0];
            return Centroid(p1, p2, p3);
        }

        public static Point Circumcenter(Point p1, Point p2, Point p3)
        {
            // Barycentric coordinates from http://mathworld.wolfram.com/BarycentricCoordinates.html

            double d1 = (p2 - p3).Length;
            double d2 = (p1 - p3).Length;
            double d3 = (p1 - p2).Length;

            double w1 = d1 * d1 * (d2 * d2 + d3 * d3 - d1 * d1);
            double w2 = d2 * d2 * (d3 * d3 + d1 * d1 - d2 * d2);
            double w3 = d3 * d3 * (d1 * d1 + d2 * d2 - d3 * d3);

            return Barycentric(p1, p2, p3, w1, w2, w3);
        }

        public static Point Circumcenter(Line l1, Line l2, Line l3)
        {
            // for a triangle defined by its sides
            Point p1 = Intersect(l2, l3)[0];
            Point p2 = Intersect(l1, l3)[0];
            Point p3 = Intersect(l1, l2)[0];
            return Circumcenter(p1, p2, p3);
        }

        public static Point Orthocenter(Point p1, Point p2, Point p3)
        {
            // Barycentric coordinates from http://mathworld.wolfram.com/BarycentricCoordinates.html

            double d1 = (p2 - p3).Length;
            double d2 = (p1 - p3).Length;
            double d3 = (p1 - p2).Length;

            double m1 = (d2 * d2 + d3 * d3 - d1 * d1);
            double m2 = (d3 * d3 + d1 * d1 - d2 * d2);
            double m3 = (d1 * d1 + d2 * d2 - d3 * d3);

            return Barycentric(p1, p2, p3, m2 * m3, m1 * m3, m1 * m2);
        }

        public static Point Orthocenter(Line l1, Line l2, Line l3)
        {
            // for a triangle defined by its sides
            Point p1 = Intersect(l2, l3)[0];
            Point p2 = Intersect(l1, l3)[0];
            Point p3 = Intersect(l1, l2)[0];
            return Orthocenter(p1, p2, p3);
        }


    }
}
