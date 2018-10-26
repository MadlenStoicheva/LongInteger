using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LargeIntegerSecondTry
{
    public class LargeInteger
    {
        private string largeInteger;
        //private List<int> digits;

        public LargeInteger(string largeInteger)
        {
            //digits = new List<int>();

            //foreach (var item in largeInteger)
            //{
            //    digits.Add(item);
            //}

            this.largeInteger = largeInteger;
        }

        public string Addition(string firstLongInteger, string secondLongInteger)
        {
            int maxLength = 0;
            if (firstLongInteger.Length > secondLongInteger.Length)
            {
                maxLength = firstLongInteger.Length;
            }
            else
            {
                maxLength = secondLongInteger.Length;
            }

            if (firstLongInteger.Length != secondLongInteger.Length)
            {
                var diff = Math.Abs(firstLongInteger.Length - secondLongInteger.Length);

                if (firstLongInteger.Length < secondLongInteger.Length)
                {
                    firstLongInteger = String.Join("", Enumerable.Repeat("0", diff)) + firstLongInteger;
                }
                else
                {
                    secondLongInteger = String.Join("", Enumerable.Repeat("0", diff)) + secondLongInteger;
                }
            }

            string firstGroupDigits = Reverse(firstLongInteger);
            string secondGroupDigits = Reverse(secondLongInteger);

            string result = "";
            int numberInMind = 0;

            for (int i = 0; i < maxLength; i++)
            {
                int digit1 = Convert.ToInt32(firstGroupDigits[i].ToString().Trim());
                int digit2 = Convert.ToInt32(secondGroupDigits[i].ToString().Trim());

                int digitSum = digit1 + digit2 + numberInMind;

                if (digitSum >= 10)
                {
                    digitSum -= 10;
                    numberInMind = 1;
                    result += digitSum;
                }
                else
                {
                    numberInMind = 0;
                    result += digitSum;
                }
            }

            if (numberInMind != 0)
            {
                result += numberInMind;
            }

            return Reverse(result);
        }

        public string Subtraction(string firstLongInteger, string secondLongInteger)
        {
            int maxLength = 0;

            if (firstLongInteger.Length > secondLongInteger.Length)
            {
                maxLength = firstLongInteger.Length;
            }
            else if (firstLongInteger.Length == secondLongInteger.Length)
            {
                if (Convert.ToInt32(firstLongInteger.First()) <= Convert.ToInt32(secondLongInteger.First()))
                {
                    bool smaller = false;
                    for (int i = 0; i < firstLongInteger.Length; i++)
                    {
                        if (Convert.ToInt32(firstLongInteger[i]) > Convert.ToInt32(secondLongInteger[i]))
                        {
                            maxLength = firstLongInteger.Length;
                        }
                        else
                        {
                            smaller = true;
                        }
                    }

                    if (smaller == true)
                    {
                        Console.WriteLine("First number is smaller than second number!");
                    }
                }
                else
                {
                    maxLength = firstLongInteger.Length;
                }
            }
            else
            {
                Console.WriteLine("Number one can't be negative number or less than second number!");
                // maxLength = secondLongInteger.Length;
            }

            if (firstLongInteger.Length != secondLongInteger.Length)
            {
                var diff = Math.Abs(firstLongInteger.Length - secondLongInteger.Length);

                if (firstLongInteger.Length < secondLongInteger.Length)
                {
                    firstLongInteger = String.Join("", Enumerable.Repeat("0", diff)) + firstLongInteger;
                }
                else
                {
                    secondLongInteger = String.Join("", Enumerable.Repeat("0", diff)) + secondLongInteger;
                }
            }

            string firstGroupDigits = Reverse(firstLongInteger);
            string secondGroupDigits = Reverse(secondLongInteger);

            string result = "";
            int numberInMind = 0;

            for (int i = 0; i < maxLength; i++)
            {
                int digit1 = Convert.ToInt32(firstGroupDigits[i].ToString());
                int digit2 = Convert.ToInt32(secondGroupDigits[i].ToString());

                int digitSum = digit1 - digit2 - numberInMind;

                if (digitSum < 0)
                {
                    digitSum += 10;
                    numberInMind = 1;
                    result += digitSum;
                }
                else
                {
                    numberInMind = 0;
                    result += digitSum;
                }
            }

            if (firstLongInteger.Equals(secondLongInteger))
            {
                //  Console.WriteLine("Subtraction: " + "0");
                return "0";
            }
            else
            {
                // Console.WriteLine("Subtraction: " + Reverse(result).TrimStart('0'));
                return Reverse(result).TrimStart('0');
            }
        }

        public string Multiplication(string firstLongInteger, string secondLongInteger)
        {
            int maxLength = Math.Max(firstLongInteger.Length, secondLongInteger.Length);
            int minLength = Math.Min(firstLongInteger.Length, secondLongInteger.Length);

            string firstGroupDigits = Reverse(firstLongInteger);
            string secondGroupDigits = Reverse(secondLongInteger);

            List<string> finalResult = new List<string>();
            string result = "";
            int numberInMind = 0;
            string resultOne = "";

            for (int i = 0; i < minLength; i++)
            {
                for (int j = 0; j < maxLength; j++)
                {
                    int digit1 = 0;
                    int digit2 = 0;

                    if (firstGroupDigits.Length > secondGroupDigits.Length)
                    {
                        digit1 = Convert.ToInt32(secondGroupDigits[i].ToString());
                        digit2 = Convert.ToInt32(firstGroupDigits[j].ToString());
                    }
                    else
                    {
                        digit1 = Convert.ToInt32(firstGroupDigits[i].ToString());
                        digit2 = Convert.ToInt32(secondGroupDigits[j].ToString());
                    }

                    int digitSum = digit1 * digit2;
                    digitSum += numberInMind;


                    if (digitSum >= 10)
                    {
                        numberInMind = digitSum / 10;
                        digitSum -= numberInMind * 10;
                        resultOne += digitSum;
                    }
                    else
                    {
                        numberInMind = 0;
                        resultOne += digitSum;
                    }

                }

                if (numberInMind != 0)
                {
                    resultOne += numberInMind;
                    numberInMind = 0;
                }

                result += (resultOne);
                resultOne = "";

                finalResult.Add(result);
                result = "";
            }

            string sum = "";
            string nextNumber = "";

            if (finalResult.Count <= 1)
            {
                // Console.WriteLine("Multiplication: " + Reverse(finalResult[0]));
                return Reverse(finalResult[0]);
            }
            else
            {

                for (int i = 0; i < finalResult.Count; i++)
                {
                    nextNumber = Reverse(finalResult[i]) + string.Concat(Enumerable.Repeat("0", i));

                    sum = Addition(sum, nextNumber);

                }
                //Console.WriteLine("Multiplication: " + sum);
                return sum;
            }
        }

        public void Division(string firstLongInteger, string secondLongInteger)
        {
           

        }


        static string Reverse(string inputText)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = inputText.Length - 1; i >= 0; i--)
            {
                stringBuilder.Append(inputText[i]);
            }
            return stringBuilder.ToString();
        }
    }
}