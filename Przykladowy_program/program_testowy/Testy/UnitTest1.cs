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
        public void odejmijTest()
        {
            Assert.AreEqual(3, k.Odejmij(4, 1));
        }

        [Test]
        public void pomnozTest()
        {
            Assert.AreEqual(12, k.Pomnoz(4, 3));
        }

        [Test]
        public void podzielTest()
        {
            Assert.AreEqual(2.5, k.Podziel(5, 2));
        }
    }
}