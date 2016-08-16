using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Collections.Generic;

namespace GeometricConstructionsTest
{
    [TestClass]
    public class ConstructionsTest
    {
        [TestMethod]
        public void LineLineIntersectTest_1()
        {
            // arrange
            Line l1 = new Line(new Point(0, 0), new Point(0, 1));
            Line l2 = new Line(new Point(0, 0), new Point(1, 0));

            // act
            Point P = Constructions.Intersect(l1, l2)[0];

            // assert
            Assert.AreEqual(new Point(0, 0), P);
        }
        [TestMethod]
        public void LineLineIntersectTest_2()
        {
            // arrange
            Line l1 = new Line(new Point(-2, 1), new Point(-1, 2));
            Line l2 = new Line(new Point(1, 1), new Point(2, -1));

            // act
            Point P = Constructions.Intersect(l1, l2)[0];

            // assert
            Assert.AreEqual(new Point(0, 3), P);
        }
        [TestMethod]
        public void LineLineIntersectTest_3()
        {
            // arrange
            Line l1 = new Line(new Point(-1, -1), new Point(1, 1));
            Line l2 = new Line(new Point(-2, 1), new Point(0, -1));

            // act
            Point P = Constructions.Intersect(l1, l2)[0];

            // assert
            Assert.AreEqual(new Point(-.5, -.5), P);
        }
        [TestMethod]
        public void LineCircleIntersectTest_1()
        {
            // arrange
            Line l = new Line(new Point(0, 0), new Point(0, 1));
            Circle c = new Circle(new Point(0, 0), new Point(1, 0));

            // act
            List<Point> Intersections = Constructions.Intersect(l, c);

            // assert
            Assert.AreEqual(2, Intersections.Count);
        }
        [TestMethod]
        public void LineCircleIntersectTest_2()
        {
            // arrange
            Line l = new Line(new Point(0, 0), new Point(1, 0));
            Circle c = new Circle(new Point(1, 1), new Point(0, 1));

            // act
            List<Point> Intersections = Constructions.Intersect(l, c);

            // assert
            Assert.AreEqual(new Point(1,0), Intersections[0]);
        }
        [TestMethod]
        public void LineCircleIntersectTest_3_1()
        {
            // arrange
            Line l = new Line(new Point(-1, 3), new Point(3, -1));
            Circle c = new Circle(new Point(1, 1), new Point(0, 0));

            // act
            List<Point> Intersections = Constructions.Intersect(l, c);

            // assert
            Assert.AreEqual(0, Intersections[0].X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(2, Intersections[0].Y, .00000001, "Y is out of range");

        }
        [TestMethod]
        public void LineCircleIntersectTest_3_2()
        {
            // arrange
            Line l = new Line(new Point(-1, 3), new Point(3, -1));
            Circle c = new Circle(new Point(1, 1), new Point(0, 0));

            // act
            List<Point> Intersections = Constructions.Intersect(l, c);

            // assert
            Assert.AreEqual(2, Intersections[1].X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(0, Intersections[1].Y, .00000001, "Y is out of range");
        }
        [TestMethod]
        public void CircleCircleIntersectTest_1()
        {
            // arrange
            Circle c1 = new Circle(new Point(0, 0), new Point(1, 1));
            Circle c2 = new Circle(new Point(2, 0), new Point(1, 1));

            // act
            List<Point> Intersections = Constructions.Intersect(c1, c2);

            // assert
            Assert.AreEqual(1, Intersections[1].X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(-1, Intersections[1].Y, .00000001, "Y is out of range");
        }
        [TestMethod]
        public void CircleCircleIntersectTest_2_1()
        {
            // arrange
            Circle c1 = new Circle(new Point(-1, -1), 35);
            Circle c2 = new Circle(new Point(32, 32), 13);

            // act
            List<Point> Intersections = Constructions.Intersect(c1, c2);

            // assert
            Assert.AreEqual(20, Intersections[0].X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(27, Intersections[0].Y, .00000001, "Y is out of range");
        }
        [TestMethod]
        public void CircleCircleIntersectTest_2_2()
        {
            // arrange
            Circle c1 = new Circle(new Point(-1, -1), 35);
            Circle c2 = new Circle(new Point(32, 32), 13);

            // act
            List<Point> Intersections = Constructions.Intersect(c1, c2);

            // assert
            Assert.AreEqual(27, Intersections[1].X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(20, Intersections[1].Y, .00000001, "Y is out of range");
        }
        [TestMethod]
        public void PointToLineProjection_1()
        {
            // arrange
            Point p = new Point(2, 4);
            Line l = new Line(new Point(-1, 0), new Point(1, 1));

            // act
            Point q = Constructions.Project(p, l);

            // assert
            Assert.AreEqual(3, q.X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(2, q.Y, .00000001, "Y is out of range");
        }
        [TestMethod]
        public void PointToLineProjection_2()
        {
            // arrange
            Point p = new Point(2, 2);
            Line l = new Line(new Point(0, 0), new Point(1, 1));

            // act
            Point q = Constructions.Project(p, l);

            // assert
            Assert.AreEqual(2, q.X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(2, q.Y, .00000001, "Y is out of range");
        }

        [TestMethod]
        public void Perpendicular_1()
        {
            // arrange
            Point p = new Point(2, 2);
            Line l = new Line(new Point(0, 0), new Point(30, 20));

            // act
            Point q = Constructions.Project(p, l);
            Point r = Constructions.Intersect(l, Constructions.Perpendicular(l, p))[0];

            // assert
            Assert.AreEqual(r.X, q.X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(r.Y, q.Y, .00000001, "Y is out of range");
        }
        [TestMethod]
        public void Parallel_1()
        {
            // arrange
            Point p = new Point(2, 2);
            Line l = new Line(new Point(0, 0), new Point(30, 20));

            // act
            int x = Constructions.Intersect(l, Constructions.Parallel(l, p)).Count;

            // assert
            Assert.AreEqual(0, x); // parallel lines have no intersection point
        }
        [TestMethod]
        public void PerpendicularBisector_1()
        {
            // arrange
            Point p1 = new Point(2, 2);
            Point p2 = new Point(3, 2);

            // act
            Line l = Constructions.PerpendicularBisector(p1, p2);
            Point q = Constructions.Project(l.Point1, l);
            Point r = Constructions.Midpoint(p1, p2);

            // assert
            Assert.AreEqual(r.X, q.X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(r.Y, q.Y, .00000001, "Y is out of range");
        }
        [TestMethod]
        public void AngleBisector_1()
        {
            // arrange
            Point p1 = new Point(2, 2);
            Point p2 = new Point(3, 2);
            Point p3 = new Point(5, 3);
            Line l1 = new Line(p3, p1);
            Line l2 = new Line(p3, p2);


            // act
            Line l = Constructions.AngleBisector(l1,l2)[0];
            Point q = Constructions.Project(l.Point1, l1);
            Point r = Constructions.Project(l.Point1, l2);

            // assert
            Assert.AreEqual((l.Point1-q).Length,(l.Point1-r).Length, .00000001, "point on bisector not equidistant");
        }
        [TestMethod]
        public void Incenter_1()
        {
            // arrange
            Point p1 = new Point(0, 0);
            Point p2 = new Point(3, 0);
            Point p3 = new Point(3, 4);

            // act
            List<Point> points = Constructions.Incenter(p1, p2, p3);

            // assert
            Assert.AreEqual(2, points[0].X, .00000001, "incenter X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(1, points[0].Y, .00000001, "incenter Y is out of range");

            Assert.AreEqual(6, points[1].X, .00000001, "excenter1 X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(3, points[1].Y, .00000001, "excenter1 Y is out of range");

            Assert.AreEqual(-3, points[2].X, .00000001, "excenter2 X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(6, points[2].Y, .00000001, "excenter2 Y is out of range");

            Assert.AreEqual(1, points[3].X, .00000001, "excenter3 X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(-2, points[3].Y, .00000001, "excenter3 Y is out of range");
        }
        [TestMethod]
        public void Incenter_2()
        {
            // arrange (this is the same as the previous test, but with lines instead of points. the triangle is identical.
            Line l1 = new Line(new Point(3, -1), new Point(3, 1));
            Line l2 = new Line(new Point(-3, -4), new Point(6, 8));
            Line l3 = new Line(new Point(1, 0), new Point(2, 0));

            // act
            List<Point> points = Constructions.Incenter(l1, l2, l3);

            // assert
            Assert.AreEqual(2, points[0].X, .00000001, "incenter X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(1, points[0].Y, .00000001, "incenter Y is out of range");

            Assert.AreEqual(6, points[1].X, .00000001, "excenter1 X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(3, points[1].Y, .00000001, "excenter1 Y is out of range");

            Assert.AreEqual(-3, points[2].X, .00000001, "excenter2 X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(6, points[2].Y, .00000001, "excenter2 Y is out of range");

            Assert.AreEqual(1, points[3].X, .00000001, "excenter3 X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(-2, points[3].Y, .00000001, "excenter3 Y is out of range");
        }
        [TestMethod]
        public void Circumcenter_1()
        {
            // arrange
            Point p1 = new Point(0, 0);
            Point p2 = new Point(3, 0);
            Point p3 = new Point(3, 4);

            // act
            Point p = Constructions.Circumcenter(p1, p2, p3);

            // assert
            Assert.AreEqual(1.5, p.X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(2, p.Y, .00000001, "Y is out of range");
        }
        [TestMethod]
        public void Circumcenter_2()
        {
            // arrange (this is the same as the previous test, but with lines instead of points. the triangle is identical.
            Line l1 = new Line(new Point(3, -1), new Point(3, 1));
            Line l2 = new Line(new Point(-3, -4), new Point(6, 8));
            Line l3 = new Line(new Point(1, 0), new Point(2, 0));

            // act
            Point p = Constructions.Circumcenter(l1, l2, l3);

            // assert
            Assert.AreEqual(1.5, p.X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(2, p.Y, .00000001, "Y is out of range");
        }

        [TestMethod]
        public void Orthocenter_1()
        {
            // arrange
            Point p1 = new Point(0, 0);
            Point p2 = new Point(3, 0);
            Point p3 = new Point(3, 4);

            // act
            Point p = Constructions.Orthocenter(p1, p2, p3);

            // assert
            Assert.AreEqual(3, p.X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(0, p.Y, .00000001, "Y is out of range");
        }
        [TestMethod]
        public void Orthocenter_2()
        {
            // arrange (this is the same as the previous test, but with lines instead of points. the triangle is identical.
            Line l1 = new Line(new Point(3, -1), new Point(3, 1));
            Line l2 = new Line(new Point(-3, -4), new Point(6, 8));
            Line l3 = new Line(new Point(1, 0), new Point(2, 0));

            // act
            Point p = Constructions.Orthocenter(l1, l2, l3);

            // assert
            Assert.AreEqual(3, p.X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(0, p.Y, .00000001, "Y is out of range");
        }

        [TestMethod]
        public void Centroid_1()
        {
            // arrange
            Point p1 = new Point(0, 0);
            Point p2 = new Point(3, 0);
            Point p3 = new Point(3, 4);

            // act
            Point p = Constructions.Centroid(p1, p2, p3);

            // assert
            Assert.AreEqual(2, p.X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(4.0/3.0, p.Y, .00000001, "Y is out of range");
        }
        [TestMethod]
        public void Centroid_2()
        {
            // arrange (this is the same as the previous test, but with lines instead of points. the triangle is identical.
            Line l1 = new Line(new Point(3, -1), new Point(3, 1));
            Line l2 = new Line(new Point(-3, -4), new Point(6, 8));
            Line l3 = new Line(new Point(1, 0), new Point(2, 0));

            // act
            Point p = Constructions.Centroid(l1, l2, l3);

            // assert
            Assert.AreEqual(2, p.X, .00000001, "X is out of range"); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(4.0 / 3.0, p.Y, .00000001, "Y is out of range");
        }
    }
}
