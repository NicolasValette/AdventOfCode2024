using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AdventOfCode2024.Days.Day14;
using AdventOfCode2024.Utilities;
using Toolbox.Datas;

namespace AdventOfCode2024.Days
{
    public class SolverDay14 : Solver
    {
        private const int Width = 101;
        private const int Height = 103;
        private int[][] _area = new int [Width][];
        private List<Robot> _robots = new List<Robot>();

        private const string _PATTERN = "p=(?<PX>[-]?[0-9]+),(?<PY>[-]?[0-9]+) v=(?<VX>[-]?[0-9]+),(?<VY>[-]?[0-9]+)";
        private long _second = 0;

        public SolverDay14(bool verbose = false)
        {
            _verbose = verbose;
            for (int i = 0; i < _area.Length; i++)
            {
                _area[i] = new int[Height];
            }

            ReadInputFile();
        }

        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day14.txt");
            _input = fileReader.ReadToEnd();
            fileReader.Close();

            Regex regex = new Regex(_PATTERN, RegexOptions.IgnoreCase);
            Match currentMatch = regex.Match(_input);

            while (currentMatch.Success)
            {
                long posX = long.Parse(currentMatch.Groups["PX"].Captures[0].ToString());
                long posY = long.Parse(currentMatch.Groups["PY"].Captures[0].ToString());
                long velX = long.Parse(currentMatch.Groups["VX"].Captures[0].ToString());
                long velY = long.Parse(currentMatch.Groups["VY"].Captures[0].ToString());

                Robot robot = new Robot(posX, posY, velX, velY);
                _area[robot.Pos.X][robot.Pos.Y]++;
                _robots.Add(robot);
                currentMatch = currentMatch.NextMatch();
            }
        }

        private void PrintArea(bool showQuadrant = false)
        {
           
            Console.WriteLine("*******************************");
            for (int i = 0; i < _area[0].Length; i++)
            {
                if (showQuadrant)
                {
                    if (i == _area[0].Length / 2)
                    {
                        Console.WriteLine();
                        continue;
                    }
                }

                for (int j = 0; j < _area.Length; j++)
                {
                    if (showQuadrant)
                    {
                        if (j == _area.Length / 2)
                        {
                            Console.Write(' ');
                            continue;
                        }
                    }

                    if (_area[j][i] == 0)
                    {
                        Console.Write('.');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(_area[j][i]);
                        Console.ResetColor();
                    }
                }

                Console.Write('\n');
            }

            Console.WriteLine("*******************************");
            Console.WriteLine("second : " + _second);
        }

        #region Star1

        private void MoveRobotsForSeconds(int seconds)
        {
            foreach (var robot in _robots)
            {
                _area[robot.Pos.X][robot.Pos.Y]--;

                long positionX = (robot.Pos.X + seconds * robot.Vel.X) % Width;
                if (positionX < 0)
                    positionX += Width;
                long positionY = (robot.Pos.Y + seconds * robot.Vel.Y) % Height;
                if (positionY < 0)
                    positionY += Height;

                robot.UpdatePosition(new Coord2D<long>(positionX, positionY));

                _area[robot.Pos.X][robot.Pos.Y]++;
            }
        }

        public long GetSafetyFactor()
        {
            long quadNE = 0, quadSE = 0, quadNW = 0, quadSW = 0;
            
            //Console.WriteLine("QUAD NW");
            for (int i = 0; i < Height / 2; i++)
            {
                for (int j = 0; j < Width / 2; j++)
                {
                    quadNW += _area[j][i];
                    //Console.Write(_area[j][i]);
                }
                //Console.WriteLine();
            }
            //Console.WriteLine("QUAD NE");
            for (int i = 0; i < Height / 2; i++)
            {
                for (int j = Width / 2 + 1; j < Width; j++)
                {
                    quadNE += _area[j][i];
                    //Console.Write(_area[j][i]);
                }
                //Console.WriteLine();
            }
            //Console.WriteLine("QUAD SE");
            for (int i = Height / 2+ 1; i < Height; i++)
            {
                for (int j = 0; j < Width / 2; j++)
                {
                    quadSE += _area[j][i];
                    //Console.Write(_area[j][i]);
                }
                //Console.WriteLine();
            }
            //Console.WriteLine("QUAD SW");
            for (int i = Height / 2+ 1; i < Height; i++)
            {
                for (int j = Width / 2 + 1; j < Width; j++)
                {
                    quadSW += _area[j][i];
                    //Console.Write(_area[j][i]);
                }
                //Console.WriteLine();
            }
            //Console.WriteLine("quad NE=" + quadNE + " quad NW=" + quadNW+" quad SE="+quadSE+" quad SW="+quadSW);
            return quadNE * quadNW * quadSE * quadSW;
        }

        public override long GetSolution1Star()
        {
            return -1;
            //PrintArea();
            MoveRobotsForSeconds(100);

            //PrintArea(true);
            return GetSafetyFactor();
        }

        #endregion

        #region Star2

        public bool IsOnlyPosUnique()
        {
            for (int i = 0; i < _area.Length; i++)
            {
                for (int j = 0; j < _area[i].Length; j++)
                {
                    if (_area[i][j] > 1)
                        return false;
                }
            }
            return true;
        }
        public override long GetSolution2Star()
        {
            while (true)
            {
                _second++;
                MoveRobotsForSeconds(1);
                if (IsOnlyPosUnique())
                    PrintArea();
                
            }
            return base.GetSolution2Star();
        }

        #endregion
    }
}