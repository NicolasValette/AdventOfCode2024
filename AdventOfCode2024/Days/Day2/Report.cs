using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days.Day2
{
    internal class Report
    {
        private List<int> _levels;
        private bool _diapener = false;
        public Report(List<int> levels, bool diapener = false)
        {
            _levels = new List<int>(levels);
            _diapener = diapener;
        }
        public override string ToString()
        {
            StringBuilder stream = new StringBuilder();
            stream.Append(_levels[0]);
            for (int i = 1; i < _levels.Count; i++)
            {
                stream.Append(" " + _levels[i]);
            }
            return stream.ToString();

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
                        List<int> left = new List<int>(_levels);
                        left.RemoveAt(i);
                        Report leftReport = new Report(left, true);
                        List<int> right = new List<int>(_levels);
                        right.RemoveAt(i+1);
                        Report rightReport = new Report(right, true);
                        _diapener = true;

                        if (i-1 >=0)
                        {
                            List<int> edge = new List<int>(_levels);
                            edge.RemoveAt(i-1);
                            Report edgeCase = new Report (edge, true);
                            if (edgeCase.IsSafe())
                                return true;
                        }
                        if (leftReport.IsSafe() || rightReport.IsSafe())
                            return true;
                        else
                            return false;

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
                        List<int> left = new List<int>(_levels);
                        left.RemoveAt(i);
                        Report leftReport = new Report(left, true);
                        List<int> right = new List<int>(_levels);
                        right.RemoveAt(i + 1);
                        Report rightReport = new Report(right, true);
                        _diapener = true;

                        if (i - 1 >= 0)
                        {
                            List<int> edge = new List<int>(_levels);
                            edge.RemoveAt(i-1);
                            Report edgeCase = new Report(edge, true);
                            if (edgeCase.IsSafe())
                                return true;
                        }
                        if (leftReport.IsSafe() || rightReport.IsSafe())
                            return true;
                        else
                            return false;
                    }
                    else
                        return false;
                }
            }
            return true;
        }
        public bool IsSafeDiapenerBrute()
        {
            if (_levels[0] < _levels[1])
                return IsIncreaseSafelyDiapenerBrute();
            else if (_levels[0] > _levels[1])
                return IsDecreaseSafelyDiapenerBrute();
            else
                return false;
        }
        public bool IsSafe2()
        {
            if (!IsSafe())
            {
                return Diapener();
            }
            else
                return true;
        }
        private bool Diapener()
        {
            for (int i = 0; i<_levels.Count; i++)
            {
                List<int> list = new List<int>(_levels);
                list.RemoveAt(i);
                Report report = new Report(list, true);
                if (report.IsSafe())
                    return true;
            }
            return false;
        }
        private bool IsIncreaseSafelyDiapenerBrute()
        {

            for (int i = 0; i < _levels.Count - 1; i++)
            {
                if (_levels[i] >= _levels[i + 1] || _levels[i + 1] - _levels[i] > 3)
                {
                    if (!_diapener)
                    {
                        return Diapener();
                    }
                }
            }
            return true;
        }
        private bool IsDecreaseSafelyDiapenerBrute()
        {
            for (int i = 0; i < _levels.Count - 1; i++)
            {
                if (_levels[i] <= _levels[i + 1] || _levels[i] - _levels[i + 1] > 3)
                {
                    if (!_diapener)
                    {
                        return Diapener();
                    }
                }
            }
            return true;
        }
    }
}
