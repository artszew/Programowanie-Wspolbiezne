using NUnit.Framework;

namespace Data.UnitTests
{
    internal class EllipseTests
    {
        [Test]
        public void NewEllipseTest()
        {
            // Arrange
            Ellipse Ellipse = new Ellipse(1, 10, 20, 30, 40, 5, -5);
            // Assert
            Assert.That(Ellipse.Id == 1);
            Assert.That(Ellipse.Mass == 10);
            Assert.That(Ellipse.Diameter == 20);
            Assert.That(Ellipse.X == 30);
            Assert.That(Ellipse.Y == 40);
            Assert.That(Ellipse.velocityX == 5);
            Assert.That(Ellipse.velocityY == -5);
        }

        [Test]
        public void EllipsesSettersTest()
        {
            // Arrange
            Ellipse Ellipse = new Ellipse(1, 10, 20, 30, 40, 5, -5);
            // Act
            Ellipse.Id = 2;
            Ellipse.Mass = 20;
            Ellipse.Diameter = 30;
            Ellipse.velocityX = -4;
            Ellipse.velocityY = 4;
            // Assert
            Assert.That(Ellipse.Id == 2);
            // Assert and Handle Exceptions
            Assert.That(Ellipse.Mass == 20);
            Assert.Throws<InvalidDataException>(() => Ellipse.Mass = -20);
            Assert.That(Ellipse.Diameter == 30);
            Assert.Throws<InvalidDataException>(() => Ellipse.Diameter = -30);
            Assert.That(Ellipse.velocityX == -4);
            Assert.That(Ellipse.velocityY == 4);
        }
    }
}
