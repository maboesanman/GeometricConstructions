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
            Assert.AreEqual(0, Intersections[0].X, .00000001); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(2, Intersections[0].Y, .00000001);

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
            Assert.AreEqual(2, Intersections[1].X, .00000001); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(0, Intersections[1].Y, .00000001);
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
            Assert.AreEqual(1, Intersections[1].X, .00000001); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(-1, Intersections[1].Y, .00000001);
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
            Assert.AreEqual(20, Intersections[0].X, .00000001); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(27, Intersections[0].Y, .00000001);
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
            Assert.AreEqual(27, Intersections[1].X, .00000001); // test the x and y coordinates separately and use a tolerance
            Assert.AreEqual(20, Intersections[1].Y, .00000001);
        }
    }
}
