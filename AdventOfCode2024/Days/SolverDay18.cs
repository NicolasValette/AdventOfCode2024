using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2024.Utilities;
using Toolbox.Datas;
using Toolbox.Utilities.Algorythms;

namespace AdventOfCode2024.Days
{
    public class SolverDay18 : Solver
    {
        private char[][] _map;
        
        private const int NumberOfBits = 2873;
        private const int SizeX = 71;
        private const int SizeY = 71;
        private Coord2D<int> Start = new Coord2D<int>(0, 0);
        private Coord2D<int> End = new Coord2D<int>(70,70);
        private List<List<int>> _bytes;
        public SolverDay18(bool verbose = false)
        {
            _verbose = verbose;

            _map = new char[SizeX][];
            for (int i = 0; i < SizeX; i++)
            {
                _map[i] = new char[SizeY];
                for (int j = 0; j < SizeY; j++)
                {
                    _map[i][j] = '.';
                }
            }
            ReadInputFile();
        }

        private void PrintPath(List<Coord2D<int>> path)
        {
            foreach (Coord2D<int> coord in path)
            {
                _map[coord.X][coord.Y] = 'O';
            }

            PrintMap();
        }
        private void PrintMap(List<Coord2D<int>> path = null)
        {
            for (int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                {
                    if (_map[i][j] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(_map[i][j]);
                        Console.ResetColor();
                    }
                    else
                        Console.Write(_map[i][j]);
                }
                Console.WriteLine();
            }

            
        }
        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day18.txt");
            _bytes  = fileReader.ReadAndSplitInto2DList().Select(x => x.Split(',').Select(y => int.Parse(y.Trim())).ToList()).ToList();
            fileReader.Close();
            for (int i = 0; i < NumberOfBits; i++)
            {
                Console.WriteLine($"{i}:{_bytes[i][0]},{_bytes[i][1]}");
                _map[_bytes[i][1]][_bytes[i][0]] = '#';
            }
            //PrintMap();
        }

        #region Star1

        public override long GetSolution1Star()
        {
            
            Dijkstra dij = new Dijkstra(_map, '#', '.', Start, End);
            var path = dij.Run();
            if (path == null)
            {
                Console.WriteLine("Aucune solution");
                return -1;
            }

            var c = new Coord2D<int>(1, 0);
            Console.WriteLine(path.Contains(c));
            PrintPath(path);
            return path.Count-1;
        }

        #endregion

        #region Star2

        public override long GetSolution2Star()
        {
            return -1;
            int index = 1140;
            Dijkstra dij = new Dijkstra(_map, '#', '.', Start, End);
            var path = dij.Run();
            for (; index < _bytes.Count; index++)
            {
                Console.WriteLine($"###--- TEST: {index} => {_bytes[index][0]},{_bytes[index][1]} ---###");
                _map[_bytes[index][1]][_bytes[index][0]] = '#';
                var corruptedByte = new Coord2D<int>(_bytes[index][1], _bytes[index][0]);
                if (path.Contains(corruptedByte))
                {
                    Console.WriteLine("Corrupted byte cut the path, searching for new one");
                    dij =new Dijkstra(_map, '#', '.', Start, End);
                    path.Clear();
                    path = dij.Run();
                }
                else
                {
                    continue;
                }
                if (path == null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Aucun chemin trouvé, solution trouvé !");
                    Console.WriteLine($"{_bytes[index][0]},{_bytes[index][1]}");
                    Console.ResetColor();
                    return 0;
                }
               
            }

            return 0;
        }

        #endregion
    }
}