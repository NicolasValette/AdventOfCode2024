using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Days
{
    public class SolverDay19 : Solver
    {
        private List<string> _towels;
        private List<string> _designs;

        private Dictionary<string, long> _memoizeDesign = new Dictionary<string, long> { [string.Empty] = 1L };
        public SolverDay19(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }

        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day19.txt");
            var tempInput = fileReader.ReadToEnd().Split('\n');
            _towels = tempInput[0].Split(',').Select(x => x.Trim()).ToList();
            _designs = new List<string>();
            for (int i = 2; i < tempInput.Length; i++)
            {
                _designs.Add(tempInput[i].Trim());
            }
            fileReader.Close();
        }

        #region Star1

        private void Init()
        {
            for (int i = 0; i < _towels.Count; i++)
            {
                _memoizeDesign.Add(_towels[i], 1);
            }
        }

        private bool IsDesignPossible2(string design)
        {
            if (_memoizeDesign.ContainsKey(design) && _memoizeDesign[design]>0) return true;

            if (design.Length == 1)
            {
                if (!_memoizeDesign.ContainsKey(design))
                    _memoizeDesign.Add(design, 0);
                return false;
            }

            for (int i=1;i<design.Length;i++)
            {
                string prevDesign = design.Substring(0, i);
                string newDesign = design.Substring(i);
                bool prev = _memoizeDesign.TryGetValue(prevDesign, out var value) ? value>0 : IsDesignPossible2(prevDesign);
                bool newD = _memoizeDesign.TryGetValue(newDesign, out var value2) ? value2>0 : IsDesignPossible2(newDesign);
                
                if (prev && newD)
                {
                    if (!_memoizeDesign.ContainsKey(design))
                        _memoizeDesign.Add(design, 1);
                    else
                    {
                        _memoizeDesign[design]++;
                    }
                    //return true;
                }
            }

            
            if (!_memoizeDesign.ContainsKey(design))
                _memoizeDesign.Add(design, 0);
            return _memoizeDesign[design]>0;
        }
        private long IsDesignPossible(string design)
        {
            if (_memoizeDesign.TryGetValue(design, out var valueMemo))
            {
                return valueMemo;
            }

            // long n = 0;
            // foreach (var pattern in _towels)
            // {
            //     if (design.StartsWith(pattern))
            //     {
            //         n += IsDesignPossible(design.Substring(pattern.Length));
            //     }
            // }

            // _memoizeDesign[design] = n;
            
            _memoizeDesign[design] = _towels
                .Where(design.StartsWith)
                .Sum(pattern=>IsDesignPossible(design.Substring(pattern.Length)));
            return _memoizeDesign[design];
            //
            // for (int i=1;i<design.Length;i++)
            // {
            //     string prevDesign = design.Substring(0, i);
            //     string newDesign = design.Substring(i);
            //     bool prev = _memoizeDesign.TryGetValue(prevDesign, out var value) ? value>0 : IsDesignPossible2(prevDesign);
            //     bool newD = _memoizeDesign.TryGetValue(newDesign, out var value2) ? value2>0 : IsDesignPossible2(newDesign);
            //     
            //     if (prev && newD)
            //     {
            //         if (!_memoizeDesign.ContainsKey(design))
            //             _memoizeDesign.Add(design, 1);
            //         else
            //         {
            //             _memoizeDesign[design]++;
            //         }
            //         //return true;
            //     }
            // }
            //
            //
            // if (!_memoizeDesign.ContainsKey(design))
            //     _memoizeDesign.Add(design, 0);
            // return _memoizeDesign[design]>0;
        }
        public override long GetSolution1Star()
        {
            //Init();
            long solution = 0;
            foreach (var design in _designs)
            {
                Console.Write(design + " => ");
                long rep = IsDesignPossible(design);
                if (rep > 0) solution++;
                Console.ForegroundColor = rep>0 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.Write(rep);
                Console.ResetColor();
                Console.WriteLine();                
            }
            Console.WriteLine();
            return solution;
        }

        #endregion

        #region Star2

        public override long GetSolution2Star()
        {
            return _memoizeDesign
                .Where(x=>_designs.Contains(x.Key.ToString()))
                .Sum(k=>k.Value);
        }

        #endregion
    }
}