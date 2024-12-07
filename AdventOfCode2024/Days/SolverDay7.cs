using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Days
{
    public class SolverDay7 : Solver
    {
        private List<(long, List<long>)> _tests= new List<(long, List<long>)>();
        public SolverDay7(bool verbose)
        {
            _verbose = verbose;
            ReadInputFile();
        }
        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day7.txt");
            while (!fileReader.EndOfStream)
            {
                string line = fileReader.Read();
                string[] resultLine = line.Split(':');
                long res = long.Parse(resultLine[0]);
                var list = resultLine[1].Trim().Split(' ').Select(x => long.Parse(x.Trim())).ToList();
                _tests.Add((res, list));
            }
            fileReader.Close();
        }

        private long AddOperator(long result, List<long> numbers, long tempResult = -1, bool useContat = false)
        {
            if (result == tempResult && numbers.Count == 0)
                return result;
            if (numbers.Count == 0)
                return 0;
            long n = numbers.First();
            List<long> listP = new List<long>(numbers);
            listP.RemoveAt(0);
            List<long> listM = new List<long>(numbers);
            listM.RemoveAt(0);
            List<long> listC = new List<long>(numbers);
            listC.RemoveAt(0);
           
            long resPlus = AddOperator(result, listP, tempResult==-1?n:tempResult + n, useContat);
            long resMult = AddOperator(result, listM, tempResult==-1?n:tempResult * n, useContat);
            if (useContat)
            {
                long resConcat = AddOperator(result, listM, tempResult == -1 ? n : long.Parse(string.Concat(tempResult.ToString(), n.ToString())), useContat);
               
                if (resConcat == result)
                    return resConcat;
            }
            if (resPlus == result)
                return resPlus;
            if (resMult == result)
                return resMult;
            return 0;

        }
        public override long GetSolution1Star()
        {
            long solution = 0;
            foreach (var element in _tests)
            {
                long resultat = AddOperator(element.Item1, element.Item2);
                if (resultat != 0)
                {
                    solution += resultat;
                    Console.WriteLine("Find res with : " + element.Item1);
                }
            }
           
            return solution;
        }
        public override long GetSolution2Star()
        {
            long solution = 0;
            foreach (var element in _tests)
            {
                long resultat = AddOperator(element.Item1, element.Item2, useContat:true);
                if (resultat != 0)
                {
                    solution += resultat;
                    //Console.WriteLine("Find res with : " + element.Item1);
                }
            }
           
            return solution;
        }
    }
}