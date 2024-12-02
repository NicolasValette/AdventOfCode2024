using AdventOfCode2024.Days;
using AdventOfCode2024.Days.Day2;
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
            ISolver day = new SolverDay2();

            #region Solution 1 étoile
            Console.WriteLine("####################");
            Console.WriteLine("Resolution 1 étoiles");
            long solution = day.GetSolution1Star();
            Console.WriteLine("solution : " + solution);
            Console.WriteLine("####################");
            Console.WriteLine("");
            #endregion

            #region Solution 2 étoiles
            Console.WriteLine("####################");
            Console.WriteLine("Resolution 2 étoiles");
            long solution2 = day.GetSolution2Star();
            Console.WriteLine("solution : " + solution2);
            Console.WriteLine("####################");            
            Console.WriteLine("");
            #endregion

            Console.WriteLine("Enter to quit");
            Console.Read();
        }
    }
}
