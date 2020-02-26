//rian.apriansyah

using System;

namespace chequewriter.src
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Function func = new Function();
            do
            {
                Console.WriteLine();
                Console.WriteLine("==============ZZZZ==================");
                Console.WriteLine();
                Console.Write("Your Input : ");
                string input = Console.ReadLine();
                string output = func.ChequeWriting(input);
                Console.WriteLine(output);
            }
            while (true);
        }
    }
}