using System;
using System.Windows.Media;
using kulki3;
using kulki3.MVVM.Model;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ElipsaTestX()
        {
            EllipseModel ellipse = new EllipseModel
            {
                X = 50, // Bounds for X coordinate
                Y = 50, // Bounds for Y coordinate
                                              // Y = 400, // Bounds for Y coordinate
                Color = Brushes.Blue // Color
            };
            Assert.That(ellipse.X, Is.EqualTo(50));
        }

        [Test]
        public void ElipsaTestY()
        {
            EllipseModel ellipse = new EllipseModel
            {
                X = 50,
                Y = 50,
                Color = Brushes.Blue
            };
            Assert.That(ellipse.Y, Is.EqualTo(50));
        }

        [Test]
        public void ElipsaTestColor()
        {
            EllipseModel ellipse = new EllipseModel
            {
                X = 50,
                Y = 50,
                Color = Brushes.Blue
            };
            Assert.That(ellipse.Color, Is.EqualTo(Brushes.Blue));
        }
    }
}