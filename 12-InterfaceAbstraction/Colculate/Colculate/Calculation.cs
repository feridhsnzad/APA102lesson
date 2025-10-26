using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colculate
{
    public class Calculation : ICalculation
    {
        public double Calculate(double a, double b, string operation)
        {
            switch (operation)
            {
                case "+":
                    return a + b;
                case "-":
                    return a - b;
                case "*":
                    return a * b;
                case "/":
                    if (b == 0)
                    {
                        Console.WriteLine("❌ Xeta: Sıfıra bölmək olmaz!");
                        return double.NaN;
                    }
                    return a / b;
                default:
                    Console.WriteLine("❌ Xeta: Yanlış emeliyyat daxil etdiniz!");
                    return double.NaN;
            }
        }
    }
}
