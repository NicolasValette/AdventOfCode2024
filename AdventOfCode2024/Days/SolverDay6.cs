using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2024.Days.Day6;
using AdventOfCode2024.Utilities;
using Toolbox.Datas;

namespace AdventOfCode2024.Days
{
    public class SolverDay6 : Solver
    {
        private List<string> _map;

        public SolverDay6(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }

        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day6.txt");

            _map = new List<string>((fileReader.ReadToEndAndSplit()));
            
            fileReader.Close();
        }
        #region STAR1
        public override long GetSolution1Star()
        {
           
            //var x = _map.Where(line => line.Contains('^')).
           // var coord = _map.Where(line => line.Contains('^')).Select(line => line.IndexOf('^'));

           var t = _map.Where(line => line.Contains('^')).Select(x => new { Row = _map.IndexOf(x), Column = x.IndexOf('^') }).First();
            LaboMap labo = new LaboMap(new List<string>(_map), new Coord2D<int>(t.Row, t.Column));

            while (labo.MoveToFindLoop() == 1)
            {
            }

            return labo.CountX();
        }
        #endregion

        #region Star2

        public bool IsLoop(LaboMap lab)
        {
            int result;
            while (true)
            {
                result = lab.MoveToFindLoop();
                if (result != 1)
                    break;

            }
            return result == -1;
        }
        public override long GetSolution2Star()
        {
            int nbTry = 0;
            long solution = 0;
            var t = _map.Where(line => line.Contains('^')).Select(x => new { Row = _map.IndexOf(x), Column = x.IndexOf('^') }).First();
            LaboMap labo = new LaboMap(new List<string>(_map), new Coord2D<int>(t.Row, t.Column));
            int result;
            
            LaboMap laboZero = new LaboMap(new List<string>(_map), new Coord2D<int>(t.Row, t.Column));

            while (laboZero.MoveToFindLoop() == 1)
            {
            }

            
            for (int i = 0; i < _map.Count; i++)
            {
                for (int j = 0; j < _map[i].Length; j++)
                {
                   
                    if ((i == t.Row && j == t.Column) || (!laboZero.IsSomethingClose(i, j))) continue;
                    List<string> tempLabList = new List<string>(_map);
                    StringBuilder stringBuilder = new StringBuilder(tempLabList[i]);
                    stringBuilder[j] = '#';
                    tempLabList[i] = stringBuilder.ToString();
                    LaboMap tempLab = new LaboMap(new List<string>(tempLabList), new Coord2D<int>(t.Row, t.Column));
                    nbTry++;
                    if (tempLab.FindLoop())
                    {
                      
                        // Console.WriteLine("**************************************************");
                        // Console.ForegroundColor = ConsoleColor.Red;
                        // Console.WriteLine("**************************************************");
                        // Console.ResetColor();
                        // Console.WriteLine("**************************************************");
                        // tempLab.WriteInConsole(i, j);
                        solution++;
                    }
                }
            }
            Console.WriteLine("Nombre de try : " + nbTry);
            return solution;
        }

        #endregion
    }
}