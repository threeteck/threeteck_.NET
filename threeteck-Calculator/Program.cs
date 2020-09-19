using System;

namespace threeteck_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = Calculator.GetStandartCalculator();
            Console.WriteLine(calculator.MakeCalculation(Console.ReadLine()));
            Console.ReadKey();
        }
    }
}