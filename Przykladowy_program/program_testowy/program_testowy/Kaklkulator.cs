using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace program_testowy
{
    public class Kalkulator
    {
        public decimal Dodaj(decimal x, decimal y)
        {
            return x + y;
        }

        public decimal Odejmij(decimal x, decimal y)
        {
            return x - y;
        }

        public decimal Pomnoz(decimal x, decimal y)
        {
            return x * y;
        }

        public decimal Podziel(decimal x, decimal y)
        {
            return x / y;
        }
    }
}
