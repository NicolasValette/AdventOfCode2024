using AdventOfCode2024.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Datas;
using Toolbox.Utilities.Algorythms;

namespace AdventOfCode2024.Days
{
    internal class SolverDay4 : Solver
    {
        private List<string> _letters;
        public SolverDay4(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }


        public override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day4.txt");
            _letters = fileReader.ReadAndSplitInto2DList();
            fileReader.Close();
        }
        #region STAR1
        private bool IsXMASInDir(int row, int col, Directions dir)
        {
            switch(dir)
            {
                case Directions.NORTH:
                    if (row - 3 < 0) return false;
                    if ((_letters[row - 1][col] == 'M') && (_letters[row - 2][col] == 'A') && (_letters[row - 3][col] == 'S')) 
                    {
                        if (_verbose) Console.WriteLine($"XMAS found in ({row}-{col}) in dir : {dir}");
                        return true; 
                    }
                    return false;
                case Directions.NORTHEAST:
                    if ((row - 3 < 0) || (col + 3 >= _letters[0].Length)) return false;
                    if ((_letters[row - 1][col+1] == 'M') && (_letters[row - 2][col+2] == 'A') && (_letters[row - 3][col+3] == 'S'))
                    {
                        if (_verbose) Console.WriteLine($"XMAS found in ({row}-{col}) in dir : {dir}");
                        return true;
                    }
                    return false;
                case Directions.EAST:
                    if (col + 3 >= _letters[0].Length) return false;
                    if ((_letters[row][col + 1] == 'M') && (_letters[row][col + 2] == 'A') && (_letters[row][col + 3] == 'S'))
                    {
                        if (_verbose) Console.WriteLine($"XMAS found in ({row}-{col}) in dir : {dir}");
                        return true;
                    }
                    return false;
                case Directions.SOUTHEAST:
                    if ((row + 3 >= _letters.Count) || (col + 3 >= _letters[0].Length)) return false;
                    if ((_letters[row + 1][col + 1] == 'M') && (_letters[row + 2][col + 2] == 'A') && (_letters[row + 3][col + 3] == 'S'))
                    {
                        if (_verbose) Console.WriteLine($"XMAS found in ({row}-{col}) in dir : {dir}");
                        return true;
                    }
                    return false;
                case Directions.SOUTH:
                    if (row + 3 >= _letters.Count) return false;
                    if ((_letters[row + 1][col] == 'M') && (_letters[row + 2][col] == 'A') && (_letters[row + 3][col] == 'S'))
                    {
                        if (_verbose) Console.WriteLine($"XMAS found in ({row}-{col}) in dir : {dir}");
                        return true;
                    }
                    return false;
                case Directions.SOUTHWEST:
                    if ((row + 3 >= _letters.Count) || (col - 3 < 0)) return false;
                    if ((_letters[row + 1][col - 1] == 'M') && (_letters[row + 2][col - 2] == 'A') && (_letters[row + 3][col - 3] == 'S'))
                    {
                        if (_verbose) Console.WriteLine($"XMAS found in ({row}-{col}) in dir : {dir}");
                        return true;
                    }
                    return false;
                case Directions.WEST:
                    if (col - 3 < 0) return false;
                    if ((_letters[row][col - 1] == 'M') && (_letters[row][col - 2] == 'A') && (_letters[row][col - 3] == 'S'))
                    {
                        if (_verbose) Console.WriteLine($"XMAS found in ({row}-{col}) in dir : {dir}");
                        return true;
                    }
                    return false;
                case Directions.NORTHWEST:
                    if ((row - 3 < 0) || (col - 3 < 0)) return false;
                    if ((_letters[row - 1][col - 1] == 'M') && (_letters[row - 2][col - 2] == 'A') && (_letters[row - 3][col - 3] == 'S'))
                    {
                        if (_verbose) Console.WriteLine($"XMAS found in ({row}-{col}) in dir : {dir}");
                        return true;
                    }
                    return false;
            }
            return false;
        }
        private int HowManyXMAS(int row, int col)
        {
            int nbOccurs = 0;
            if (IsXMASInDir(row, col, Directions.NORTH))
            {
                nbOccurs++;
            }
            if (IsXMASInDir(row, col, Directions.NORTHEAST))
            {
                nbOccurs++;
            }
            if (IsXMASInDir(row, col, Directions.EAST))
            {
                nbOccurs++;
            }
            if (IsXMASInDir(row, col, Directions.SOUTHEAST))
            {
                nbOccurs++;
            }
            if (IsXMASInDir(row, col, Directions.SOUTH))
            {
                nbOccurs++;
            }
            if (IsXMASInDir(row, col, Directions.SOUTHWEST))
            {
                nbOccurs++;
            }
            if (IsXMASInDir(row, col, Directions.WEST))
            {
                nbOccurs++;
            }
            if (IsXMASInDir(row, col, Directions.NORTHWEST))
            {
                nbOccurs++;
            }
            return nbOccurs;
        }

        public override long GetSolution1Star()
        {
            long solution = 0;
            for (int i=0; i < _letters.Count; i++)
            {
                for (int j=0; j < _letters[i].Length; j++)
                {

                    if (_letters[i][j] == 'X')
                        solution += HowManyXMAS(i, j);
                }
            }


            return solution;
        }
        #endregion
        #region STAR2

        public bool IsCrossMas(int row, int col)
        {
            if (row - 1 < 0 || col - 1 < 0 || row + 1 >= _letters.Count || col + 1 >= _letters[row].Length) return false;
            /**
             * M - M
             * - A -
             * S - S
             * **/
            if (_letters[row - 1][col-1] == 'M' && _letters[row - 1][col + 1] == 'M' && _letters[row + 1][col - 1] == 'S' && _letters[row + 1][col + 1] == 'S')
            {
                return true;
            }
            /**
             * M - S
             * - A -
             * M - S
             * **/
            if (_letters[row - 1][col - 1] == 'M' && _letters[row - 1][col + 1] == 'S' && _letters[row + 1][col - 1] == 'M' && _letters[row + 1][col + 1] == 'S')
            {
                return true;
            }
            /**
             * S - S
             * - A -
             * M - M
             * **/
            if (_letters[row - 1][col - 1] == 'S' && _letters[row - 1][col + 1] == 'S' && _letters[row + 1][col - 1] == 'M' && _letters[row + 1][col + 1] == 'M')
            {
                return true;
            }
            /**
             * S - M
             * - A -
             * S - M
             * **/
            if (_letters[row - 1][col - 1] == 'S' && _letters[row - 1][col + 1] == 'M' && _letters[row + 1][col - 1] == 'S' && _letters[row + 1][col + 1] == 'M')
            {
                return true;
            }
            return false;
        }
        public override long GetSolution2Star()
        {
            long solution = 0;
            for (int i = 0; i < _letters.Count; i++)
            {
                for (int j = 0; j < _letters[i].Length; j++)
                {

                    if (_letters[i][j] == 'A' && IsCrossMas(i, j))
                        solution++;
                }
            }


            return solution;
        }
        #endregion
    }
}
