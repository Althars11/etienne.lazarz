using System;
using System.IO;

namespace Tiny42sh
{
    class Program
    {
        static void Main(string[] args)
        {
            string mycommand;
            do
            {
                Console.Write("Tiny42sh $ ");
                mycommand = Console.ReadLine();
                Console.WriteLine();
            } while (Execution.execute_commad(Interpreter.parse_input(mycommand)) != -1);
        }
    }
}
