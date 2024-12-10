using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AdventOfCode2024.Days.Day10;
using AdventOfCode2024.Utilities;
using Toolbox.Datas;

namespace AdventOfCode2024.Days
{
    public class SolverDay10 : Solver
    {
        private List<string> _map;
        private List<Graph<Node>> _trails = new List<Graph<Node>>();
        public SolverDay10(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }

        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day10.txt");
            _map = fileReader.ReadAndSplitInto2DList();
            fileReader.Close();
        }

        #region Star1

        private void CreateTrails(int i, int j, Node currentNode, ref Graph<Node> graph)
        {
            int value = int.Parse(_map[i][j].ToString());
            if (_map[i][j] == '0')
            {
                graph.InitStartingNode(currentNode);
            }
            //South
            if (i+1 < _map.Count)
            {
                int valueSouth = int.Parse(_map[i + 1][j].ToString());
                if (value + 1 == valueSouth)
                {
                    Node node = new Node(valueSouth, i + 1, j);
                    graph.AddEdge(currentNode, node);
                    if (valueSouth < 9)
                        CreateTrails(i+1, j, node, ref graph);
                    else
                    {
                        graph.AddFinalNode(node);
                    }
                }
            }
            //East
            if (j+1 < _map[i].Length)
            {
                int valueEast = int.Parse(_map[i][j+1].ToString());
                if (value + 1 == valueEast)
                {
                    Node node = new Node(valueEast, i, j+1);
                    graph.AddEdge(currentNode, node);
                    if (valueEast < 9)
                        CreateTrails(i, j+1, node, ref graph);
                    else
                    {
                        graph.AddFinalNode(node);
                    }
                }
            }
            //North
            if (i-1 >= 0)
            {
                int valueNorth = int.Parse(_map[i - 1][j].ToString());
                if (value + 1 == valueNorth)
                {
                    Node node = new Node(valueNorth, i - 1, j);
                    graph.AddEdge(currentNode, node);
                    if (valueNorth < 9)
                        CreateTrails(i-1, j, node, ref graph);
                    else
                    {
                        graph.AddFinalNode(node);
                    }
                }
            }
            //West
            if (j-1 >= 0)
            {
                int valueWest = int.Parse(_map[i][j-1].ToString());
                if (value + 1 == valueWest)
                {
                    Node node = new Node(valueWest, i, j-1);
                    graph.AddEdge(currentNode, node);
                    if (valueWest < 9)
                        CreateTrails(i, j-1, node, ref graph);
                    else
                    {
                        graph.AddFinalNode(node);
                    }
                }
            }
        }
        public override long GetSolution1Star()
        {
            for (int i = 0; i < _map.Count; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                    int value = int.Parse(_map[i][j].ToString());
                    if (value != 0) continue;
                    Node startingNode = new Node(value, i, j);
                    Graph<Node> newGraph = new Graph<Node>();
                    CreateTrails(i, j, startingNode, ref newGraph);
                    _trails.Add(newGraph);
                }
            }

            long sol = 0;
            for (int i = 0; i < _trails.Count; i++)
            {
                List<Coord2D<int>> solution = new List<Coord2D<int>>();
                foreach (Node n in _trails[i].Final)
                {
                    if (solution.Contains(n.Coord)) continue;
                    solution.Add(n.Coord);
                }

                sol += solution.Count;
            }

            return sol;
        }

        #endregion

        #region Star2

        public override long GetSolution2Star()
        {
            return _trails.Select(x => x.Final.Count).Sum();
        }

        #endregion
    }
}