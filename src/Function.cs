using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace chequewriter.src
{
    public class Function
    {
        static List<string> unitsMap = new List<string>() {
                "", "ONE", "TWO",
                "THREE", "FOUR", "FIVE",
                "SIX", "SEVEN", "EIGHT",
                "NINE", "TEN", "ELEVEN",
                "TWELVE", "THIRTEEN", "FOURTEEN",
                "FIFTEEN", "SIXTEEN", "SEVENTEEN",
                "EIGHTEEN", "NINETEEN" };

        static List<string> tensMap = new List<string>(){
                "", "TEN", "TWENTY",
                "THIRTY", "FORTY", "FIFTY",
                "SIXTY", "SEVENTY", "EIGHTY",
                "NINETY" };

        static Dictionary<double, string> tenPowerMaps = new Dictionary<double, string>(){
                {(double)Math.Pow(10, 21), "SEXTILLION"},
                {(double)Math.Pow(10, 18), "QUINTILLION"},
                {(double)Math.Pow(10, 15), "QUADRILLION"},
                {(double)Math.Pow(10, 12), "TRILLION"},
                {(double)Math.Pow(10, 9), "BILLION"},
                {(double)Math.Pow(10, 6), "MILLION"},
                {(double)Math.Pow(10, 3), "THOUSAND"},
                {(double)Math.Pow(10, 2), "HUNDRED"},
                {(double)Math.Pow(10, 1), "TEN"}
            };

        public static bool IsValid(string input)
        {
            if (input.Contains("-"))
            {
                return false;
            }

            if (input.Contains(",") || input.Contains("."))
            {
                char[] delimiterChars = { ',', '.' };
                string[] a = input.Split(delimiterChars);

                if (a.Length > 2)
                {
                    return false;
                }

                if (ValidateDollarValue(a[0]) && ValidateCentsValue(a[1]))
                {
                    return true;
                }
                return false;
            }

            if (ValidateDollarValue(input) && ValidateCentsValue(""))
            {
                return true;
            }

            return false;
        }
        static bool ValidateDollarValue(string mainInput)
        {
            double x = 0;
            double maxPower = tenPowerMaps.ElementAt(0).Key;
            if (mainInput == "")
            {
                return true;
            }

            try
            {
                x = double.Parse(mainInput);

                if (x >= maxPower)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    return false;
                }
            }

            return true;
        }

        static bool ValidateCentsValue(string cents)
        {
            int x = 0;

            if (cents == "")
            {
                return true;
            }

            try
            {
                x = int.Parse(cents);

                if(x >99){
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message != "")
                {
                    return false;
                }
            }

            return true;
        }

        static string getCents(int cents)
        {
            string print = "";

            if (cents < 20)
            {
                print += getUnits(cents);
            }
            else
            {
                print += getTens(cents);
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

        public static string getRecurrenceUnit(double input, double power, string outputFromPrev, int i)
        {
            string output = "";
            output += outputFromPrev;
            double maxPower = tenPowerMaps.ElementAt(0).Key;

            double currentPower = power;

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
                    double l = input / currentPower;
                    double r = input % currentPower;

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

                    if(right != 0){
                        output += "AND ";
                    }

                    if (right > 999)
                    {
                        output = getRecurrenceUnit(right, currentPower, output, i);
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
                    output = getRecurrenceUnit(input, currentPower, output, i);
                    return output;
                }
            }
        }

        static string getHundred(int num)
        {
            string hundred = "";
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

        static string getTens(int num)
        {
            string tens = "";

            tens += tensMap[num / 10];
            if (num % 10 > 0)
            {
                tens += " " + getUnits(num % 10);
            }

            return tens;
        }

        static string getUnits(int num)
        {
            string units = "";
            units += unitsMap[num];
            return units;
        }

        static string tidyUpThestring(string input)
        {
            string pattern = "\\s+";
            string replacement = " ";
            Regex rx = new Regex(pattern);
            input = rx.Replace(input, replacement);
            return input;
        }

        public string ChequeWriting(string input)
        {
            string print = "";

            double mainInput = 0;
            int cents = 0;

            if (input.Contains(",") || input.Contains("."))
            {
                if (!IsValid(input))
                {
                    return ">> Input is not in a correct format";
                }
                else
                {
                    if (input.Contains("."))
                    {
                        input = input.Replace(".", ",");
                    }

                    double x = double.Parse(input);
                    x = Math.Round(x, 2);

                    input = string.Format("{0:0.00}", x);

                    char[] delimiterChars = { ',', '.' };
                    string[] a = input.Split(delimiterChars);

                    double.TryParse(a[0], out mainInput);
                    int.TryParse(a[1], out cents);

                    if (mainInput == 0 && cents == 0)
                    {
                        return ">> Zero Value";
                    }

                    if (mainInput != 0)
                    {
                        print += getRecurrenceUnit(mainInput, tenPowerMaps.ElementAt(0).Key, "", 0);
                        if (cents != 0)
                        {
                            print += " AND ";
                            print += Function.getCents(cents);
                        }
                        print = tidyUpThestring(print);
                        return print;
                    }
                    else
                    {
                        print += Function.getCents(cents);
                        print = tidyUpThestring(print);
                        return print;
                    }
                }
            }
            else
            {
                if (!IsValid(input))
                {
                    return ">> Input is not in a correct format";
                }

                double.TryParse(input, out mainInput);
                if (mainInput == 0)
                {
                    return ">> Zero Value";
                }

                print += Function.getRecurrenceUnit(mainInput, Function.tenPowerMaps.ElementAt(0).Key, "", 0);
            }
            print = tidyUpThestring(print);
            return print;
        }
    }
}