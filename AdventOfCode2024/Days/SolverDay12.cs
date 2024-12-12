using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2024.Days.Day12;
using AdventOfCode2024.Utilities;
using Toolbox.Datas;
using Toolbox.Utilities.Algorythms;

namespace AdventOfCode2024.Days
{

    public class SolverDay12 : Solver
    {

        private char[][] _input;
        private List<GardenRegion> _garden = new List<GardenRegion>();
        public SolverDay12(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }
        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day12.txt");
            _input = fileReader.ReadToEndAndSplitInto2DCharArray();

            
            fileReader.Close();
        }

        private string PrintInput()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _input[i].Length; j++)
                {
                    stringBuilder.Append(_input[i][j]);
                }

                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }
        #region Star1

        private GardenRegion CountAndReplaceChar(char c, char replacement)
        {
            GardenRegion region = new GardenRegion();
            
            long count = 0;
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _input[i].Length; j++)
                {
                    if (_input[i][j] != c) continue;
                    if (i - 1 < 0 || (_input[i - 1][j] != c ))
                    {
                        region.Perimeter++;
                        region.NorthBarrier.Add(new Barrier(new Coord2D<int>(i-1, j), new Coord2D<int>(i-1, j)));
                    }
                    
                    if (i + 1 >= _input.Length || (_input[i+1][j] != c ))
                    {
                        region.Perimeter++;
                        region.SouthBarrier.Add(new Barrier(new Coord2D<int>(i+1, j), new Coord2D<int>(i+1, j)));
                    }
                    
                    if (j - 1 < 0 || (_input[i][j-1] != c ))
                    {
                        region.Perimeter++;
                        region.WestBarrier.Add(new Barrier(new Coord2D<int>(i, j-1), new Coord2D<int>(i, j-1)));
                    }
                    
                    if (j + 1 >= _input[i].Length || (_input[i][j+1] != c))
                    {
                        region.Perimeter++;
                        region.EastBarrier.Add(new Barrier(new Coord2D<int>(i, j+1), new Coord2D<int>(i, j+1)));
                    }
                    region.Area++;
                }
            }

            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _input[i].Length; j++)
                {
                    if (_input[i][j] != c) continue;
                    _input[i][j] = replacement;
                }
            }

            return region;
        }
        public override long GetSolution1Star()
        {

            FloodFill<char> floodFill = new FloodFill<char>();
            //Console.WriteLine(PrintInput());
            for (int i = 0; i < _input.Length; i++)
            {
                for (int j = 0; j < _input[i].Length; j++)
                {
                    if (_input[i][j] != '.' && _input[i][j] != '-')
                    {
                        List<Coord2D<int>> barrier = new List<Coord2D<int>>();
                        char seed = _input[i][j];
                        Flood(i, j,ref _input, _input[i][j],'-', ref barrier);
                        //Console.WriteLine(PrintInput());
                        GardenRegion region = CountAndReplaceChar('-', '.');
                        region.Seed = seed;
                        _garden.Add(region);
                    }
                }
            }

            return _garden.Sum(x=>x.Area * x.Perimeter);
        }

        #endregion

        #region Star2

        private bool IsBarrierAdj(Barrier bar1, Barrier bar2)
        {
            return ((bar1.Start.X - 1 == bar2.End.X) && bar1.Start.Y == bar2.End.Y)|| ((bar1.End.X + 1 == bar2.Start.X) && bar1.End.Y == bar2.Start.Y)||
                   ((bar1.Start.Y - 1 == bar2.End.Y) && bar1.Start.X == bar2.End.X)|| ((bar1.End.Y + 1 == bar2.Start.Y) && bar1.End.X == bar2.Start.X);
        }
        private void FusionSide(GardenRegion region)
        {
            for (int i = 0; i <region.NorthBarrier.Count;i++)
            {
                if (i + 1 >= region.NorthBarrier.Count) continue;
                for (int j = i; j < region.NorthBarrier.Count; j++)
                {
                    if (IsBarrierAdj(region.NorthBarrier[i], region.NorthBarrier[j]))
                    {
                        region.NorthBarrier[i].FusionBarrier(region.NorthBarrier[j]);
                            region.NorthBarrier.RemoveAt(j);
                            j = i;
                    }
                }
            }
            for (int i = 0; i <region.SouthBarrier.Count;i++)
            {
                if (i + 1 >= region.SouthBarrier.Count) continue;
                for (int j = i; j < region.SouthBarrier.Count; j++)
                {
                    if (IsBarrierAdj(region.SouthBarrier[i], region.SouthBarrier[j]))
                    {
                        region.SouthBarrier[i].FusionBarrier(region.SouthBarrier[j]);
                        region.SouthBarrier.RemoveAt(j);
                        j = i;
                    }
                }
            }
            for (int i = 0; i <region.EastBarrier.Count;i++)
            {
                if (i + 1 >= region.EastBarrier.Count) continue;
                for (int j = i; j < region.EastBarrier.Count; j++)
                {
                    if (IsBarrierAdj(region.EastBarrier[i], region.EastBarrier[j]))
                    {
                        region.EastBarrier[i].FusionBarrier(region.EastBarrier[j]);
                        region.EastBarrier.RemoveAt(j);
                        j = i;
                    }
                }
            }
            for (int i = 0; i <region.WestBarrier.Count;i++)
            {
                if (i + 1 >= region.WestBarrier.Count) continue;
                for (int j = i; j < region.WestBarrier.Count; j++)
                {
                    if (IsBarrierAdj(region.WestBarrier[i], region.WestBarrier[j]))
                    {
                        region.WestBarrier[i].FusionBarrier(region.WestBarrier[j]);
                        region.WestBarrier.RemoveAt(j);
                        j = i;
                    }
                }
            }
        }
        private long CountSide(int startingRow, int startingCol, char seed)
        {
            List<Coord2D<int>> northSide = new List<Coord2D<int>>();
            int i = startingRow;
            int j = startingCol;
            while (true)
            {
                if (i - 1 < 0 || _input[i][j] != seed)
                {
                    northSide.Add(new Coord2D<int>(i,j));
                }

                do
                {
                    j++;
                    if (j >= _input[i].Length) break;
                } while(i - 1 < 0  || _input[i][j] != seed);

                if (j >= _input[i].Length)
                {
                    j = 0;
                    i++;
                    if (i >= _input.Length) break;
                }
            }

            return 0;
        }
        public void Flood(int startingLine, int startingRow, ref char[][] input, char target, char replacement, ref List<Coord2D<int>> listBarrier)
        {
            // We need to implemant an internal stack to optimize compute time and avoid stack overflow error.
            Stack<(int, int)> stack = new Stack<(int, int)>();
            if (input[startingLine][startingRow].Equals(target))
            {
                stack.Push((startingLine, startingRow));
                while (stack.Count > 0)
                {
                    (int, int) pair = stack.Pop();
                    input[pair.Item1][pair.Item2] = replacement;

                    if (pair.Item1 - 1 >= 0 && input[pair.Item1 - 1][pair.Item2].Equals(target))
                    {
                        stack.Push((pair.Item1 - 1, pair.Item2));
                    }
                    else
                    {
                        listBarrier.Add(new Coord2D<int>(pair.Item1 - 1, pair.Item2));
                    }
                    if (pair.Item1 + 1 < input.Length && input[pair.Item1 + 1][pair.Item2].Equals(target))
                    {
                        stack.Push((pair.Item1 + 1, pair.Item2));
                    }
                    else
                    {
                        listBarrier.Add(new Coord2D<int>(pair.Item1 + 1, pair.Item2));
                    }
                    if (pair.Item2 - 1 >= 0 && input[pair.Item1][pair.Item2 - 1].Equals(target))
                    {
                        stack.Push((pair.Item1, pair.Item2 - 1));
                    }
                    else
                    {
                        listBarrier.Add(new Coord2D<int>(pair.Item1, pair.Item2 - 1));
                    }
                    if (pair.Item2 + 1 < input[pair.Item1].Length && input[pair.Item1][pair.Item2 + 1].Equals(target))
                    {
                        stack.Push((pair.Item1, pair.Item2 + 1));
                    }
                    else
                    {
                        listBarrier.Add(new Coord2D<int>(pair.Item1, pair.Item2 + 1));
                    }
                }
            }
        }
        public override long GetSolution2Star()
        {
            foreach (var region in _garden)
            {
                FusionSide(region);
            }

            return _garden.Sum(x=> x.Area * (x.NorthBarrier.Count + x.SouthBarrier.Count + x.EastBarrier.Count + x.WestBarrier.Count));
        }

        #endregion
    }
}