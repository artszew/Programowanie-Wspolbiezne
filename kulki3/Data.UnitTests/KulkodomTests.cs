using NUnit.Framework;

namespace Data.UnitTests
{
    internal class KulkodomTests
    {
        [Test]
        public void NewKulkodomTest()
        {
            // Arrange & Act
            Kulkodom Kulkodom = new Kulkodom(200, 400);
            // Assert
            Assert.That(Kulkodom.Length == 200);
            Assert.That(Kulkodom.Width == 400);
            Assert.That(Kulkodom.getRepository().Count == 0);
        }

        [Test]
        public void KulkodomSettersTest()
        {
            // Arrange
            Kulkodom Kulkodom = new Kulkodom(200, 400);
            // Act
            Kulkodom.Length = 300;
            Kulkodom.Width = 500;
            // Assert
            Assert.That(Kulkodom.Length == 300);
            Assert.That(Kulkodom.Width == 500);
        }

        [Test]
        public void AddEllipsesTest()
        {
            // Arrange
            Kulkodom Kulkodom = new Kulkodom(200, 400);
            Ellipse Ellipse = new Ellipse(1, 10, 20, 30, 40, 5, -5);
            // Act
            Kulkodom.addEllipse(Ellipse);
            // Assert
            Assert.That(Kulkodom.getSize() == 1);
            Assert.That(Kulkodom.getRepository().FirstOrDefault(Ellipse => Ellipse.Id == 1) == Ellipse);
            // Act
            Kulkodom.removeEllipse(Ellipse);
            // Assert
            Assert.That(Kulkodom.getSize() == 0);
        }
    }
}
