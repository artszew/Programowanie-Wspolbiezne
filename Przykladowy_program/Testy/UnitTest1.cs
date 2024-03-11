using program_testowy;

namespace Testy
{
    public class Tests
    {
        private Kalkulator k;
        [SetUp]
        public void Setup()
        {
            k = new Kalkulator();
        }

        [Test]
        public void dodajTest()
        {
            Assert.AreEqual(3,k.Dodaj(1,2));
        }

        [Test]
        public void dodajTest2()
        {
            Assert.AreEqual(1, k.Dodaj(-1, 2));
        }

        [Test]
        public void dodawanieZleTest()
        {
            Assert.IsFalse(4 == k.Dodaj(3,3));
        }
    }
}