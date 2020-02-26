using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace chequewriter.src
{
    public class Function
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

        public static bool isValid(string input)
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
            Double x = 0;
            Double maxPower = tenPowerMaps.ElementAt(0).Key;
            if (mainInput == "")
            {
                return true;
            }

            try
            {
                x = Double.Parse(mainInput);

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

        static String getHundred(int num)
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

        static string getTens(int num)
        {
            String tens = "";

            tens += tensMap[num / 10];
            if (num % 10 > 0)
            {
                tens += " " + getUnits(num % 10);
            }

            return tens;
        }

        static string getUnits(int num)
        {
            String units = "";
            units += unitsMap[num];
            return units;
        }

        static string tidyUpTheString(string input)
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

            Double mainInput = 0;
            int cents = 0;

            if (input.Contains(",") || input.Contains("."))
            {
                if (!Function.isValid(input))
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

                    input = String.Format("{0:0.00}", x);

                    char[] delimiterChars = { ',', '.' };
                    string[] a = input.Split(delimiterChars);

                    Double.TryParse(a[0], out mainInput);
                    int.TryParse(a[1], out cents);

                    if (mainInput == 0 && cents == 0)
                    {
                        return ">> Zero Value";
                    }

                    if (mainInput != 0)
                    {
                        print += Function.getRecurrenceUnit(mainInput, Function.tenPowerMaps.ElementAt(0).Key, "", 0);
                        if (cents != 0)
                        {
                            print += " AND ";
                            print += Function.getCents(cents);
                        }
                        print = tidyUpTheString(print);
                        return print;
                    }
                    else
                    {
                        print += Function.getCents(cents);
                        print = tidyUpTheString(print);
                        return print;
                    }
                }
            }
            else
            {
                if (!Function.isValid(input))
                {
                    return ">> Input is not in a correct format";
                }

                Double.TryParse(input, out mainInput);
                if (mainInput == 0)
                {
                    return ">> Zero Value";
                }

                print += Function.getRecurrenceUnit(mainInput, Function.tenPowerMaps.ElementAt(0).Key, "", 0);
            }
            print = tidyUpTheString(print);
            return print;
        }
    }
}