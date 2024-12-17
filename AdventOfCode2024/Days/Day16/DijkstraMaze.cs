using System;
using System.Collections.Generic;
using Toolbox.Datas;

namespace AdventOfCode2024.Days.Day16
{
    public class DijkstraMaze
    {
        public enum Dir
        {
            Forward,
            Left,
            Right
        }
        
        private readonly char[][] _map;
        private readonly char _wallSeparator;
        private readonly char _freeSpace;
        private readonly Coord2D<int> _startingPoint;
        private readonly Coord2D<int> _endingPoint;
        
        private Dictionary<Coord2D<int>, int> _dist = new Dictionary<Coord2D<int>, int>();

        private Dictionary<Coord2D<int>, List<Coord2D<int>>> _previous = new Dictionary<Coord2D<int>, List<Coord2D<int>>>();
        
        
        public DijkstraMaze(char[][] map, char wallSeparator, char freeSpace, Coord2D<int> start, Coord2D<int> end)
        {
            _map = map;
            _wallSeparator = wallSeparator;
            _freeSpace = freeSpace;
            _startingPoint = start;
            _endingPoint = end;
        }

        private void Init()
        {
            for (int i = 0; i < _map.Length; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    if (_map[i][j] == '.')
                    {
                        _dist.Add(new Coord2D<int>(i, j), int.MaxValue);
                    }
                }
            }

            _dist[_startingPoint] = 0;
        }

        private Coord2D<int> FindMinimalDistNode(List<Coord2D<int>> listNodes)
        {
            int minimum = int.MaxValue;
            Coord2D<int> currentNode = null;
            foreach (var node in listNodes)
            {
                if (_dist[node] < minimum)
                {
                    minimum = _dist[node];
                    currentNode = node;
                }
            }
            return currentNode;
        }


        private void UpdateDist(Reindeer deer, Coord2D<int> node1, Coord2D<int> node2)
        {
            int weight = Int32.MaxValue;
            if (node1.X == node2.X + 1 && node1.Y == node2.Y)
            {
                if (deer.Facing == Directions.SOUTH)
                    weight = 1;
                if (deer.Facing == Directions.EAST || deer.Facing == Directions.WEST)
                    weight = 1000;
                else
                    weight = 2000;
            }

            if (node1.X == node2.X - 1 && node1.Y == node2.Y)
            {
                if (deer.Facing == Directions.NORTH)
                    weight = 1;
                if (deer.Facing == Directions.EAST || deer.Facing == Directions.WEST)
                    weight = 1000;
                else
                    weight = 2000;
            }

            if (node1.X == node2.X && node1.Y == node2.Y + 1)
            {
                if (deer.Facing == Directions.EAST)
                    weight = 1;
                if (deer.Facing == Directions.NORTH || deer.Facing == Directions.SOUTH)
                    weight = 1000;
                else
                    weight = 2000;
            }

            if (node1.X == node2.X && node1.Y == node2.Y - 1)
            {
                if (deer.Facing == Directions.WEST)
                    weight = 1;
                if (deer.Facing == Directions.NORTH || deer.Facing == Directions.SOUTH)
                    weight = 1000;
                else
                    weight = 2000;
            }

            if (_dist[node2] > _dist[node1] + weight)
            {
                _dist[node2] = _dist[node1] + weight;
                if (!_previous.ContainsKey(node2))
                    _previous.Add(node2, new List<Coord2D<int>>());
                _previous[node2].Add(node1);
                   
            }
        }

        private List<Move> NextMoves(Move lastMove)
        {
            List<Move> moves = new List<Move>();
            //
            // if (_map[lastMove.Position.X-1][lastMove.Position.Y] == _freeSpace)
            //     nodes.Add(new Coord2D<int>(lastMove.Position.X, lastMove.Position.Y));
            //
            //
            //
            //
            //
            // if (_map[node.X][node.Y+1] == _freeSpace)
            //     nodes.Add(new Coord2D<int>(node.X, node.Y+1));
            // if (_map[node.X-1][node.Y] == _freeSpace)
            //     nodes.Add(new Coord2D<int>(node.X-1, node.Y));
            // if (_map[node.X][node.Y-1] == _freeSpace)
            //     nodes.Add(new Coord2D<int>(node.X, node.Y-1));
            return moves;
        }

        private void Dijkstra()
        {
            // Init();
            // var Q = new List<Coord2D<int>>();
            // for (int i = 0; i < _map.Length; i++)
            // {
            //     for (int j = 0; j < _map[i].Length; j++)
            //     {
            //         if (_map[i][j] == _freeSpace)
            //             Q.Add(new Coord2D<int>(i,j));
            //     }
            // }
            //
            // while (Q.Count > 0)
            // {
            //     var node = FindMinimalDistNode(Q);
            //     Q.Remove(node);
            //     foreach (var element in Neighbours(node))
            //     {
            //         UpdateDist(node, element);
            //     }
            // }

        }







    }
}