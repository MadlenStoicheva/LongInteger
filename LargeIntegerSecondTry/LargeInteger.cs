using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LargeIntegerSecondTry
{
    public class LargeInteger
    {
        private string largeInteger;
        private int Length;

        public LargeInteger()
        {
            this.largeInteger = "";
        }

        public LargeInteger(string largeInteger)
        {
            this.largeInteger = largeInteger.Trim();
            this.Length = largeInteger.Length;

            if (String.IsNullOrEmpty(largeInteger))
            {
                throw new ArgumentException("Input cannot be empty!");
            }

            if (!Regex.IsMatch(largeInteger, "^[0-9]*$"))
            {
                throw new ArgumentException("Input must contains only numbers 0-9!");
            }
        }

        public static LargeInteger operator +(LargeInteger firstLongInteger, LargeInteger secondLongInteger)
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
                if (firstLongInteger.Length < secondLongInteger.Length)
                {
                    firstLongInteger = AddZeroes(firstLongInteger, secondLongInteger);
                }
                else
                {
                    secondLongInteger = AddZeroes(firstLongInteger, secondLongInteger);
                }
            }

            string firstGroupDigits = Reverse(firstLongInteger.ToString());
            string secondGroupDigits = Reverse(secondLongInteger.ToString());

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


            LargeInteger additionResult = new LargeInteger(Reverse(result));
            return additionResult;
        }

        public static LargeInteger operator -(LargeInteger firstLongInteger, LargeInteger secondLongInteger)
        {
            int maxLength = 0;
            LargeInteger subtractionResult = new LargeInteger();

            if (firstLongInteger.Length > secondLongInteger.Length)
            {
                maxLength = firstLongInteger.Length;
                //Here add Zeroes!
                secondLongInteger = AddZeroes(firstLongInteger, secondLongInteger);
            }
            else if (firstLongInteger.Length == secondLongInteger.Length)
            {
                if (IsFirstIntegerSmaller(firstLongInteger, secondLongInteger) == false)
                {
                    maxLength = firstLongInteger.Length;
                }
                else
                {
                    throw new ArgumentException("First number is smaller than second number!");
                }
            }
            else
            {
                throw new ArgumentException("First integer can't be negative or less than second number!");
            }

            string firstGroupDigits = Reverse(firstLongInteger.ToString());
            string secondGroupDigits = Reverse(secondLongInteger.ToString());

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


            if (firstLongInteger.ToString().Equals(secondLongInteger.ToString()))
            {
                subtractionResult = new LargeInteger("0");
                return subtractionResult;
            }
            else
            {
                subtractionResult = new LargeInteger(Reverse(result).TrimStart('0'));
                return subtractionResult;

            }
        }

        public static LargeInteger operator *(LargeInteger firstLongInteger, LargeInteger secondLongInteger)
        {
            if (firstLongInteger.ToString().Equals("0") || secondLongInteger.ToString().Equals("0"))
            {
                throw new ArgumentException("You can't multiply by 0!");
            }

            int maxLength = Math.Max(firstLongInteger.Length, secondLongInteger.Length);
            int minLength = Math.Min(firstLongInteger.Length, secondLongInteger.Length);

            string firstGroupDigits = Reverse(firstLongInteger.ToString());
            string secondGroupDigits = Reverse(secondLongInteger.ToString());

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

            LargeInteger sum = new LargeInteger();
            LargeInteger nextNumber = new LargeInteger();
            LargeInteger multiplicationResult = new LargeInteger();

            if (finalResult.Count == 1)
            {
                multiplicationResult = new LargeInteger(Reverse(finalResult[0]));
                return multiplicationResult;
            }
            else
            {
                for (int i = 0; i < finalResult.Count; i++)
                {
                    nextNumber = new LargeInteger(Reverse(finalResult[i]) + string.Concat(Enumerable.Repeat("0", i)));

                    sum += nextNumber;

                }
                multiplicationResult = sum;

                return multiplicationResult;
            }
        }

        public static LargeInteger operator /(LargeInteger firstLongInteger, LargeInteger secondLongInteger)
        {
            var diff = Math.Abs(firstLongInteger.Length - secondLongInteger.Length);

            LargeInteger difference = new LargeInteger();
            LargeInteger multiplyer = secondLongInteger;
            LargeInteger subtractionResult = firstLongInteger;
            LargeInteger sumFinalResult = new LargeInteger();


            if (firstLongInteger.ToString().Equals("0") || secondLongInteger.ToString().Equals("0"))
            {
                throw new DivideByZeroException("You can't divide by 0!");
            }

            if (firstLongInteger.Length < secondLongInteger.Length)
            {
                throw new NotSupportedException("Second long integer is bigger than first long integer! Result is 0!");
            }

            if (secondLongInteger.Length == 1 && secondLongInteger.ToString().Equals("1"))
            {
                return firstLongInteger;
            }

            if (firstLongInteger.Length == secondLongInteger.Length)
            {
                if (IsFirstIntegerSmaller(firstLongInteger, secondLongInteger) == true)
                {
                    throw new ArgumentException("First integer is smaller than second number!");
                }
            }


            while (subtractionResult.Length >= secondLongInteger.Length)
            {
                if (subtractionResult.Length == secondLongInteger.Length && subtractionResult.ToString()[0] < secondLongInteger.ToString()[0])
                {
                    break;
                }

                if (subtractionResult.Length > secondLongInteger.Length)
                {
                    if (subtractionResult.ToString().First() > secondLongInteger.ToString().First())
                    {
                        diff = Math.Abs(subtractionResult.Length - secondLongInteger.Length);
                    }
                    else
                    {
                        diff = Math.Abs(subtractionResult.Length - secondLongInteger.Length) - 1;
                    }

                    difference = new LargeInteger("1" + GenerateZeroes(diff));
                    multiplyer = secondLongInteger * difference;
                    subtractionResult = subtractionResult - multiplyer;
                    sumFinalResult += difference;
                }
                else
                {
                    if (IsFirstIntegerSmaller(subtractionResult, secondLongInteger) == false)
                    {
                        difference = new LargeInteger("1");

                        subtractionResult = subtractionResult - secondLongInteger;
                        sumFinalResult += difference;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return sumFinalResult;
        }

        public override string ToString()
        {
            return largeInteger;
        }

        static string Reverse(string inputText)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = inputText.Length - 1; i >= 0; i--)
            {
                stringBuilder.Append(inputText.ToString()[i]);
            }
            return stringBuilder.ToString();
        }

        static LargeInteger AddZeroes(LargeInteger firstLongInteger, LargeInteger secondLongInteger)
        {
            var diff = Math.Abs(firstLongInteger.Length - secondLongInteger.Length);

            if (firstLongInteger.Length < secondLongInteger.Length)
            {
                firstLongInteger = new LargeInteger(GenerateZeroes(diff) + firstLongInteger);
                return firstLongInteger;
            }
            else
            {
                secondLongInteger = new LargeInteger(GenerateZeroes(diff) + secondLongInteger);
                return secondLongInteger;
            }
        }

        static bool IsFirstIntegerSmaller(LargeInteger firstLongInteger, LargeInteger secondLongInteger)
        {
            bool smaller = true;

            if (Convert.ToInt32(firstLongInteger.ToString()[0]) <= Convert.ToInt32(secondLongInteger.ToString()[0]))
            {
                int i;
                if (Convert.ToInt32(firstLongInteger.ToString()[0]) < Convert.ToInt32(secondLongInteger.ToString()[0]))
                {
                    i = 0;
                }
                else
                {
                    i = 1;
                }

                for (; i < firstLongInteger.Length;)
                {
                    if (Convert.ToInt32(firstLongInteger.ToString()[i]) >= Convert.ToInt32(secondLongInteger.ToString()[i]))
                    {
                        smaller = false;
                        return false;
                    }
                    else
                    {
                        smaller = true;
                        return true;
                    }
                }

            }
            else
            {
                smaller = false;
                return false;
            }

            return smaller;
        }

        static string GenerateZeroes(int diff)
        {
            string Zeroes = String.Join("", Enumerable.Repeat("0", diff));

            return Zeroes;
        }
    }
}