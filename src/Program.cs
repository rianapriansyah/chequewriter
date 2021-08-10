//rian.apriansyah

using System;

namespace chequewriter.src
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LoopFunction func = new LoopFunction();
            do
            {
                Console.WriteLine();
                Console.WriteLine("Z=================ZZZZ=================Z");
                Console.WriteLine();
                Console.Write("Your Input : ");
                string input = Console.ReadLine();
                if (input.Equals("clear"))
                {
                    Console.Clear();
                    continue;
                }
                string output = func.ChequeWriting(input);
                Console.WriteLine(output);
            }
            while (true);
        }
    }
}