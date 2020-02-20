//rian.apriansyah

using System;
using System.Linq;

namespace chequewriter.src
{
    public class Program
    {
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

            Double mainInput = 0;
            int cents = 0;

            if (input.Contains(",") || input.Contains("."))
            {
                char[] delimiterChars = { ',', '.' };
                string[] a = input.Split(delimiterChars);

                if (!Function.isDigit(a[0], a[1]))
                {
                    return ">> Only digit and decimal separator are allowed";
                }

                if (a.Length > 2)
                {
                    return ">> Input is not in a correct format";
                }
                else if (a[1].Length > 2)
                {
                    return ">> Input is not in a correct format";
                }
                else
                {

                    Double.TryParse(a[0], out mainInput);
                    int.TryParse(a[1], out cents);

                    if (mainInput == 0 && cents == 0)
                    {
                        return ">> Zero Value";
                    }

                    if (cents != 0)
                    {
                        print += Function.getRecurrenceUnit(mainInput, Function.tenPowerMaps.ElementAt(0).Key, "", 0);
                        print += " AND ";
                        print += Function.getCents(cents);

                        return print;
                    }

                    print += Function.getRecurrenceUnit(mainInput, Function.tenPowerMaps.ElementAt(0).Key, "", 0);
                }
            }
            else
            {
                if (!Function.isDigit(input, ""))
                {
                    return ">> Only digit and decimal separator are allowed";
                }

                Double.TryParse(input, out mainInput);
                if (mainInput == 0)
                {
                    return ">> Zero Value";
                }

                print += Function.getRecurrenceUnit(mainInput, Function.tenPowerMaps.ElementAt(0).Key, "", 0);
            }



            return print;
        }
    }
}