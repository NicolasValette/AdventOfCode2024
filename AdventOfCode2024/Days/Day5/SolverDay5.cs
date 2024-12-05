using AdventOfCode2024.Days.Day5;
using AdventOfCode2024.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2024.Days
{
    internal class SolverDay5 : Solver
    {
        private readonly PrintingRules _rules = new PrintingRules();
        private readonly List<List<int>> _updates = new List<List<int>>();
        public SolverDay5(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }

        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day5.txt");

            string line = fileReader.Read();
            while (!string.IsNullOrWhiteSpace(line))
            {
                int[] rule = line.Split('|').Select(x => int.Parse(x)).ToArray();
                _rules.AddRule(rule[0], rule[1]);
                line = fileReader.Read();
            }

            while (!fileReader.EndOfStream)
            {
                line = fileReader.Read();
                _updates.Add(line.Split(',').Select(x => int.Parse(x)).ToList());
            }
            fileReader.Close();
        }
        #region STAR1
        public bool IsUpdateCorrect(List<int> update)
        {
            for (int i = 0; i < update.Count; i++)
            {
                for (int j = 0; j < update.Count; j++)
                {
                    if (j == i) continue;
                    if (i < j)
                    {
                        if (!_rules.IsCorrect(update[i], update[j]))
                        {
                            return false;
                        }
                    }
                    if (i > j)
                    {
                        if (!_rules.IsCorrect(update[j], update[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public override long GetSolution1Star()
        {
            long solution = 0;
            List<List<int>> _correctUpdates = new List<List<int>>();

            foreach (List<int> update in _updates)
            {
                if (IsUpdateCorrect(update))
                {
                    int middle = update.Count / 2;
                    solution += update[middle];
                    if (_verbose)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(string.Join(",", update) + " => " + update[middle]);
                        Console.ResetColor();
                    }
                }
                else
                {
                    if (_verbose)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(string.Join(",", update));
                        Console.ResetColor();
                    }
                }
            }
            return solution;
        }
        #endregion
        #region STAR2
        public bool IsUpdateCorrect2(List<int> update)
        {
           // update.so
            for (int i = 0; i < update.Count; i++)
            {
                for (int j = 0; j < update.Count; j++)
                {
                    if (j == i) continue;
                    if (i < j)
                    {
                        if (!_rules.IsCorrect(update[i], update[j]))
                        {
                            return false;
                        }
                    }
                    if (i > j)
                    {
                        if (!_rules.IsCorrect(update[j], update[i]))
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
        public void CorrectUpdate(List<int> update)
        {

        }
        public override long GetSolution2Star()
        {
            long solution = 0;
            List<List<int>> _correctUpdates = new List<List<int>>();

            foreach (List<int> update in _updates)
            {
                if (!IsUpdateCorrect(update))
                {
                    update.Sort(_rules.Compare);
                    int middle = update.Count / 2;
                    solution += update[middle];
                    
                }
            }
            return solution;
        }
        #endregion
    }
}
