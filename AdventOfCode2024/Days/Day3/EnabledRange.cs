using System.Collections.Generic;
using Toolbox.Datas;

namespace AdventOfCode2024.Days.Day3
{
    internal class EnabledRange
    {
        public List<Range> _doRange;
        public List<Range> _dontRange;
        public EnabledRange()
        {
            _doRange = new List<Range>();
            _dontRange = new List<Range>();
        }
        public void AddDoRange(int i, int j)
        {
            Range range = new Range(i, j);
            _doRange.Add(range);
        }
        public void AddDontRange(int i, int j)
        {
            Range range = new Range(i, j);
            _dontRange.Add(range);
        }
        /// <summary>
        /// Tell if the instruction is do or don't
        /// </summary>
        /// <param name="index">index in the program</param>
        /// <returns>-1 if don't, 1 if do, 0 if error</returns>
        public int DoOrDont(int index)
        {
            foreach (Range range in _doRange)
            {
                if (range.IsInRange(index))
                {
                    return 1;
                }
            }
            foreach (Range range in _dontRange)
            {
                if (range.IsInRange(index))
                {
                    return -1;
                }
            }
            return 1; //because start is do
        }
        public bool IsDo(int index)
        {
            return DoOrDont(index) == 1;
        }
        
    }
}
