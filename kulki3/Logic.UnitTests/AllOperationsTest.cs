using Data;
using NUnit.Framework;
using System.Diagnostics;

namespace Logic.UnitTests
{
    internal class AllOperationsTest
    {

        [Test]
        public void CreateDeleteEllipseTest()
        {
            // Arrange
            AllOperations AllOperations = new AllOperations(200, 200);
            // Act
            AllOperations.NewEllipse();
            // Assert
            Assert.That(AllOperations.GetSize() == 1);
            Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault().Y >= 0);
            Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault().X >= 0);
            Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault().Y < AllOperations.GetLength());
            Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault().X < AllOperations.GetWidth());
            // Act
            AllOperations.DeleteEllipse(0);
            // Assert
            Assert.That(AllOperations.GetSize() == 0);
        }

        [Test]
        public void ResolveCollisionTest()
        {
            // Arrange
            AllOperations AllOperations = new AllOperations(200, 200);
            AllOperations.NewEllipse();
            // Act
            AllOperations.ResolveCollision(AllOperations.GetEllipseRepo().FirstOrDefault());
            // Assert
            Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault().Y >= 0);
            Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault().X >= 0);
            Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault().Y < AllOperations.GetLength());
            Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault().X < AllOperations.GetWidth());
        }

        [Test]
        public void ClearKulkodomTest()
        {
            // Arrange
            AllOperations AllOperations = new AllOperations(200, 200);
            // Act
            for (int i = 0; i < 3; i++)
            {
                AllOperations.NewEllipse();
                Assert.That(AllOperations.GetEllipseRepo().Count == i + 1);
                Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault(Ellipse => Ellipse.Id == i).Y >= 0);
                Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault(Ellipse => Ellipse.Id == i).X >= 0);
                Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault(Ellipse => Ellipse.Id == i).Y < AllOperations.GetLength());
                Assert.That(AllOperations.GetEllipseRepo().FirstOrDefault(Ellipse => Ellipse.Id == i).X < AllOperations.GetWidth());
            }
            // Act
            AllOperations.ClearKulkodom();
            // Assert
            Assert.That(AllOperations.GetSize() == 0);
        }

        [Test]
        public void KulkodomSizeTest()
        {
            // Arrange
            AllOperations AllOperations = new AllOperations(200, 400);
            // Assert
            Assert.That(AllOperations.GetWidth() == 400);
            Assert.That(AllOperations.GetLength() == 200);
        }

        [Test]
        public void EllipseCollisionTest()
        {
            // Arrange
            AllOperations AllOperations = new AllOperations(200, 200);
            AllOperations.GetEllipseRepo().Add(new Ellipse(1, 5, 50, 50, 50, 10, 0));
            AllOperations.GetEllipseRepo().Add(new Ellipse(2, 5, 50, 105, 50, -10, 0));
            var Ellipse1 = AllOperations.GetEllipseRepo().FirstOrDefault(Ellipse => Ellipse.Id == 1);
            var Ellipse2 = AllOperations.GetEllipseRepo().FirstOrDefault(Ellipse => Ellipse.Id == 2);
            // Act
            AllOperations.ResolveCollision(Ellipse1);
            AllOperations.ResolveCollision(Ellipse2);
            // Assert
            Assert.That(Ellipse1.velocityX != 10, "" + Ellipse1.Diameter + " " + Ellipse2.Diameter + " " + Ellipse1.X + " " + Ellipse2.X + " " + Ellipse1.Y + " " + Ellipse2.Y);
            Assert.That(Ellipse2.velocityX != -10);
        }

        [Test]
        public void EllipseRightWallCollisionTest()
        {
            // Arrange
            AllOperations AllOperations = new AllOperations(200, 200);
            AllOperations.GetEllipseRepo().Add(new Ellipse(1, 5, 50, 150, 100, 1, 0));
            var Ellipse = AllOperations.GetEllipseRepo().FirstOrDefault();
            // Act
            AllOperations.ResolveCollision(Ellipse);
            // Assert
            Assert.That(Ellipse.velocityX, Is.EqualTo(-1));
        }

        [Test]
        public void EllipseTopWallCollisionTest()
        {
            // Arrange
            AllOperations AllOperations = new AllOperations(200, 200);
            AllOperations.GetEllipseRepo().Add(new Ellipse(1, 5, 50, 100, 0, 0, -1));
            var Ellipse = AllOperations.GetEllipseRepo().FirstOrDefault();
            // Act
            AllOperations.ResolveCollision(Ellipse);
            // Assert
            Assert.That(Ellipse.velocityY, Is.EqualTo(1));
        }

        [Test]
        public void EllipseLeftWallCollisionTest()
        {
            // Arrange
            AllOperations AllOperations = new AllOperations(200, 200);
            AllOperations.GetEllipseRepo().Add(new Ellipse(1, 5, 50, 0, 100, -1, 0));
            var Ellipse = AllOperations.GetEllipseRepo().FirstOrDefault();
            // Act
            AllOperations.ResolveCollision(Ellipse);
            // Assert
            Assert.That(Ellipse.velocityX, Is.EqualTo(1));
        }

        [Test]
        public void EllipseBottomWallCollisionTest()
        {
            // Arrange
            AllOperations AllOperations = new AllOperations(200, 200);
            AllOperations.GetEllipseRepo().Add(new Ellipse(1, 5, 50, 100, 150, 0, 1));
            var Ellipse = AllOperations.GetEllipseRepo().FirstOrDefault();
            // Act
            AllOperations.ResolveCollision(Ellipse);
            // Assert
            Assert.That(Ellipse.velocityY, Is.EqualTo(-1));
        }
    }
}
