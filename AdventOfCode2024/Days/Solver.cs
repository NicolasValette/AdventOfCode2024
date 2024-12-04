using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Days
{
    internal abstract class Solver : ISolver
    {
        protected string _input;
        protected bool _verbose;
        public virtual long GetSolution1Star()
        {
            return -1;
        }



        public virtual long GetSolution2Star()
        {
            return -1;
        }


        public abstract void ReadInputFile();
        
       
    }
}
