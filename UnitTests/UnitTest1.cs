using System;
using System.Windows.Media;
using kulki3;
using kulki3.MVVM.Model;
using kulki3.MVVM.View;
using kulki3.MVVM.ViewModel;

namespace UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        //testowanie warstwy Model
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

        //testowanie warstwy ViewModel
        [Test]
        public void MoveEllipseTest()
        {
            KulkomaniaViewModel KV = new KulkomaniaViewModel();
            double firstX = KV.Ellipses[0].X;
            double firstY = KV.Ellipses[0].Y;
            KV.MoveEllipsesForTest(1);
            double secondX = KV.Ellipses[0].X;
            double secondY = KV.Ellipses[0].X;

            Assert.Multiple(() =>
            {
                Assert.That(firstX, Is.Not.EqualTo(secondX));
                Assert.That(firstY, Is.Not.EqualTo(secondY));
            });
        }
    }
}