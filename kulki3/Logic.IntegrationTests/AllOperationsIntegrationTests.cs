using NUnit.Framework;
using System.IO;
using System.Text.Json;
using System.Linq;

namespace Logic.IntegrationTests
{
    [TestFixture]
    internal class AllOperationsIntegrationTests
    {
        private readonly string logFilePath = "ellipses_log.json";

        [SetUp]
        public void Setup()
        {
            if (File.Exists(logFilePath))
            {
                File.Delete(logFilePath);
            }
        }

        [Test]
        public void LogEllipsesTest()
        {
            // Arrang
            AllOperations allOperations = new AllOperations(500, 500);

            // Act
            for (int i = 0; i < 5; i++)
            {
                allOperations.NewEllipse();
                Thread.Sleep(1500); // Poczekaj, aby timer móg³ zapisaæ log
            }

            // Assert
            Assert.That(File.Exists(logFilePath), Is.True, "Log file does not exist");

            var logContents = File.ReadAllLines(logFilePath);
            Assert.That(logContents.Length, Is.GreaterThan(0), "Log file is empty");

            foreach (var line in logContents)
            {
                var ellipsesData = JsonSerializer.Deserialize<dynamic>(line);
                Assert.That(ellipsesData.Count, Is.GreaterThan(0), "Logged data is empty");
            }
        }
    }
}
