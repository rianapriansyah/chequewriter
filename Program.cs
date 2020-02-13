using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

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

        static Dictionary<Double, String> tenPowerMaps = new Dictionary<Double, string>(){
                {(Double)Math.Pow(10, 21), "SEXTILLION"},
                {(Double)Math.Pow(10, 18), "QUINTILLION"},
                {(Double)Math.Pow(10, 15), "QUADRILLION"},
                {(Double)Math.Pow(10, 12), "TRILLION"},
                {(Double)Math.Pow(10, 9), "BILLION"},
                {(Double)Math.Pow(10, 6), "MILLION"},
                {(Double)Math.Pow(10, 3), "THOUSAND"},
                {(Double)Math.Pow(10, 2), "HUNDRED"},
                {(Double)Math.Pow(10, 1), "TEN"}
            };

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
                Int64 mainInput;
                string cents;
                //if user inputs with decimal point
                if (input.IndexOf('.') > 0 || (input.IndexOf(',') > 0))
                {
                    char[] delimiterChars = { ',', '.' };
                    string[] a = input.Split(delimiterChars);

                    if (a.Length > 2)
                    {
                        Console.WriteLine(">> Input is not in a correct format");
                        return;
                    }
                    mainInput = Int64.Parse(a[0]);
                    cents = a[1];

                    getRecurrenceUnit(mainInput, tenPowerMaps.ElementAt(0).Key, "", 0);
                    print += " AND ";
                    print += getCents(cents);
                }
                else
                {
                    Double num = Double.Parse(input);
                    getRecurrenceUnit(num, tenPowerMaps.ElementAt(0).Key, "", 0);
                    return;
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

        static void getRecurrenceUnit(Double input, Double power, string outputFromPrev, int i)
        {
            String output = "";
            output += outputFromPrev;
            Double maxPower = tenPowerMaps.ElementAt(0).Key;

            Double currentPower = power;

            if (i != 0 && i < tenPowerMaps.Count)
            {
                currentPower = tenPowerMaps.ElementAt(i).Key;
            }

            if (input > maxPower)
            {
                Console.Write(">> Input is too large");
                return;
            }
            else
            {
                if (input >= currentPower && input < power)
                {
                    i += 1;
                    string currentUnit = tenPowerMaps.GetValueOrDefault(currentPower);
                    Double l = input / currentPower;
                    Double r = input % currentPower;

                    Int64 left = (Int64)l;
                    Int64 right = (Int64)r;

                    if (left > 99)
                    {
                        output += getHundred((int)left);
                        output += " ";
                        output += currentUnit;
                    }
                    else
                    {
                        if(left < 20){
                            output += unitsMap[(int)left];
                        }
                        else{
                            output += getTens((int)left);
                        }
                        
                        output += " ";
                        output += currentUnit;
                    }

                    output += " ";


                    if (right > 999)
                    {
                        getRecurrenceUnit(right, currentPower, output, i);
                        return;
                    }
                    else
                    {
                        if (right > 99)
                        {
                            output += getHundred((int)right);
                            output += " DOLLARS";
                        }
                        else
                        {
                            if (right < 20)
                            {
                                output += unitsMap[(int)right];
                            }
                            else
                            {
                                output += getTens((int)right);
                            }
                            output += " DOLLARS";
                        }
                    }

                    Console.Write(output);
                    return;
                }
                else if (input < 100)
                {
                    if (input < 20)
                    {
                        output += unitsMap[(int)input];
                    }
                    else
                    {
                        output += getTens((int)input);
                    }

                    if (input == 1)
                    {
                        output += " DOLLAR";
                    }
                    else
                    {
                        output += " DOLLARS";
                    }

                    Console.Write(output);
                    return;
                }
                else
                {
                    i += 1;
                    getRecurrenceUnit(input, currentPower, output, i);
                    return;
                }
            }
        }

        static String getHundred(int num)
        {
            String hundred = "";
            int left = num / 100;
            int right = num % 100;

            hundred = unitsMap[left];

            if (right == 0)
            {
                hundred += " HUNDRED";
            }
            else
            {
                hundred += " HUNDRED AND ";
            }


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