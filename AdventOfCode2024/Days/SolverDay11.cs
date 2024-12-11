using System.Collections.Generic;
using System.Linq;
using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Days
{
    public class SolverDay11 : Solver
    {
        private List<string> _stones;
        public SolverDay11(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }

        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day11.txt");
            _stones = fileReader.ReadToEndAndSplit(' ');
            fileReader.Close();
        }

        #region Star1

        private void ComputeNewStonesList(ref Dictionary<long, long> stones)
        {
            foreach (var stoneValue in stones.ToList())
            {
                stones[stoneValue.Key]-= stoneValue.Value;
                if (stones[stoneValue.Key] == 0)
                    stones.Remove(stoneValue.Key);
                if (stoneValue.Key == 0)
                {
                    if (!stones.ContainsKey(1))
                        stones.Add(1,0);
                    stones[1]+= stoneValue.Value;;
                }
                else if (stoneValue.Key.ToString().Length % 2 == 0)
                {
                    var leftValue = long.Parse(stoneValue.Key.ToString().Substring(0, stoneValue.Key.ToString().Length / 2));
                    var rightValue = long.Parse(stoneValue.Key.ToString().Substring((stoneValue.Key.ToString().Length / 2)));
                    
                    if (!stones.ContainsKey(leftValue))
                        stones.Add(leftValue,0);
                    stones[leftValue]+= stoneValue.Value;
                    if (!stones.ContainsKey(rightValue))
                        stones.Add(rightValue,0);
                    stones[rightValue]+=stoneValue.Value;;
                }
                else
                {
                    long value = stoneValue.Key * 2024;
                    if (!stones.ContainsKey(value))
                        stones.Add(value,0);
                    stones[value]+= stoneValue.Value;
                }
            }

        }
        
        public override long GetSolution1Star()
        {
            int numberOfBlink = 25;
            int blink = 0;
            Dictionary<long, long> stones = _stones.ToDictionary<string, long, long>(long.Parse, t => 1);
            while (blink < numberOfBlink)
            {
                ComputeNewStonesList(ref stones);
                blink++;
            }

            return stones.Values.Sum();
        }

        #endregion

        #region Star2

        public override long GetSolution2Star()
        {
            int numberOfBlink = 75;
            int blink = 0;
            Dictionary<long, long> stones = _stones.ToDictionary<string, long, long>(long.Parse, t => 1);
            while (blink < numberOfBlink)
            {
                ComputeNewStonesList(ref stones);
                blink++;
            }

            return stones.Values.Sum();
        }

        #endregion
    }
}