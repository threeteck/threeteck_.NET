using System;

namespace threeteck_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = Calculator.GetStandartCalculator();
            try
            {
                Console.WriteLine(calculator.MakeCalculation(Expression.Parse(Console.ReadLine())));
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine("Cannot divide by zero");
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid expression format");
            }

            Console.ReadKey();
        }
    }
}