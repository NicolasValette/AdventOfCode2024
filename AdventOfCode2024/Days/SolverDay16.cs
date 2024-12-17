using AdventOfCode2024.Days.Day16;
using AdventOfCode2024.Utilities;
using Toolbox.Datas;

namespace AdventOfCode2024.Days
{
    public class SolverDay16 : Solver
    {
        private char[][] _input;
        private Coord2D<int> _start;
        private Coord2D<int> _end;
        private Reindeer _reindeer;
        public SolverDay16(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }
        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day16.txt");
            _input = fileReader.ReadToEndAndSplitInto2DCharArray();
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _input[i].Length; j++)
                {
                    if (_input[i][j] == 'S')
                    {
                        _start = new Coord2D<int>(i, j);
                    }
                    if (_input[i][j] == 'E')
                    {
                        _end = new Coord2D<int>(i, j);
                    }
                }
            }

            _reindeer = new Reindeer(_start.X, _start.Y, Directions.EAST);
        }

        #region Star1

        public override long GetSolution1Star()
        {
            return base.GetSolution1Star();
        }

        #endregion

        #region Star2

        public override long GetSolution2Star()
        {
            return base.GetSolution2Star();
        }

        #endregion
    }
}