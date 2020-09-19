using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace threeteck_HW1
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
