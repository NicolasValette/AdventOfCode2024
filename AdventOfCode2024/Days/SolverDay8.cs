using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using AdventOfCode2024.Days.Day8;
using AdventOfCode2024.Utilities;
using Toolbox.Datas;

namespace AdventOfCode2024.Days
{
    public class SolverDay8 : Solver
    {
        private Dictionary<char, Stack<Coord2D<long>>> _antennas;
        private List<string> _map;
        public SolverDay8(bool verbose = false)
        {
            _verbose = verbose;
            _antennas = new Dictionary<char, Stack<Coord2D<long>>>();
            ReadInputFile();
            
        }
        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day8.txt");
            _map = new List<string>((fileReader.ReadToEndAndSplit()));
          
            for (int i = 0; i < _map.Count; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    if (!char.IsLetterOrDigit(_map[i][j])) continue;
                    if (_antennas.ContainsKey(_map[i][j]))
                    {
                        _antennas[_map[i][j]].Push(new Coord2D<long>(i, j));
                    }
                    else
                    {
                        Stack<Coord2D<long>> stack = new Stack<Coord2D<long>>();
                        stack.Push(new Coord2D<long>(i, j));
                        _antennas.Add(_map[i][j], stack);
                    }
                }
            }
            
            
            
            
          
            
            var l = _map.Where(line => line.Select(char.IsLetterOrDigit).ToList().Exists(x=> x == true));
 /*
            var s = list.Where(line => line.Select(char.IsLetterOrDigit).Select(char.IsLetterOrDigit).Select(x=>));
           
            var r = list.Where(line => line.Select(char.IsLetterOrDigit).ToList().Exists(x=> x == true)).Select(line => new
            {
                Row = list.IndexOf(line), 
                Col = line.IndexOf(), 
                Frequency = 'a'
            }).ToList();
            
            
            
            var t = list.Where(line => line.
                    Select(char.IsLetterOrDigit).
                    ToList().
                    Exists(x=>x==true)).
                Select(x => new { Row = list.IndexOf(x)
                , Column = x
                .IndexOf
                ('A') })
                .First();
            
            */
            fileReader.Close();
        }

        #region Star1

        public override long GetSolution1Star()
        {
            int n = 0;
            List<Antenna> solution = new List<Antenna>();
            List<Coord2D<long>> solutionCoord = new List<Coord2D<long>>();
            foreach (var element in _antennas)
            {
                while (element.Value.Count > 0)
                {
                    Coord2D<long> currentAntenna = element.Value.Pop();
                    foreach (var antenna in element.Value.ToList())
                    {
                        Console.WriteLine("Evalutate antenna (" + element.Key + ") : " + currentAntenna + " - " + antenna);
                        Coord2D<long> antinode1 =
                            new Coord2D<long>(antenna.X - currentAntenna.X + antenna.X, antenna.Y - currentAntenna.Y + antenna.Y);
                        Coord2D<long> antinode2 = new Coord2D<long>(currentAntenna.X - antenna.X + currentAntenna.X,
                            currentAntenna.Y - antenna.Y + currentAntenna.Y);
                        if ((antinode1.X >= 0 && antinode1.X < _map.Count) && (antinode1.Y >= 0 && antinode1.Y < _map[0].Length))
                        {
                            Antenna antennaToAdd = new Antenna(antinode1, element.Key);
                            if (!solutionCoord.Contains(antinode1) /*&& _map[(int)antinode1.X][(int)antinode1.Y] == '.'*/)
                            {
                                Console.WriteLine("Add antinode1 (" + antennaToAdd+")");
                                solutionCoord.Add(antinode1);
                                solution.Add(new Antenna(antinode1, element.Key));
                                n++;
                            }
                            
                        }

                        if ((antinode2.X >= 0 && antinode2.X < _map.Count) && (antinode2.Y >= 0 && antinode2.Y < _map[0].Length))
                        {
                            Antenna antennaToAdd = new Antenna(antinode2, element.Key);
                            if (!solutionCoord.Contains(antinode2) /*&& _map[(int)antinode2.X][(int)antinode2.Y] == '.'*/)
                            {
                                Console.WriteLine("Add antinode2 (" + antennaToAdd + ")");
                                solutionCoord.Add(antinode2);
                                solution.Add(new Antenna(antinode2, element.Key));
                                n++;
                            }
                        }
                    }
                }
            }

            List<StringBuilder> list = new List<StringBuilder>();
            foreach (var line in _map)
            {
                StringBuilder stringBuilder = new StringBuilder(line);
                list.Add(stringBuilder);   
            }
            foreach (var antenna in solution)
            {
                list[(int)antenna.Coord.X][(int)antenna.Coord.Y] = '#';
            }
            
            foreach (var line in list)
            {
                Console.WriteLine(line.ToString());
            }
            Console.WriteLine("Nombre ajout : " + n);
            return solution.Count;
        }

        #endregion

        #region Star2

        public override long GetSolution2Star()
        {
            ReadInputFile();
            
             int n = 0;
            List<Antenna> solution = new List<Antenna>();
            List<Coord2D<long>> solutionCoord = new List<Coord2D<long>>();
            foreach (var element in _antennas)
            {
                if (element.Value.Count >=2)
                    solutionCoord.AddRange(element.Value.ToList());
                while (element.Value.Count > 0)
                {
                    Coord2D<long> currentAntenna = element.Value.Pop();
                    foreach (var antenna in element.Value.ToList())
                    {
                        Console.WriteLine("Evalutate antenna (" + element.Key + ") : " + currentAntenna + " - " + antenna);
                        long modifierX = 0, modifierY = 0;
                        Coord2D<long> antinode1, antinode2;
                        do
                        {
                            antinode1 =
                                new Coord2D<long>(antenna.X - currentAntenna.X + antenna.X + modifierX, 
                                    antenna.Y - currentAntenna.Y + antenna.Y + modifierY);
                            if ((antinode1.X >= 0 && antinode1.X < _map.Count) && (antinode1.Y >= 0 && antinode1.Y < _map[0].Length))
                            {
                                Antenna antennaToAdd = new Antenna(antinode1, element.Key);
                                if (!solutionCoord.Contains(antinode1) && _map[(int)antinode1.X][(int)antinode1.Y] == '.')
                                {
                                    Console.WriteLine("Add antinode1 (" + antennaToAdd+")");
                                    solutionCoord.Add(antinode1);
                                    solution.Add(new Antenna(antinode1, element.Key));
                                    n++;
                                }
                            }

                            modifierX += antenna.X - currentAntenna.X;
                            modifierY += antenna.Y - currentAntenna.Y;

                        } while ((antinode1.X >= 0 && antinode1.X < _map.Count) && (antinode1.Y >= 0 && antinode1.Y < _map[0].Length));

                        modifierX = 0;
                        modifierY = 0;
                        do
                        {
                            antinode2 = new Coord2D<long>(currentAntenna.X - antenna.X + currentAntenna.X + modifierX,
                                currentAntenna.Y - antenna.Y + currentAntenna.Y + modifierY);
                        
                            Antenna antennaToAdd = new Antenna(antinode2, element.Key);
                            if ((antinode2.X >= 0 && antinode2.X < _map.Count) && (antinode2.Y >= 0 && antinode2.Y < _map[0].Length))
                            {
                                
                                if (!solutionCoord.Contains(antinode2) && _map[(int)antinode2.X][(int)antinode2.Y] == '.')
                                {
                                    Console.WriteLine("Add antinode2 (" + antennaToAdd + ")");
                                    solutionCoord.Add(antinode2);
                                    solution.Add(new Antenna(antinode2, element.Key));
                                    n++;
                                }
                            }
                            modifierX += currentAntenna.X - antenna.X;
                            modifierY += currentAntenna.Y - antenna.Y ;
                            
                        } while ((antinode2.X >= 0 && antinode2.X < _map.Count) && (antinode2.Y >= 0 && antinode2.Y < _map[0].Length));
                      
                       

                     
                    }
                }
            }
            List<StringBuilder> list = new List<StringBuilder>();
            foreach (var line in _map)
            {
                StringBuilder stringBuilder = new StringBuilder(line);
                list.Add(stringBuilder);   
            }
            foreach (var antenna in solution)
            {
                if (list[(int)antenna.Coord.X][(int)antenna.Coord.Y] == '.')
                    list[(int)antenna.Coord.X][(int)antenna.Coord.Y] = '#';
            }
            
            foreach (var line in list)
            {
                Console.WriteLine(line.ToString());
            }
            Console.WriteLine("Nombre ajout : " + n);
            return solutionCoord.Count;
        }

        #endregion
    }
}