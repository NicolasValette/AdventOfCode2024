using AdventOfCode2024.Days.Day2;
using AdventOfCode2024.Days.Day3;
using AdventOfCode2024.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    internal class SolverDay3 : Solver
    {
        private bool _verbose;
        private string _prog;
        private const string MULPATTERN = "mul\\((?<value1>[0-9]+),(?<value2>[0-9]+)\\)";
        private const string DOPATTERN = "do\\(\\)";
        private const string DONTPATTERN = "don't\\(\\)";

        public SolverDay3(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }

       

        public override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day3.txt");
            _prog = fileReader.ReadToEnd();
            fileReader.Close();
        }

        public override long GetSolution1Star()
        {
            long solution = 0;
            Regex multRegex = new Regex(MULPATTERN, RegexOptions.IgnoreCase);

            Match currentMatchMult = multRegex.Match(_prog);
            while (currentMatchMult.Success)
            {
                long value1 = long.Parse(currentMatchMult.Groups["value1"].Captures[0].ToString());
                
                long value2 = long.Parse(currentMatchMult.Groups["value2"].Captures[0].ToString());
                long tempValue = value1 * value2;
                solution += tempValue;
                if (_verbose)
                    Console.WriteLine($"{value1} * {value2} = {tempValue}");
                
                currentMatchMult = currentMatchMult.NextMatch();
            }

            return solution;
        }
        public override long GetSolution2Star()
        {
            long solution = 0;
            Regex multRegex = new Regex(MULPATTERN, RegexOptions.IgnoreCase);
            Regex doRegex = new Regex(DOPATTERN, RegexOptions.IgnoreCase);
            Regex dontRegex = new Regex(DONTPATTERN, RegexOptions.IgnoreCase);

            Match currentMatchDo = doRegex.Match(_prog);
            Match currentMatchDont = dontRegex.Match(_prog);

            EnabledRange enabledRange = new EnabledRange();

            int lastInstInd = 0;
            bool isDo = true;
            bool start = true;
            while (currentMatchDo.Success || currentMatchDont.Success)
            {
                if (isDo)
                {
                    int nexInstInd = (currentMatchDont.Success)?currentMatchDont.Index:_prog.Length;
                    if (nexInstInd - lastInstInd < 0)
                        Console.WriteLine("??");
                    enabledRange.AddDoRange(lastInstInd, nexInstInd);
                    if (start)
                        start = false;
                    else
                    {
                        do
                        {
                            currentMatchDo = currentMatchDo.NextMatch();
                        } while (currentMatchDo.Success && currentMatchDo.Index < nexInstInd);
                    }
                    lastInstInd = nexInstInd + 1;
                    isDo = false;
                }
                else
                {
                    int nexInstInd = (currentMatchDo.Success) ? currentMatchDo.Index : _prog.Length;
                    if (nexInstInd - lastInstInd < 0)
                        Console.WriteLine("??");
                    enabledRange.AddDontRange(lastInstInd, nexInstInd);
                    if (start)
                        start = false;
                    else
                    {
                        do
                        {
                            currentMatchDont = currentMatchDont.NextMatch();
                        } while (currentMatchDont.Success && currentMatchDont.Index < nexInstInd);
                    }
                    lastInstInd = nexInstInd + 1;
                    isDo = true;
                }

            }

            Match currentMatchMult = multRegex.Match(_prog);
            while (currentMatchMult.Success)
            {
                if (!enabledRange.IsDo(currentMatchMult.Index))
                    currentMatchMult = currentMatchMult.NextMatch();
                else
                {
                    long value1 = long.Parse(currentMatchMult.Groups["value1"].Captures[0].ToString());

                    long value2 = long.Parse(currentMatchMult.Groups["value2"].Captures[0].ToString());
                    long tempValue = value1 * value2;
                    solution += tempValue;
                    if (_verbose)
                        Console.WriteLine($"{value1} * {value2} = {tempValue}");

                    currentMatchMult = currentMatchMult.NextMatch();
                }
            }
            return solution;
        }


    }
}
