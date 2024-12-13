using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2024.Utilities;
using Toolbox.Datas;

namespace AdventOfCode2024.Days
{
    public class SolverDay13 : Solver
    {
        private string _input;
        List<TwoEquationSystem> _clawMachines = new List<TwoEquationSystem>();

        private const string _PATTERN =
            "Button A: X\\+(?<A>[0-9]+), Y\\+(?<AP>[0-9]+)\r\nButton B: X\\+(?<B>[0-9]+), Y\\+(?<BP>[0-9]+)\r\nPrize: X=(?<C>[0-9]+), Y=(?<CP>[0-9]+)";
        
        public SolverDay13(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }
        
        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day13.txt");
            _input = fileReader.ReadToEnd();

            
            fileReader.Close();
        }

        #region Star1

        public override long GetSolution1Star()
        {
            long totalCost = 0;
            
            Regex regex = new Regex(_PATTERN, RegexOptions.IgnoreCase);

            Match currentMatch = regex.Match(_input);
            int i = 1;
            StringBuilder stringBuilder = new StringBuilder();
            while (currentMatch.Success)
            {
                long a = long.Parse(currentMatch.Groups["A"].Captures[0].ToString());
                long ap = long.Parse(currentMatch.Groups["AP"].Captures[0].ToString());
                long b = long.Parse(currentMatch.Groups["B"].Captures[0].ToString());
                long bp = long.Parse(currentMatch.Groups["BP"].Captures[0].ToString());
                long c = long.Parse(currentMatch.Groups["C"].Captures[0].ToString());
                long cp = long.Parse(currentMatch.Groups["CP"].Captures[0].ToString());

                TwoEquationSystem equation = new TwoEquationSystem(a, ap, b, bp, c, cp);
                _clawMachines.Add(equation);
                if (equation.Resolve(out (long, long) solution))
                {
                    long prize = solution.Item1 * 3 + solution.Item2 * 1;
                    //Console.WriteLine("Solution Machine #"+i+" X = " + solution.Item1 + ", Y = " + solution.Item2);
                    totalCost += prize;
                }
                else
                {
                    //
                    // stringBuilder.AppendLine("Button A: X+" + equation.A + ", Y+" + equation.APrime);
                    // stringBuilder.AppendLine("Button B: X+" + equation.B + ", Y+" + equation.BPrime);
                    // stringBuilder.AppendLine("Prize: X=" + equation.C + ", Y=" + equation.CPrime);
                    // stringBuilder.AppendLine();
                    //
                    //     
                        
                }
                i++;
                currentMatch = currentMatch.NextMatch();
            }

            
            
            // Console.WriteLine("Machine Fausse\n");
            // Console.WriteLine(stringBuilder.ToString());
            
            return totalCost;
        }

        #endregion

        #region Star2

        public override long GetSolution2Star()
        {
            long totalCost = 0;
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var equation in _clawMachines)
            {
                equation.C += 10000000000000;
                equation.CPrime += 10000000000000;
                if (equation.Resolve(out (long, long) solution))
                {
                    long prize = solution.Item1 * 3 + solution.Item2 * 1;
                    totalCost += prize;
                }
                else
                {
                    
                    stringBuilder.AppendLine("Button A: X+" + equation.A + ", Y+" + equation.APrime);
                    stringBuilder.AppendLine("Button B: X+" + equation.B + ", Y+" + equation.BPrime);
                    stringBuilder.AppendLine("Prize: X=" + equation.C + ", Y=" + equation.CPrime);
                    stringBuilder.AppendLine();
                    
                        
                        
                }
            }
            Console.WriteLine("Machine Fausse\n");
            Console.WriteLine(stringBuilder.ToString());
            return totalCost;
        }

        #endregion
    }
}