using AdventOfCode2024.Days.Day1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Salut");
            SolverDay1 day1 = new SolverDay1();

            Console.WriteLine("Resolution 1 étoiles");
            long solution = day1.GetSolution2Star();
            Console.WriteLine("solution : " + solution);

            Console.WriteLine("");
            Console.WriteLine("Enter to quit");
            Console.Read();
        }
    }
}
