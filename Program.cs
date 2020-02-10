using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace codingtest
{
    class Program
    {
        static List<String> unitsMap = new List<String>() {
                "", "ONE", "TWO",
                "THREE", "FOUR", "FIVE",
                "SIX", "SEVEN", "EIGHT",
                "NINE", "TEN", "ELEVEN",
                "TWELVE", "THIRTEEN", "FOURTEEN",
                "FIFTEEN", "SIXTEEN", "SEVENTEEN",
                "EIGHTEEN", "NINETEEN" };

        static List<String> tensMap = new List<String>(){
                "", "TEN", "TWENTY",
                "THIRTY", "FORTY", "FIFTY",
                "SIXTY", "SEVENTY", "EIGHTY",
                "NINETY" };
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
            String print = "";

            int num = int.Parse(input);
            if (num >= Math.Pow(10, 2) && num < Math.Pow(10, 3))
            {
                print = getHundred(num);
                print += " DOLLARS";
            }
            else if (num >= Math.Pow(10, 3) && num < Math.Pow(10, 5))
            {
                print += getThousand(num);
                print += " DOLLARS";
            }
            else if (num >= Math.Pow(10, 5) && num < Math.Pow(10, 7))
            {
                int million = 100000;
                int a = num / million;
                print = unitsMap[a];
                print += " MILLION ";
                print += getThousand(num % million);
                print += " DOLLARS";
            }
            else
            {
                if (num > 19 && num < 100)
                {
                    print = getTens(num);
                }
                else
                {
                    print = unitsMap[num];
                }

                if (num == 1)
                {
                    print += " DOLLAR";
                }
                else
                {
                    print += " DOLLARS";
                }
            }
            Console.Write(print);
        }

        

        static String getThousand(int num)
        {
            String output = "";
            int thousand = 1000;
            int a = num / thousand;
            if (a > 19 && a < 1000)
            {
                output += getTens(a);
                output += getThousand(num%thousand);
            }
            else
            {
                output += unitsMap[a];
                output += " THOUSAND ";
                output += getHundred(num % thousand);
            }

            return output;
        }

        static String getHundred(int num)
        {
            String hundred = "";
            int a = num / 100;
            hundred = unitsMap[a];
            hundred += " HUNDRED AND ";
            int b = num % 100;
            if (b < 20)
            {
                hundred += unitsMap[b];
            }
            else
            {
                hundred += getTens(b);
            }

            return hundred;
        }

        static String getTens(int num)
        {
            String tens = "";

            tens += tensMap[num / 10];
            if (num % 10 > 0)
            {
                tens += " " + unitsMap[num % 10];
            }

            return tens;
        }
    }
}