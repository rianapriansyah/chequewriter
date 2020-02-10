using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace codingtest
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("================================");
                Console.WriteLine();
                Console.Write("Your Input : ");
                string input = Console.ReadLine();
                ChequeWriting(input);
                Console.WriteLine();

            }
            while (true);
        }

        static void ChequeWriting(string input)
        {
            if (!isDigit(input))
            {
                Console.WriteLine(">> Only digit is allowed");
            }
            else
            {
                convertToString(input);
            }
        }

        static bool isDigit(string input)
        {
            string pattern = @"\d";
            Regex re = new Regex(pattern);
            return re.IsMatch(input) ? true : false;
        }

        static void convertToString(string input)
        {
            Int64 mainInput;
            int cents;
            //if user inputs with decimal point
            if (input.IndexOf('.') > 0 || (input.IndexOf(',') > 0))
            {
                char[] delimiterChars = { ',', '.' };
                string[] a = input.Split(delimiterChars);

                Int64.TryParse(a[0], out mainInput);
                int.TryParse(a[1], out cents);

                convertToString(input);
            }
            else
            {
                getComponent(input);
            }
        }

        static void getComponent(string input)
        {
            string[] single_digits = new string[]{ "zero", "one", "two",
                                           "three", "four", "five",
                                           "six", "seven", "eight",
                                           "nine"};

            string[] two_digits = new string[]{"", "ten", "eleven", "twelve",
                                       "thirteen", "fourteen",
                                       "fifteen", "sixteen", "seventeen",
                                       "eighteen", "nineteen"};

            string[] tens_multiple = new string[]{"", "", "twenty", "thirty",
                                          "forty", "fifty","sixty",
                                          "seventy", "eighty", "ninety"};

            string[] tens_power = new string[] { "hundred", "thousand" };

            var bucket = new List<String>();

            String t = "";
            var s = new List<String>();

            for (int i = (input.Length - 1); i >= 0; i--)
            {
                String c = input[i].ToString();

                if (!t.Equals(""))
                {
                    t = t.Insert(0, c);
                }
                else
                {
                    t = c;
                }

                if (t.Length == 3)
                {
                    bucket.Insert(0, t);
                    t = "";
                }
            }

            foreach (string item in bucket)
            {
                Console.WriteLine(item);
            }
        }

        static String getUnit(Int64 mainInput)
        {
            String output = "";
            if (mainInput < Math.Pow(10, 1))
            {
                int x = (int)mainInput;
                if (mainInput == 1)
                {
                    output = " DOLLAR";
                }
                else
                {
                    output = " DOLLARS";
                }
            }
            else if (mainInput < Math.Pow(10, 2))
            {
                output = " HUNDRED";
            }
            else if (mainInput < Math.Pow(10, 3))
            {
                output = " THOUSAND";
            }


            return output;
        }


        static String getUpToTensName(int input)
        {

            Dictionary<int, String> tens = new Dictionary<int, string>()
            {
                {0, ""},
                {1, "ONE"},
                {2, "TWO"},
                {3, "THREE"},
                {4, "FOUR"},
                {5, "FIVE"},
                {6, "SIX"},
                {7, "SEVEN"},
                {8, "EIGHT"},
                {9, "NINE"},
                {10, "TEN"},
                {11, "ELEVEN"},
                {12, "TWELVE"},
                {13, "THIRTEEN"},
                {14, "FOURTEEN"},
                {15, "FIFTEEN"},
                {16, "SIXTEEN"},
                {17, "SEVENTEEN"},
                {18, "EIGHTEEN"},
                {19, "NINETEEN"},
                {20, "TWENTY"},
                {30, "THIRTY"},
                {40, "FOURTY"},
                {50, "FIFTY"},
                {60, "SIXTY"},
                {70, "SEVENTY"},
                {80, "EIGHTY"},
                {90, "NINETY"}
            };

            String result = "";
            if (tens.ContainsKey(input))
            {
                tens.TryGetValue(input, out result);
            }

            return result;
        }
    }
}