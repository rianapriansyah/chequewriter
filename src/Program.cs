//rian.apriansyah

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace codingtest
{
    public class Program
    {
        public static List<String> unitsMap = new List<String>() {
                "", "ONE", "TWO",
                "THREE", "FOUR", "FIVE",
                "SIX", "SEVEN", "EIGHT",
                "NINE", "TEN", "ELEVEN",
                "TWELVE", "THIRTEEN", "FOURTEEN",
                "FIFTEEN", "SIXTEEN", "SEVENTEEN",
                "EIGHTEEN", "NINETEEN" };

        public static List<String> tensMap = new List<String>(){
                "", "TEN", "TWENTY",
                "THIRTY", "FORTY", "FIFTY",
                "SIXTY", "SEVENTY", "EIGHTY",
                "NINETY" };

        public static Dictionary<Double, String> tenPowerMaps = new Dictionary<Double, string>(){
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

        public static void Main(string[] args)
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("==============ZZZ==================");
                Console.WriteLine();
                Console.Write("Your Input : ");
                string input = Console.ReadLine();
                string output = ChequeWriting(input);
                Console.WriteLine(output);

            }
            while (true);
        }

        public static string ChequeWriting(string input)
        {
            string print = "";
            if (!isDigit(input))
            {
                return ">> Only digit is allowed";
            }
            else
            {
                Double mainInput;
                string cents;
                if (input.IndexOf('.') > 0 || (input.IndexOf(',') > 0))
                {
                    char[] delimiterChars = { ',', '.' };
                    string[] a = input.Split(delimiterChars);

                    if (a.Length > 2)
                    {
                        return ">> Input is not in a correct format";
                    }
                    else
                    {
                        if (a[1].Length > 2)
                        {
                            return ">> Input is not in a correct format";
                        }
                        else
                        {
                            mainInput = Double.Parse(a[0]);
                            cents = a[1];

                            print += getRecurrenceUnit(mainInput, tenPowerMaps.ElementAt(0).Key, "", 0);
                            print += " AND ";
                            print += getCents(cents);
                        }
                    }
                }
                else
                {
                    Double num = Double.Parse(input);
                    print += getRecurrenceUnit(num, tenPowerMaps.ElementAt(0).Key, "", 0);
                }
            }

            return print;
        }

        public static bool isDigit(string input)
        {
            string pattern = @"\d";
            Regex re = new Regex(pattern);
            return re.IsMatch(input) ? true : false;
        }

        public static string getCents(string input)
        {
            string print = "";
            int cents = int.Parse(input);

            if (cents < 20)
            {
                print += getUnits((int)cents);
            }
            else
            {
                print += getTens((int)cents);
            }

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

        public static string getRecurrenceUnit(Double input, Double power, string outputFromPrev, int i)
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
                return ">> Input is too large";
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
                        if (left < 20)
                        {
                            output += getUnits((int)left);
                        }
                        else
                        {
                            output += getTens((int)left);
                        }

                        output += " ";
                        output += currentUnit;
                    }

                    output += " ";


                    if (right > 999)
                    {
                        output += getRecurrenceUnit(right, currentPower, output, i);
                        return output;
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
                                output += getUnits((int)right);
                            }
                            else
                            {
                                output += getTens((int)right);
                            }
                            output += " DOLLARS";
                        }
                    }
                    return output;
                }
                else if (input < 100)
                {
                    if (input < 20)
                    {
                        output += getUnits((int)input);
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

                    return output;
                }
                else
                {
                    i += 1;
                    output += getRecurrenceUnit(input, currentPower, output, i);
                    return output;
                }
            }
        }

        public static String getHundred(int num)
        {
            String hundred = "";
            int left = num / 100;
            int right = num % 100;

            hundred = getUnits(left);

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
                hundred += getUnits(right);
            }
            else
            {
                hundred += getTens(right);
            }

            return hundred;
        }

        public static string getTens(int num)
        {
            String tens = "";

            tens += tensMap[num / 10];
            if (num % 10 > 0)
            {
                tens += " " + getUnits(num % 10);
            }

            return tens;
        }

        public static string getUnits(int num)
        {
            String units = "";
            units += unitsMap[num];
            return units;
        }
    }
}