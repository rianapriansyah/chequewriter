using System;
using System.Text.RegularExpressions;

namespace codingtest
{
    class Program
    {
        static void Main(string[] args)
        {
            do{
                Console.WriteLine();
                Console.WriteLine("================================");
                Console.WriteLine();
                Console.Write("Your Input : ");
                string input = Console.ReadLine();
                string output = ChequeWriting(input);
                Console.WriteLine();
                Console.WriteLine(">> " + output);
            }
            while(true);
        }

        static string ChequeWriting(string input){
            string output = "";
            if(!isDigit(input)){
                output = "Only digit is allowed";
            }
            else{
                output = input;
            }

            return output;
        }

        static bool isDigit(string input){
            string pattern = @"\d";
            Regex re = new Regex(pattern);
            return re.IsMatch(input) ?  true : false;
        }
    }
}
