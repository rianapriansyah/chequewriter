using codingtest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace chequewriter.src
{
    public class LoopFunction
    {
        public string ChequeWriting(string input)
        {
            if (!Validator.Validate(input))
            {
                return ">> Input is not in a correct format";
            }
            else
            {
                return PrintNumbers(input);
            }
        }        

        static string PrintNumbers(string str)
        {
            string print = "";

            return print;
        }

        static double Converter(string str)
        {
            double input = double.Parse(str);
        }

        static double ConvertDollars(string str)
        {
            double value = 0d;
            return value;
        }

        static double ConvertCents(string str)
        {
            double value = 0d;
            return value;
        }
    }
}
