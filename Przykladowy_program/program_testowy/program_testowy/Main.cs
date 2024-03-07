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
                Console.WriteLine("Kalkulator");

                Console.WriteLine("Podaj 1 liczbe:");
                var number1 = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Wybierz operacje sposrod: '+', '-', '*', '/'");
                var operation = Console.ReadLine();

                Console.WriteLine("Podaj 2 liczbe:");
                var number2 = decimal.Parse(Console.ReadLine());

                decimal result = 0;

                switch (operation)
                {
                    case "+":
                        result = k.Dodaj(number1, number2);
                        break;
                    case "-":
                        result = k.Odejmij(number1, number2);
                        break;
                    case "*":
                        result = k.Pomnoz(number1, number2);
                        break;
                    case "/":
                        result = k.Podziel(number1, number2);
                        break;
                    default:
                        throw new Exception("Zla operacja!");
                }

                Console.WriteLine($"Wynik: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}