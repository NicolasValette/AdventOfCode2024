using AdventOfCode2024.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2024.Days.Day2
{
    internal class SolverDay2 : ISolver
    {
        private List<Report> _reportList;
        private bool _verbose = false;
        public SolverDay2(bool verbose = false)
        {
            _reportList = new List<Report>();
            _verbose = verbose;
            ReadInputFile();
        }

        public long GetSolution1Star()
        {
            long solution = 0;
            for (int i = 0;i<_reportList.Count;i++)
            {
                if (_reportList[i].IsSafe())
                { 
                    solution++;
                    if (_verbose) Console.WriteLine($"Report n#{i} is safe");
                }
            }
            return solution;
        }

        public long GetSolution2Star()
        {
                    
            long solution = 0;
            for (int i = 0; i < _reportList.Count; i++)
            {
                if (_reportList[i].IsSafe2())
                {
                    solution++;
                    if (_verbose)
                    {
                        StringBuilder strb = new StringBuilder();
                        strb.Append($"line {i + 1} : ");
                        strb.Append(_reportList[i].ToString());
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(strb.ToString());
                    }
                }
                else
                {
                    if (_verbose)
                    {
                        StringBuilder strb = new StringBuilder();
                        strb.Append($"line {i + 1} : ");
                        strb.Append(_reportList[i].ToString());
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(strb.ToString());
                    }
                }
            }
            Console.ResetColor();
            return solution;
        }

        public void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day2.txt");
            string line = fileReader.Read();
            while (line != null)
            {
                
                var reportLine = line.Split(' ').Select(x=> int.Parse(x)).ToList();
                Report rep = new Report(reportLine);
                _reportList.Add(rep);
                line = fileReader.Read();
            }

            Console.WriteLine("FinishReading");
        }

    }
}
