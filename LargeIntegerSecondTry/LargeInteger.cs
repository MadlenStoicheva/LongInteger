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
        private int Length;

        public LargeInteger()
        {
            this.largeInteger = "";
        }

        public LargeInteger(string largeInteger)
        {
            this.largeInteger = largeInteger;
            this.Length = largeInteger.Length;
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
                var diff = Math.Abs(firstLongInteger.Length - secondLongInteger.Length);

                if (firstLongInteger.Length < secondLongInteger.Length)
                {
                    firstLongInteger = new LargeInteger(String.Join("", Enumerable.Repeat("0", diff)) + firstLongInteger);
                }
                else
                {
                    secondLongInteger = new LargeInteger(String.Join("", Enumerable.Repeat("0", diff)) + secondLongInteger);
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

            if (firstLongInteger.Length > secondLongInteger.Length)
            {
                maxLength = firstLongInteger.Length;
            }
            else if (firstLongInteger.Length == secondLongInteger.Length)
            {
                if (Convert.ToInt32(firstLongInteger.ToString().First()) <= Convert.ToInt32(secondLongInteger.ToString().First()))
                {
                    bool smaller = false;
                    for (int i = 0; i < firstLongInteger.Length; i++)
                    {
                        if (Convert.ToInt32(firstLongInteger.ToString()[i]) > Convert.ToInt32(secondLongInteger.ToString()[i]))
                        {
                            maxLength = firstLongInteger.Length;
                            smaller = false;
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
            }

            if (firstLongInteger.Length != secondLongInteger.Length)
            {
                var diff = Math.Abs(firstLongInteger.Length - secondLongInteger.Length);

                if (firstLongInteger.Length < secondLongInteger.Length)
                {
                    firstLongInteger = new LargeInteger(String.Join("", Enumerable.Repeat("0", diff)) + firstLongInteger);
                }
                else
                {
                    secondLongInteger = new LargeInteger(String.Join("", Enumerable.Repeat("0", diff)) + secondLongInteger);
                }
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

            LargeInteger subtractionResult = new LargeInteger();

            if (firstLongInteger.Equals(secondLongInteger))
            {
                return subtractionResult = new LargeInteger("0");
            }
            else
            {
                return subtractionResult = new LargeInteger(Reverse(result).TrimStart('0'));

            }
        }

        public static LargeInteger operator *(LargeInteger firstLongInteger, LargeInteger secondLongInteger)
        {
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

            if (finalResult.Count < 1)
            {
                return multiplicationResult = new LargeInteger("0");
            }
            else if (finalResult.Count == 1)
            {

                return multiplicationResult = new LargeInteger(Reverse(finalResult[0]));
            }
            else
            {

                for (int i = 0; i < finalResult.Count; i++)
                {
                    nextNumber = new LargeInteger(Reverse(finalResult[i]) + string.Concat(Enumerable.Repeat("0", i)));

                    sum += nextNumber;

                }
                return multiplicationResult = sum;
            }
        }
        public static LargeInteger operator /(LargeInteger firstLongInteger, LargeInteger secondLongInteger)
        {
            LargeInteger result = new LargeInteger();

            if (secondLongInteger.Length < 1 || firstLongInteger.Length < 1)
            {
                return result = new LargeInteger("You can't divide by 0!");
            }

            if (firstLongInteger.Length < secondLongInteger.Length)
            {
                // return result = new LargeInteger("Second long integer is bigger than first long integer!");
                return result = new LargeInteger("0");
            }

            if (firstLongInteger.Length == 1 && secondLongInteger.Length == 1)
            {
                bool smaller = false;

                if (Convert.ToInt32(firstLongInteger.ToString()[0]) > Convert.ToInt32(secondLongInteger.ToString()[0]))
                {
                    for (int i = 1; i < firstLongInteger.Length; i++)
                    {
                        if (Convert.ToInt32(firstLongInteger.ToString()[i]) > Convert.ToInt32(secondLongInteger.ToString()[i]))
                        {
                            smaller = false;
                        }
                        else
                        {
                            smaller = true;
                        }
                        if (smaller == true)
                        {
                            return result = new LargeInteger("0");
                        }
                    }
                }
                else
                {
                    smaller = false;
                }
            }

            if (secondLongInteger.Length == 1 && secondLongInteger.ToString().Equals("1"))
            {
                return result = firstLongInteger;
            }

            LargeInteger multiplicationResult = new LargeInteger();
            LargeInteger multiplyer = new LargeInteger("10");
            LargeInteger subtractionResult = firstLongInteger;
            LargeInteger sumFinalResult = new LargeInteger();

            multiplicationResult = secondLongInteger * multiplyer;

            if (firstLongInteger.Length > secondLongInteger.Length)
            {
                while (subtractionResult.Length >= multiplicationResult.Length )
                {
                    if (subtractionResult.Length==1 && subtractionResult.ToString().Equals("1"))
                    {
                        break;
                    }
                    if (!(subtractionResult.ToString().First() <= multiplicationResult.ToString().First()))
                    {

                        subtractionResult = subtractionResult - multiplicationResult;
                        sumFinalResult += multiplyer;
                    }
                    else
                    {
                        multiplyer = new LargeInteger("1");
                        multiplicationResult = secondLongInteger * multiplyer;
                        subtractionResult = subtractionResult - multiplicationResult;
                        sumFinalResult += multiplyer;
                    }
                }
            }
            else if (firstLongInteger.Length == secondLongInteger.Length)
            {
                do
                {
                    subtractionResult = subtractionResult - secondLongInteger;
                    sumFinalResult += new LargeInteger("1");
                }
                while (subtractionResult.Length == secondLongInteger.Length && subtractionResult.ToString().First() >= secondLongInteger.ToString().First());
            }
            else
            {
                do
                {
                    multiplyer = new LargeInteger("1");
                    multiplicationResult = secondLongInteger * multiplyer;
                    subtractionResult = subtractionResult - multiplicationResult;
                    sumFinalResult += multiplyer;
                }
                while (subtractionResult.Length >= secondLongInteger.Length);
            }


            return sumFinalResult;
        }


        public override string ToString()
        {
           // base.ToString();
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
    }
}