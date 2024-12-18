using System;
using System.Text.RegularExpressions;
using AdventOfCode2024.Days.Day14;
using AdventOfCode2024.Days.Day17;
using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Days
{
    public class SolverDay17 : Solver
    {
        private ComputerProgram _computerProgram;
        private long _regA;
        private long _regB;
        private long _regC;
        private string _prog;
        private const string PATTERN =
            "Register A: (?<RA>[0-9]+)\r\nRegister B: (?<RB>[0-9]+)\r\nRegister C: (?<RC>[0-9]+)\r\n\r\nProgram: (?<PROG>[0-9]([,][0-9]+)*)";
        public SolverDay17(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }
        
        
        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day17.txt");
            _input = fileReader.ReadToEnd();
            fileReader.Close();
            
            Regex regex = new Regex(PATTERN, RegexOptions.IgnoreCase);
            Match currentMatch = regex.Match(_input);

            while (currentMatch.Success)
            {
                _regA = long.Parse(currentMatch.Groups["RA"].Captures[0].ToString());
                _regB = long.Parse(currentMatch.Groups["RB"].Captures[0].ToString());
                _regC = long.Parse(currentMatch.Groups["RC"].Captures[0].ToString());
                _prog = currentMatch.Groups["PROG"].Captures[0].ToString();

                _computerProgram = new ComputerProgram(_regA, _regB, _regC, _prog);
                currentMatch = currentMatch.NextMatch();
            }
            
            Console.WriteLine("EndReading");
           
        }

        #region Star1

        public override long GetSolution1Star()
        {
            //Console.WriteLine(_computerProgram.Run(true));
            return 0;
        }

        #endregion

        #region Star2

        public override long GetSolution2Star()
        {
            long regA = 0;
            while (true)
            {
                ComputerProgram computerProg = new ComputerProgram(regA, _regB, _regC, _prog);
                string output = computerProg.Run();
                if (output.Equals(_prog)) break;
                regA++;
            }
            
            return regA;
        }

        #endregion
    }
}