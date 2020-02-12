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
            string print = "";
            if (!isDigit(input))
            {
                Console.WriteLine(">> Only digit is allowed");
            }
            else
            {
                string mainInput;
                string cents;
                //if user inputs with decimal point
                if (input.IndexOf('.') > 0 || (input.IndexOf(',') > 0))
                {
                    char[] delimiterChars = { ',', '.' };
                    string[] a = input.Split(delimiterChars);

                    if(a.Length > 2 && a[1].Length > 2){
                         Console.WriteLine(">> Input is not in a correct format");
                         return;
                    }
                    mainInput = a[0];
                    cents = a[1];

                    print += getComponent(mainInput);
                    print += " AND ";
                    print += getCents(cents);
                }
                else
                {
                    print += getComponent(input);
                }
            }

            Console.Write(print);
        }

        static bool isDigit(string input)
        {
            string pattern = @"\d";
            Regex re = new Regex(pattern);
            return re.IsMatch(input) ? true : false;
        }

        static string getCents(string input)
        {
            string print = "";
            int cents = int.Parse(input);
            print += getTens(cents);

            if (cents == 1)
            {
                print += " CENT";
            }
            else
            {
                print += " CENTS";
            }

            return print;
        }

        static string getComponent(string input)
        {
            String print = "";

            int num = int.Parse(input);
            if (num >= Math.Pow(10, 2) && num < Math.Pow(10, 3))
            {
                print = getHundred(num);
                print += " DOLLARS";
            }
            else if (num >= Math.Pow(10, 3) && num < Math.Pow(10, 6))
            {
                print += getThousand(num);
                print += " DOLLARS";
            }
            else if (num >= Math.Pow(10, 6) && num < Math.Pow(10, 9))
            {
                print += getMillion(num);
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

            return print;
        }

        static String getMillion(int num)
        {
            String output = "";
            int million = 1000000;
            int left = num / million;
            int right = num % million;
            if (left > 19 && left < million)
            {
                output += getTens(left);
                output += getMillion(right);
            }
            else
            {
                output = unitsMap[left];
                output += " MILLION ";
                output += getThousand(right);
            }

            return output;
        }

        static String getThousand(int num)
        {
            String output = "";
            int thousand = 1000;
            int left = num / thousand;
            int right = num % thousand;

            if (left > 19 && left < thousand)
            {
                if (left > 99)
                {
                    output += getHundred(left);
                    output += getThousand(right);
                }
            }
            else
            {
                output += unitsMap[left];
                output += " THOUSAND ";
                output += getHundred(right);
            }

            return output;
        }

        static String getHundred(int num)
        {
            String hundred = "";
            int left = num / 100;
            int right = num % 100;

            hundred = unitsMap[left];
            hundred += " HUNDRED AND ";

            if (right < 20)
            {
                hundred += unitsMap[right];
            }
            else
            {
                hundred += getTens(right);
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