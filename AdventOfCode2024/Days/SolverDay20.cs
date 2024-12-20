using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using AdventOfCode2024.Utilities;
using Toolbox.Datas;
using Toolbox.Utilities.Algorythms;

namespace AdventOfCode2024.Days
{
    public class SolverDay20 : Solver
    {
        private const int MaxCheat = 6;
        private char[][] _map;
        private Coord2D<int> _start;
        private Coord2D<int> _end;
        public SolverDay20(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }

        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day20.txt");
            _map = fileReader.ReadToEndAndSplitInto2DCharArray();
            for (int i = 0; i < _map.Length; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    if (_map[i][j] == 'S')
                    {
                        _start = new Coord2D<int>(i, j);
                    }

                    if (_map[i][j] == 'E')
                    {
                        _end = new Coord2D<int>(i, j);
                    }
                }
            }

            _map[_start.X][_start.Y] = '.';
            _map[_end.X][_end.Y] = '.';
            fileReader.Close();
        }

        #region Star1

        private List<Directions> CanCut(Coord2D<int> from)
        {
            List<Directions> directions = new List<Directions>();
            if (_map[from.X-1][from.Y] == '#' && from.X - 2 >= 0)
            {
                if (_map[from.X - 2][from.Y] == '.')
                {
                    directions.Add(Directions.NORTH);
                }
            }

            if (_map[from.X+1][from.Y] == '#' && from.X + 2 < _map.Length)
            {
                if (_map[from.X + 2][from.Y] == '.')
                {
                    directions.Add(Directions.SOUTH);
                }
            }

            if (_map[from.X][from.Y-1] == '#' && from.Y - 2 >= 0)
            {
                if (_map[from.X ][from.Y-2] == '.')
                {
                    directions.Add(Directions.WEST);
                }
            }

            if (_map[from.X][from.Y+1] == '#' && from.Y + 2 < _map[from.X].Length)
            {
                if (_map[from.X ][from.Y+2] == '.')
                {
                    directions.Add(Directions.EAST);
                }
            }
            
            return directions;
        }

        private bool IsCoordValid(Coord2D<int> coord)
        {
            return (coord.X >= 0 && coord.X < _map.Length && coord.Y >= 0 && coord.Y < _map[0].Length);
        }
        public override long GetSolution1Star()
        {
            long solution = 0;
            Dictionary<int, int> _times = new Dictionary<int, int>();
            Dijkstra dij = new Dijkstra(_map, '#', '.', _start, _end);
            List<Coord2D<int>> path = dij.Run();
            var listPath = path.Select(x => x.ToString()).ToList();
           Console.WriteLine(path.Count);
            List<string> visited = new List<string>();
            foreach (var point in path)
            {
                List<Directions> cutDirection = CanCut(point);
                if (cutDirection == null) continue;

                foreach (var dir in cutDirection)
                {
                    var coord = dir switch
                    {
                        Directions.NORTH => new Coord2D<int>(point.X-2, point.Y),
                        Directions.EAST => new Coord2D<int>(point.X, point.Y+2),
                        Directions.SOUTH => new Coord2D<int>(point.X+2, point.Y),
                        Directions.WEST => new Coord2D<int>(point.X, point.Y-2),
                        _ => new Coord2D<int>(point.X, point.Y)
                    };
                    
                    if (!IsCoordValid(coord)) 
                        continue;
                    // if (visited.Contains(coord.ToString())) 
                    //     continue;

                    if (_map[coord.X][coord.Y] == '.' &&  listPath.IndexOf(point.ToString()) < listPath.IndexOf(coord.ToString()))
                    {
                        int timeSaved = listPath.IndexOf(coord.ToString()) -listPath.IndexOf(point.ToString());
                        if (!_times.ContainsKey(timeSaved-2)) _times.Add(timeSaved-2,0);
                        _times[timeSaved-2]++;
                        //Console.WriteLine("raccourci trouvé " + point.ToString() + "-" + dir +", temps gagné : " + (timeSaved - 2));
                        if (timeSaved - 2 >= 100) solution++;
                        visited.Add(coord.ToString());
                    }
                }
            }


            // foreach (var element in _times.OrderBy(x => x.Key))
            // {
            //     Console.WriteLine($"{element.Value} path(s) that saves {element.Key}");
            // }
            //
            return solution;
        }

        #endregion

        #region Star2

        List<Coord2D<int>> GetPositionAfterCheat(Coord2D<int> point, List<string> originalPath)
        {
            List<Coord2D<int>> positions = new List<Coord2D<int>>();

            for (int i = point.X - MaxCheat; i < point.X + MaxCheat; i++)
            {
                if (point.X + i < 0 || point.X + i >= _map.Length)
                    continue;
                for (int j = point.Y - MaxCheat; j < point.Y + MaxCheat; j++)
                {
                    if (point.Y + j < 0 || point.Y + j >= _map[point.X].Length) 
                        continue;
                    if (Math.Abs(i) + Math.Abs(j) > MaxCheat) 
                        continue;
                    if (_map[point.X + i][point.Y + j] == '.')
                    {
                        Coord2D<int> pos = new Coord2D<int>(point.X + i, point.Y + j);
                        if (Math.Abs(i) + Math.Abs(j) != originalPath.IndexOf(pos.ToString()))
                        {
                            positions.Add(new Coord2D<int>(point.X + i, point.Y + j));
                        }
                    }
                }
            }

            return positions;    
        }
        public override long GetSolution2Star()
        {
            long solution = 0;
            Dictionary<int, int> _times = new Dictionary<int, int>();
            Dijkstra dij = new Dijkstra(_map, '#', '.', _start, _end);
            List<Coord2D<int>> path = dij.Run();
            var listPath = path.Select(x => x.ToString()).ToList();
            Console.WriteLine(path.Count);
            List<string> visited = new List<string>();
            foreach (var point in path)
            {
                List<Coord2D<int>> positions = GetPositionAfterCheat(point, listPath);
                if (positions.Count == 0) continue;

                foreach (var pos in positions)
                {

                    if (_map[pos.X][pos.Y] == '.' && listPath.IndexOf(point.ToString()) < listPath.IndexOf(pos.ToString()))
                    {
                        int timeSaved = listPath.IndexOf(pos.ToString()) - listPath.IndexOf(point.ToString());
                       
                        if (timeSaved - 2 >= 50)
                        {
                            
                            if (!_times.ContainsKey(timeSaved - 2)) _times.Add(timeSaved - 2, 0);
                                _times[timeSaved - 2]++;
                            solution++;
                        }
                       
                    }
                }

            }


            foreach (var element in _times.OrderBy(x => x.Key))
            {
                Console.WriteLine($"{element.Value} path(s) that saves {element.Key}");
            }

            return solution;
        }

        #endregion
    }
}