using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Datas;

namespace AdventOfCode2024.Days.Day5
{
    internal class PrintingRules : IComparer<int>
    {
        private List<Coord2D<int>> _printingOrder;

        public PrintingRules()
        {
            _printingOrder = new List<Coord2D<int>>();
        }

        public void AddRule(int firstPage, int  secondPage)
        {
            Coord2D<int> rule = new Coord2D<int>(firstPage, secondPage);
            _printingOrder.Add(rule);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>-1 : a gauche / 1 : a droite</returns>
        public int Compare(int x, int y)
        {
            foreach (Coord2D<int> rule in _printingOrder)
            {
                if ((rule.X == x) && (rule.Y == y))
                {
                    return -1;
                }
                if ((rule.Y == x) && (rule.X == y))
                {
                    return 1;
                }
            }
            return 0;
        }

        public bool IsCorrect (int left, int right)
        {
            foreach (Coord2D<int> rule in _printingOrder)
            {
                if ((rule.X == right) && (rule.Y == left))
                {
                    return false;
                }
            }
            return true;
        }
      
    }
}
