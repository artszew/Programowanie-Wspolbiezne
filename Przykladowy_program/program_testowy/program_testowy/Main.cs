using System;

namespace program_testowy
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            try
            {
                Kalkulator k = new Kalkulator();
                Console.WriteLine("Kalkulator dodajacy 2 liczby");

                Console.WriteLine("Podaj 1 liczbe:");
                var number1 = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Podaj 2 liczbe:");
                var number2 = decimal.Parse(Console.ReadLine());

                decimal result = 0;

                result = k.Dodaj(number1, number2);
                      
                Console.WriteLine($"Wynik: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}