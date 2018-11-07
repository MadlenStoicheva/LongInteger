using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeIntegerSecondTry
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter first long integer: ");
            string firstNumber = Console.ReadLine();

            Console.WriteLine("Enter second long integer: ");
            string secondNumber = Console.ReadLine();

            try
            {
                LargeInteger x = new LargeInteger(firstNumber);
                LargeInteger y = new LargeInteger(secondNumber);

                LargeInteger addition = new LargeInteger();
                addition = x + y;
                Console.WriteLine("Addition: " + addition);

                var subt = x - y;
                Console.WriteLine("Subtraction: " + subt);

                var multiplication = x * y;
                Console.WriteLine("Multiplication: " + multiplication);

                var division = x / y;
                Console.WriteLine("Division: " + division);

            }
            catch (ArgumentException exeption)
            {
                Console.Error.WriteLine("Error: " + exeption.Message);
            }
            catch (DivideByZeroException exeption)
            {
                Console.Error.WriteLine("Error: " + exeption.Message);
            }
            catch (NotSupportedException exeption)
            {
                Console.Error.WriteLine("Error: " + exeption.Message);
            }

            Console.ReadKey();
        }
    }
}
