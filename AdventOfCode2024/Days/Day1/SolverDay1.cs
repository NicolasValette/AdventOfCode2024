using AdventOfCode2024.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days.Day1
{
    internal class SolverDay1
    {
        private List<long> _leftList;
        private List<long> _rightList;
        
        public SolverDay1()
        {
            _leftList = new List<long>();
            _rightList = new List<long>();
            ReadInputFile();
        }
        private void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day1.txt");
            string[] lines = fileReader.ReadToEndAndSplit();
            for (int i=0; i<lines.Length; i++)
            {
                string[] line = lines[i].Split(' ');

                _leftList.Add(Convert.ToInt64(line[0].Trim()));
                _rightList.Add(Convert.ToInt64(line[line.Length-1].Trim()));
            }

            int ij = 2;
        }
        public long GetSolution1Star()
        {
            long solution = 0;
            
            _leftList.Sort();
            _rightList.Sort();
            for (int i = 0;i<_leftList.Count;i++)
            {
                long diff = Math.Abs(_rightList[i] - _leftList[i]);
              
                solution += diff;
            }
            return solution;
        }
        private long CountOccursInRight(long value)
        {
            long occurs = 0;
            for (int i = 0; i<_rightList.Count && value >= _rightList[i];i++)
            {
                if (value == _rightList[i])
                    occurs++;
            }
            return occurs;
        }
        public long GetSolution2Star()
        {
            long solution = 0;
            _leftList.Sort();
            _rightList.Sort();
            for (int i = 0; i<_leftList.Count;i++)
            {
                long occurs = CountOccursInRight(_leftList[i]);
                Console.WriteLine($"{_leftList} occurs {occurs} ");
                solution += _leftList[i] * occurs;
            }
            return solution;
        }

    }
}
