using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2024.Days.Day14;
using AdventOfCode2024.Utilities;
using Toolbox.Datas;

namespace AdventOfCode2024.Days
{
    public class SolverDay15 : Solver
    {
        private string _moveOrder;
        private char[][] _warehouse;
        private char[][] _wideWarehouse;
        private Coord2D<int> _robot;
        private Coord2D<int> _wideRobot;

        public SolverDay15(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }

        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day15.txt");
            var input = fileReader.ReadAndSplitInto2DList();
            fileReader.Close();
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            List<string> warehouseTemp = new List<string>();
            for (; !string.IsNullOrEmpty(input[i]); i++)
            {
                var ind = input[i].IndexOf('@');
                if (ind != -1)
                {
                    _robot = new Coord2D<int>(i, ind);
                }

                warehouseTemp.Add(input[i]);
            }

            _warehouse = warehouseTemp.Select(x => x.ToCharArray()).ToArray();
            StringBuilder stringBuilderWarehouse = new StringBuilder();
            for (int k = 0; k < _warehouse.Length; k++)
            {
                for (int j = 0; j < _warehouse[k].Length; j++)
                {
                    if (_warehouse[k][j] == '#')
                        stringBuilderWarehouse.Append("##");
                    if (_warehouse[k][j] == '.')
                        stringBuilderWarehouse.Append("..");
                    if (_warehouse[k][j] == 'O')
                        stringBuilderWarehouse.Append("[]");
                    if (_warehouse[k][j] == '@')
                        stringBuilderWarehouse.Append("@.");
                }

                stringBuilderWarehouse.AppendLine();
            }

            _wideWarehouse = stringBuilderWarehouse.ToString().Split('\n').Select(x => x.ToCharArray()).ToArray();

            for (int k = 0; k < _wideWarehouse.Length; k++)
            {
                for (int l = 0; l < _wideWarehouse[k].Length; l++)
                {
                    if (_wideWarehouse[k][l] == '@')
                    {
                        _wideRobot = new Coord2D<int>(k, l);
                    }
                }
            }

            PrintWarehouse(_wideWarehouse);
            //_moveOrder = input[i + 1];
            for (; i < input.Count; i++)
            {
                stringBuilder.Append(input[i]);
            }

            _moveOrder = stringBuilder.ToString();


            // Regex regex = new Regex(_PATTERN, RegexOptions.IgnoreCase);
            // Match currentMatch = regex.Match(_input);
            //
            // while (currentMatch.Success)
            // {
            //     long posX = long.Parse(currentMatch.Groups["PX"].Captures[0].ToString());
            //     long posY = long.Parse(currentMatch.Groups["PY"].Captures[0].ToString());
            //     long velX = long.Parse(currentMatch.Groups["VX"].Captures[0].ToString());
            //     long velY = long.Parse(currentMatch.Groups["VY"].Captures[0].ToString());
            //
            //     Robot robot = new Robot(posX, posY, velX, velY);
            //     _area[robot.Pos.X][robot.Pos.Y]++;
            //     _robots.Add(robot);
            //     currentMatch = currentMatch.NextMatch();
            // }
        }

        private void Move(Directions direction)
        {
            Coord2D<int> startingPosition = _robot;
            Coord2D<int> currentPos = new Coord2D<int>(_robot.X, _robot.Y);
            int modX = 0, modY = 0;
            switch (direction)
            {
                case Directions.NORTH:
                    modX = -1;
                    break;
                case Directions.EAST:
                    modY = 1;
                    break;
                case Directions.SOUTH:
                    modX = 1;
                    break;
                case Directions.WEST:
                    modY = -1;
                    break;
            }

            currentPos.X += modX;
            currentPos.Y += modY;
            if (_warehouse[currentPos.X][currentPos.Y] == '.')
            {
                _warehouse[startingPosition.X][startingPosition.Y] = '.';
                _robot.X = currentPos.X;
                _robot.Y = currentPos.Y;
                _warehouse[_robot.X][_robot.Y] = '@';
            }
            else if (_warehouse[currentPos.X][currentPos.Y] == 'O')
            {
                do
                {
                    currentPos.X += modX;
                    currentPos.Y += modY;
                } while (_warehouse[currentPos.X][currentPos.Y] == 'O');

                if (_warehouse[currentPos.X][currentPos.Y] == '.')
                {
                    _warehouse[startingPosition.X][startingPosition.Y] = '.';
                    _robot.X = startingPosition.X + modX;
                    _robot.Y = startingPosition.Y + modY;
                    _warehouse[_robot.X][_robot.Y] = '@';
                    _warehouse[currentPos.X][currentPos.Y] = 'O';
                }
            }
        }

        private List<long> GetGPSCoordinates()
        {
            List<long> coordinates = new List<long>();
            for (int i = 0; i < _warehouse.Length; i++)
            {
                for (int j = 0; j < _warehouse[i].Length; j++)
                {
                    if (_warehouse[i][j] != 'O') continue;
                    coordinates.Add(100 * i + j);
                }
            }

            return coordinates;
        }

        private void PrintWarehouse(char[][] wareHouseToPrint)
        {
            for (int i = 0; i < wareHouseToPrint.Length; i++)
            {
                for (int j = 0; j < wareHouseToPrint[i].Length; j++)
                {
                    if (wareHouseToPrint[i][j] == '@')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(wareHouseToPrint[i][j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(wareHouseToPrint[i][j]);
                    }
                }

                Console.WriteLine();
            }
        }

        #region Star1

        public override long GetSolution1Star()
        {
            for (int i = 0; i < _moveOrder.Length; i++)
            {
                //PrintWarehouse();
                //Console.WriteLine("Mouvement : " + _moveOrder[i]);
                if (_moveOrder[i] == '^')
                    Move(Directions.NORTH);
                else if (_moveOrder[i] == '>')
                    Move(Directions.EAST);
                else if (_moveOrder[i] == 'v')
                    Move(Directions.SOUTH);
                else
                    Move(Directions.WEST);
            }

            //PrintWarehouse();
            return GetGPSCoordinates().Sum();
        }

        #endregion

        #region Star2

        private bool PushNorth(Coord2D<int> from, bool onlyChecking = false)
        {
            if (_wideWarehouse[from.X + -1][from.Y + 0] == '[' && _wideWarehouse[from.X + -1][from.Y + 0 + 1] == ']')
            {
                if (PushCrate(Directions.NORTH, new Coord2D<int>(from.X + -1, from.Y + 0), onlyChecking) == false)
                    return false;
                // else
                // {
                //     PushCrate(Directions.NORTH, new Coord2D<int>(from.X + -1, from.Y + 0), false);
                // }
            }

            if (_wideWarehouse[from.X + -1][from.Y + 0] == ']' && _wideWarehouse[from.X + -1][from.Y + 0 + 1] == '.')
            {
                if (PushCrate(Directions.NORTH, new Coord2D<int>(from.X + -1, from.Y + 0 - 1), onlyChecking) == false)
                    return false;
                // else
                // {
                //     PushCrate(Directions.NORTH, new Coord2D<int>(from.X + -1, from.Y + 0 - 1), false);
                // }
            }

            if (_wideWarehouse[from.X + -1][from.Y + 0] == '.' && _wideWarehouse[from.X + -1][from.Y + 0 + 1] == '[')
            {
                if (PushCrate(Directions.NORTH, new Coord2D<int>(from.X + -1, from.Y + 0 + 1), onlyChecking) == false) return false;
                // else
                // {
                //     PushCrate(Directions.NORTH, new Coord2D<int>(from.X + -1, from.Y + 0 + 1), false);
                // }
            }

            if (_wideWarehouse[from.X + -1][from.Y + 0] == ']' && _wideWarehouse[from.X + -1][from.Y + 0 + 1] == '[')
            {
                if (PushCrate(Directions.NORTH, new Coord2D<int>(from.X + -1, from.Y + 0 - 1), onlyChecking) == false
                    || PushCrate(Directions.NORTH, new Coord2D<int>(from.X + -1, from.Y + 0 + 1), onlyChecking) == false)
                    return false;
                // else
                // {
                //     PushCrate(Directions.NORTH, new Coord2D<int>(from.X + -1, from.Y + 0 - 1), false);
                //     PushCrate(Directions.NORTH, new Coord2D<int>(from.X + -1, from.Y + 0 + 1), false);
                // }
            }

            if (_wideWarehouse[from.X + -1][from.Y + 0] == '#' || _wideWarehouse[from.X + -1][from.Y + 0 + 1] == '#')
            {
                return false;
            }

            if (_wideWarehouse[from.X + -1][from.Y + 0] == '.' && _wideWarehouse[from.X + -1][from.Y + 0 + 1] == '.')
            {
                if (onlyChecking) return true;
                _wideWarehouse[from.X][from.Y] = '.';
                _wideWarehouse[from.X][from.Y + 1] = '.';
                _wideWarehouse[from.X + -1][from.Y + 0] = '[';
                _wideWarehouse[from.X + -1][from.Y + 1 + 0] = ']';
                return true;
            }

            return true;
        }

        private bool PushEast(Coord2D<int> from, bool onlyChecking = false)
        {
            if (_wideWarehouse[from.X][from.Y + 2] == '[')
            {
                if (PushCrate(Directions.EAST, new Coord2D<int>(from.X, from.Y + 2), onlyChecking) == false) return false;
                // else
                // {
                //     PushCrate(Directions.EAST, new Coord2D<int>(from.X, from.Y + 2), false);
                // }
            }

            if (_wideWarehouse[from.X][from.Y + 2] == '#')
            {
                return false;
            }

            if (_wideWarehouse[from.X][from.Y + 2] == '.')
            {
                if (onlyChecking) return true;
                _wideWarehouse[from.X][from.Y] = '.';
                _wideWarehouse[from.X][from.Y + 1] = '[';
                _wideWarehouse[from.X][from.Y + 2] = ']';
                return true;
            }

            return true;
        }

        private bool PushSouth(Coord2D<int> from, bool onlyChecking = false)
        {
            if (_wideWarehouse[from.X + 1][from.Y] == '[' && _wideWarehouse[from.X + 1][from.Y + 1] == ']')
            {
                if (PushCrate(Directions.SOUTH, new Coord2D<int>(from.X + 1, from.Y + 0), onlyChecking) == false) return false;
                // else
                // {
                //     PushCrate(Directions.SOUTH, new Coord2D<int>(from.X + 1, from.Y + 0), false);
                // }
            }

            if (_wideWarehouse[from.X + 1][from.Y + 0] == ']' && _wideWarehouse[from.X + 1][from.Y + 0 + 1] == '.')
            {
                if (PushCrate(Directions.SOUTH, new Coord2D<int>(from.X + 1, from.Y - 1), onlyChecking) == false) return false;
                // else
                // {
                //     PushCrate(Directions.SOUTH, new Coord2D<int>(from.X + 1, from.Y - 1), false);
                // }
            }

            if (_wideWarehouse[from.X + 1][from.Y + 0] == '.' && _wideWarehouse[from.X + 1][from.Y + 1] == '[')
            {
                if (PushCrate(Directions.SOUTH, new Coord2D<int>(from.X + 1, from.Y + 1), onlyChecking) == false) return false;
                // else
                // {
                //     PushCrate(Directions.SOUTH, new Coord2D<int>(from.X + 1, from.Y + 1), false);
                // }
            }

            if (_wideWarehouse[from.X + 1][from.Y + 0] == ']' && _wideWarehouse[from.X + 1][from.Y + 1] == '[')
            {
                if (PushCrate(Directions.SOUTH, new Coord2D<int>(from.X + 1, from.Y - 1), onlyChecking) == false
                    || PushCrate(Directions.SOUTH, new Coord2D<int>(from.X + 1, from.Y + 1), onlyChecking) == false)
                    return false;
                // else
                // {
                //     PushCrate(Directions.SOUTH, new Coord2D<int>(from.X + 1, from.Y - 1), false);
                //     PushCrate(Directions.SOUTH, new Coord2D<int>(from.X + 1, from.Y + 1), false);
                // }
            }

            if (_wideWarehouse[from.X + 1][from.Y] == '#' || _wideWarehouse[from.X + 1][from.Y + 1] == '#')
            {
                return false;
            }

            if (_wideWarehouse[from.X + 1][from.Y + 0] == '.' && _wideWarehouse[from.X + 1][from.Y + 1] == '.')
            {
                if (onlyChecking) return true;
                _wideWarehouse[from.X][from.Y] = '.';
                _wideWarehouse[from.X][from.Y + 1] = '.';
                _wideWarehouse[from.X + 1][from.Y + 0] = '[';
                _wideWarehouse[from.X + 1][from.Y + 1] = ']';
                return true;
            }

            return true;
        }

        private bool PushWest(Coord2D<int> from, bool onlyChecking = false)
        {
            if (_wideWarehouse[from.X][from.Y - 1] == ']')
            {
                if (PushCrate(Directions.WEST, new Coord2D<int>(from.X, from.Y - 2), onlyChecking) == false) return false;
                // else
                // {
                //     PushCrate(Directions.WEST, new Coord2D<int>(from.X, from.Y - 2), onlyChecking);
                // }
            }

            if (_wideWarehouse[from.X][from.Y - 1] == '#')
            {
                return false;
            }

            if (_wideWarehouse[from.X][from.Y - 1] == '.')
            {
                if (onlyChecking) return true;
                _wideWarehouse[from.X][from.Y + 1] = '.';
                _wideWarehouse[from.X][from.Y] = ']';
                _wideWarehouse[from.X][from.Y - 1] = '[';
                return true;
            }

            return true;
        }

        private bool PushCrate(Directions direction, Coord2D<int> from, bool onlyChecking = false)
        {
            int modX = 0, modY = 0;
            switch (direction)
            {
                case Directions.NORTH:
                    return PushNorth(from, onlyChecking);
                case Directions.EAST:
                    return PushEast(from, onlyChecking);
                case Directions.SOUTH:
                    return PushSouth(from, onlyChecking);
                case Directions.WEST:
                    return PushWest(from, onlyChecking);
            }


            return false;
        }

        private void MoveWide(Directions direction)
        {
            Coord2D<int> startingPosition = _wideRobot;
            Coord2D<int> currentPos = new Coord2D<int>(_wideRobot.X, _wideRobot.Y);
            int modX = 0, modY = 0;
            switch (direction)
            {
                case Directions.NORTH:
                    modX = -1;
                    break;
                case Directions.EAST:
                    modY = 1;
                    break;
                case Directions.SOUTH:
                    modX = 1;
                    break;
                case Directions.WEST:
                    modY = -1;
                    break;
            }

            currentPos.X += modX;
            currentPos.Y += modY;
            if (_wideWarehouse[currentPos.X][currentPos.Y] == '.')
            {
                _wideWarehouse[startingPosition.X][startingPosition.Y] = '.';
                _wideRobot.X = currentPos.X;
                _wideRobot.Y = currentPos.Y;
                _wideWarehouse[_wideRobot.X][_wideRobot.Y] = '@';
            }
            else if (_wideWarehouse[currentPos.X][currentPos.Y] == '[')
            {
                if (PushCrate(direction, new Coord2D<int>(currentPos.X, currentPos.Y), true))
                {
                    PushCrate(direction, new Coord2D<int>(currentPos.X, currentPos.Y));
                    _wideWarehouse[startingPosition.X][startingPosition.Y] = '.';
                    _wideRobot.X = currentPos.X;
                    _wideRobot.Y = currentPos.Y;
                    _wideWarehouse[_wideRobot.X][_wideRobot.Y] = '@';
                }
            }
            else if (_wideWarehouse[currentPos.X][currentPos.Y] == ']')
            {
                if (PushCrate(direction, new Coord2D<int>(currentPos.X, currentPos.Y - 1), true))
                {
                    PushCrate(direction, new Coord2D<int>(currentPos.X, currentPos.Y - 1));
                    _wideWarehouse[startingPosition.X][startingPosition.Y] = '.';
                    _wideRobot.X = currentPos.X;
                    _wideRobot.Y = currentPos.Y;
                    _wideWarehouse[_wideRobot.X][_wideRobot.Y] = '@';
                }
            }
        }

        private List<long> GetGPSWideCoordinates()
        {
            List<long> coordinates = new List<long>();
            for (int i = 0; i < _wideWarehouse.Length; i++)
            {
                for (int j = 0; j < _wideWarehouse[i].Length; j++)
                {
                    if (_wideWarehouse[i][j] != '[') continue;
                    coordinates.Add(100 * i + j);
                }
            }

            return coordinates;
        }

        public override long GetSolution2Star()
        {
            for (int i = 0; i < _moveOrder.Length; i++)
            {
                if (i == 7)
                    Console.WriteLine("BDebug");
                // PrintWarehouse(_wideWarehouse);
                // Console.WriteLine("Mouvement : " + _moveOrder[i]);
                if (_moveOrder[i] == '^')
                    MoveWide(Directions.NORTH);
                else if (_moveOrder[i] == '>')
                    MoveWide(Directions.EAST);
                else if (_moveOrder[i] == 'v')
                    MoveWide(Directions.SOUTH);
                else
                    MoveWide(Directions.WEST);
            }

            PrintWarehouse(_wideWarehouse);
            return GetGPSWideCoordinates().Sum();
        }

        #endregion
    }
}