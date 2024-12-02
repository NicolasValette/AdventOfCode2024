using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days.Day2
{
    internal class Report
    {
        private List<int> _levels;
        private bool _diapener = false;
        public Report(List<int> levels)
        {
            _levels = new List<int>(levels);
        }

        public bool IsSafe()
        {
            if (_levels[0] < _levels[1])
                return IsIncreaseSafely();
            else if (_levels[0] > _levels[1])
                return IsDecreaseSafely();
            else
                return false;
        }
        private bool IsIncreaseSafely()
        {
            
            for (int i=0; i < _levels.Count - 1;i++)
            {
                if (_levels[i] >= _levels[i + 1] || _levels[i+1] - _levels[i] > 3)
                    return false;
            }
            return true;
        }
        private bool IsDecreaseSafely()
        {
            for (int i = 0; i < _levels.Count - 1; i++)
            {
                if (_levels[i] <= _levels[i + 1] || _levels[i] - _levels[i+1] > 3)
                    return false;
            }
            return true;
        }
        public bool IsSafeDiapener()
        {
            if (_levels[0] < _levels[1])
                return IsIncreaseSafelyDiapener();
            else if (_levels[0] > _levels[1])
                return IsDecreaseSafelyDiapener();
            else
                return false;
        }
        private bool IsIncreaseSafelyDiapener()
        {

            for (int i = 0; i < _levels.Count - 1; i++)
            {
                if (_levels[i] >= _levels[i + 1] || _levels[i + 1] - _levels[i] > 3)
                {
                    if (!_diapener)
                    {
                        _levels.RemoveAt(i + 1);
                        i = 0;
                        _diapener = true;
                    }
                    else
                        return false;
                }
            }
            return true;
        }
        private bool IsDecreaseSafelyDiapener()
        {
            for (int i = 0; i < _levels.Count - 1; i++)
            {
                if (_levels[i] <= _levels[i + 1] || _levels[i] - _levels[i + 1] > 3)
                {
                    if (!_diapener)
                    {
                        _levels.RemoveAt(i + 1);
                        i = 0;
                        _diapener = true;
                    }
                    else
                        return false;
                }
            }
            return true;
        }
    }
}
