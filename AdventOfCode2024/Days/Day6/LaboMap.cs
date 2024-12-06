using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolbox.Datas;

namespace AdventOfCode2024.Days.Day6
{
    public class LaboMap
    {
        private List<string> _laboMap;
        private Coord2D<int> _guardCoord;
        private Directions _currentDir;
        private List<List<Dictionary<Directions, bool>>> _visited;

        public LaboMap(List<string> map, Coord2D<int> guard)
        {
            _laboMap = map;
            _guardCoord = guard;
            _currentDir = Directions.NORTH;
            StringBuilder stringBuilder = new StringBuilder(_laboMap[_guardCoord.X]);
            stringBuilder[_guardCoord.Y] = '.';
            _laboMap[_guardCoord.X] = stringBuilder.ToString();
            _visited = new List<List<Dictionary<Directions, bool>>>();
            for (int i = 0; i < _laboMap.Count; i++)
            {
                _visited.Add(new List<Dictionary<Directions, bool>>());
                for (int j = 0; j < _laboMap[i].Length; j++)
                {
                    _visited[i].Add(new Dictionary<Directions, bool>());
                    _visited[i][j].Add((Directions.EAST), false);
                    _visited[i][j].Add((Directions.WEST), false);
                    _visited[i][j].Add((Directions.NORTH), false);
                    _visited[i][j].Add((Directions.SOUTH), false);
                }
            }
        }
        
        public void WriteInConsole(int x, int y)
        {
            for (int i = 0; i < _laboMap.Count; i++)
            {
                for (int j = 0; j < _laboMap[i].Length; j++)
                {
                    if (i == x && j == y)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write('O');
                        Console.ResetColor();
                    }
                    else if (_laboMap[i][j] == '-' || _laboMap[i][j] == '|' || _laboMap[i][j] == '+')
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(_laboMap[i][j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(_laboMap[i][j]);
                    }
                }
                Console.Write("\n");
            }
        }

        public bool IsSomethingClose(int x, int y)
        {
            if ((_laboMap[x][y] == '-' || _laboMap[x][y] == '|' || _laboMap[x][y] == '+') ||
                ((x-1 >= 0) && (_laboMap[x-1][y] == '-' || _laboMap[x-1][y] == '|' || _laboMap[x-1][y] == '+')) ||
                ((x+1 < _laboMap.Count) && (_laboMap[x+1][y] == '-' || _laboMap[x+1][y] == '|' || _laboMap[x+1][y] == '+')) ||
                ((y-1 >= 0)) && ((_laboMap[x][y-1] == '-' || _laboMap[x][y-1] == '|' || _laboMap[x][y-1] == '+')) ||
                ((y+1 < _laboMap[x].Length)) && ((_laboMap[x][y+1] == '-' || _laboMap[x][y+1] == '|' || _laboMap[x][y+1] == '+')))
                return true;
            return false;
        }
        private Directions GetDirectionOfGuardMove()
        {
            if (_laboMap[_guardCoord.X][_guardCoord.Y] == '^')
            {
                return Directions.NORTH;
            }

            if (_laboMap[_guardCoord.X][_guardCoord.Y] == '<')
            {
                return Directions.WEST;
            }

            if (_laboMap[_guardCoord.X][_guardCoord.Y] == '>')
            {
                return Directions.EAST;
            }
            else // =='V'
            {
                return Directions.SOUTH;
            }
        }

        public bool Move()
        {
            StringBuilder strb;
            switch (GetDirectionOfGuardMove())
            {
                case Directions.NORTH:
                    strb = new StringBuilder(_laboMap[_guardCoord.X]);
                    if (_guardCoord.X - 1 < 0)
                    {
                        strb[_guardCoord.Y] = 'X';
                        _laboMap[_guardCoord.X] = strb.ToString();
                        return false;
                    }

                    if (_laboMap[_guardCoord.X - 1][_guardCoord.Y] != '#')
                    {
                        strb[_guardCoord.Y] = 'X';
                        StringBuilder strbN = new StringBuilder(_laboMap[_guardCoord.X - 1]);
                        _laboMap[_guardCoord.X] = strb.ToString();
                        strbN[_guardCoord.Y] = '^';
                        _laboMap[_guardCoord.X - 1] = strbN.ToString();
                        _guardCoord.X--;
                    }
                    else //Turn Right
                    {
                        strb[_guardCoord.Y] = '>';
                        _laboMap[_guardCoord.X] = strb.ToString();
                    }

                    return true;
                case Directions.EAST:
                    strb = new StringBuilder(_laboMap[_guardCoord.X]);
                    if (_guardCoord.Y + 1 >= _laboMap[_guardCoord.X].Length)
                    {
                        strb[_guardCoord.Y] = 'X';
                        _laboMap[_guardCoord.X] = strb.ToString();
                        return false;
                    }

                    if (strb[_guardCoord.Y + 1] != '#')
                    {
                        strb[_guardCoord.Y] = 'X';
                        _laboMap[_guardCoord.X] = strb.ToString();
                        strb[_guardCoord.Y + 1] = '>';
                        _laboMap[_guardCoord.X] = strb.ToString();
                        _guardCoord.Y++;
                    }
                    else // Turn right
                    {
                        strb[_guardCoord.Y] = 'v';
                    }

                    _laboMap[_guardCoord.X] = strb.ToString();
                    return true;
                case Directions.SOUTH:
                    strb = new StringBuilder(_laboMap[_guardCoord.X]);
                    if (_guardCoord.X + 1 >= _laboMap.Count)
                    {
                        strb[_guardCoord.Y] = 'X';
                        _laboMap[_guardCoord.X] = strb.ToString();
                        return false;
                    }

                    if (_laboMap[_guardCoord.X + 1][_guardCoord.Y] != '#')
                    {
                        strb[_guardCoord.Y] = 'X';
                        StringBuilder strbS = new StringBuilder(_laboMap[_guardCoord.X + 1]);
                        _laboMap[_guardCoord.X] = strb.ToString();
                        strbS[_guardCoord.Y] = 'v';
                        _laboMap[_guardCoord.X + 1] = strbS.ToString();
                        _guardCoord.X++;
                    }
                    else //Turn Right
                    {
                        strb[_guardCoord.Y] = '<';
                        _laboMap[_guardCoord.X] = strb.ToString();
                    }

                    return true;
                case Directions.WEST:
                    strb = new StringBuilder(_laboMap[_guardCoord.X]);
                    if (_guardCoord.Y - 1 < 0)
                    {
                        strb[_guardCoord.Y] = 'X';
                        _laboMap[_guardCoord.X] = strb.ToString();
                        return false;
                    }

                    if (strb[_guardCoord.Y - 1] != '#')
                    {
                        strb[_guardCoord.Y] = 'X';
                        strb[_guardCoord.Y - 1] = '<';
                        _laboMap[_guardCoord.X] = strb.ToString();
                        _guardCoord.Y--;
                    }
                    else // Turn right
                    {
                        strb[_guardCoord.Y] = '^';
                    }

                    _laboMap[_guardCoord.X] = strb.ToString();
                    return true;
            }

            return false;
        }

        private (int, int) GetModifier(Directions dir)
        {
            int modifierRow, modifierCol;
            switch (dir)
            {
                case Directions.NORTH:
                    modifierRow = -1;
                    modifierCol = 0;
                    break;
                case Directions.EAST:
                    modifierRow = 0;
                    modifierCol = +1;
                    break;
                case Directions.SOUTH:
                    modifierRow = +1;
                    modifierCol = 0;
                    break;
                default:
                    modifierRow = 0;
                    modifierCol = -1;
                    break;
            }

            return (modifierRow, modifierCol);
        }
        
        private bool IsOutside()
        {
            return _guardCoord.X < 0 || _guardCoord.X >= _laboMap.Count || _guardCoord.Y < 0 || _guardCoord.Y >= _laboMap[_guardCoord.X].Length;
        }

        private bool IsObstacle()
        {
            if (_laboMap[_guardCoord.X][_guardCoord.Y] == '#')
            {
                switch (_currentDir)
                {
                    case Directions.EAST:
                    {
                        _guardCoord.Y--;
                        break;
                    }
                    case Directions.NORTH:
                    {
                        _guardCoord.X++;
                        break;
                    }
                    case Directions.WEST:
                    {
                        _guardCoord.Y++;
                        break;
                    }
                    default:
                    {
                        _guardCoord.X--;
                        break;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private void TurnRight()
        {
            switch (_currentDir)
            {
                case Directions.EAST:
                {
                    _guardCoord.X++;
                    _currentDir = Directions.SOUTH;
                    break;
                }
                case Directions.NORTH:
                {
                    _guardCoord.Y++;
                    _currentDir = Directions.EAST;
                    break;
                }
                case Directions.WEST:
                {
                    _guardCoord.X--;
                    _currentDir = Directions.NORTH;
                    break;
                }
                default:
                {
                    _guardCoord.Y--;
                    _currentDir = Directions.WEST;
                    break;
                }
            }
        }

        /// <summary>
        /// Move until find loop or leave
        /// </summary>
        /// <returns> 1 if move, 0 if leave, -1 if loop</returns>
        public int MoveToFindLoop()
        {
            StringBuilder stringBuilder = new StringBuilder(_laboMap[_guardCoord.X]);
            char currentChar = _laboMap[_guardCoord.X][_guardCoord.Y];
            if (
                (currentChar == '|' && _currentDir == Directions.NORTH && _visited[_guardCoord.X][_guardCoord.Y][Directions.NORTH]) ||
                (currentChar == '|' && _currentDir == Directions.SOUTH && _visited[_guardCoord.X][_guardCoord.Y][Directions.SOUTH]) ||
                (currentChar == '-' && _currentDir == Directions.EAST && _visited[_guardCoord.X][_guardCoord.Y][Directions.EAST]) ||
                (currentChar == '-' && _currentDir == Directions.WEST && _visited[_guardCoord.X][_guardCoord.Y][Directions.WEST]))
                return -1;
            if ((currentChar == '-' && (_currentDir == Directions.NORTH || _currentDir == Directions.SOUTH))
                || (currentChar == '|' && (_currentDir == Directions.EAST || _currentDir == Directions.WEST)))
                stringBuilder[_guardCoord.Y] = '+';
            else
            {
                stringBuilder[_guardCoord.Y] = (_currentDir == Directions.NORTH || _currentDir == Directions.SOUTH)?'|':'-';
            }

            _visited[_guardCoord.X][_guardCoord.Y][_currentDir] = true;
            _laboMap[_guardCoord.X] = stringBuilder.ToString();

            (int, int) modifiers = GetModifier(_currentDir);
            _guardCoord.X += modifiers.Item1;
            _guardCoord.Y += modifiers.Item2;
            if (IsOutside())
                return 0;
            if (IsObstacle())
                TurnRight();
            return 1;
        }

        public int  CountX()
        {
            int sum = 0;
            for (int i = 0; i < _laboMap.Count; i++)
            {
                for (int j = 0; j < _laboMap[i].Length; j++)
                {
                    if (_laboMap[i][j] == '+' || _laboMap[i][j] == '|' ||_laboMap[i][j] == '-')
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }
/// <summary>
/// 
/// </summary>
/// <returns>0 leave 1 normal - 1 loop</returns>
        private int Ray()
        {
            while (true)
            {
                if (_visited[_guardCoord.X][_guardCoord.Y][_currentDir] == true)
                    return -1;
                switch (_currentDir)
                {
                    case Directions.EAST:
                        do
                        {
                            _guardCoord.Y++;
                            if (_guardCoord.Y >= _laboMap[_guardCoord.X].Length)
                                return 0;
                            if (_visited[_guardCoord.X][_guardCoord.Y][Directions.EAST] == true)
                                return -1;
                            if (_laboMap[_guardCoord.X][_guardCoord.Y] == '#')
                            {
                                _guardCoord.Y--;
                                break;
                            }
                            _visited[_guardCoord.X][_guardCoord.Y][Directions.EAST] = true;
                        } while (_laboMap[_guardCoord.X][_guardCoord.Y] == '.');

                        _currentDir = Directions.SOUTH;
                        break;
                    case Directions.WEST:
                        do
                        {
                            _guardCoord.Y--;
                            if (_guardCoord.Y < 0)
                                return 0;
                            if (_visited[_guardCoord.X][_guardCoord.Y][Directions.WEST] == true)
                                return -1;
                            if (_laboMap[_guardCoord.X][_guardCoord.Y] == '#')
                            {
                                _guardCoord.Y++;
                                break;
                            }
                            _visited[_guardCoord.X][_guardCoord.Y][Directions.WEST] = true;
                        } while (_laboMap[_guardCoord.X][_guardCoord.Y] == '.');
                        _currentDir = Directions.NORTH;
                        break;
                    case Directions.NORTH:
                        do
                        {
                            _guardCoord.X--;
                            if (_guardCoord.X < 0)
                                return 0;
                            if (_visited[_guardCoord.X][_guardCoord.Y][Directions.NORTH] == true)
                                return -1;
                            if (_laboMap[_guardCoord.X][_guardCoord.Y] == '#')
                            {
                                _guardCoord.X++;
                                break;
                            }
                            _visited[_guardCoord.X][_guardCoord.Y][Directions.NORTH] = true;
                        } while (_laboMap[_guardCoord.X][_guardCoord.Y] == '.');
                        _currentDir = Directions.EAST;
                        break;
                    default:
                        do
                        {
                            _guardCoord.X++;
                            if (_guardCoord.X >= _laboMap.Count)
                                return 0;
                            if (_visited[_guardCoord.X][_guardCoord.Y][Directions.SOUTH] == true)
                                return -1;
                            if (_laboMap[_guardCoord.X][_guardCoord.Y] == '#')
                            {
                                _guardCoord.X--;
                                break;
                            }
                            _visited[_guardCoord.X][_guardCoord.Y][Directions.SOUTH] = true;
                        } while (_laboMap[_guardCoord.X][_guardCoord.Y] == '.');
                        _currentDir = Directions.WEST;
                        break;
                }
            }
        }
        public bool FindLoop()
        {
            return Ray() == -1;
        }
    }
}