using System;
using System.Linq;
using AdventOfCode2024.Days.Day9;
using AdventOfCode2024.Utilities;
using static System.Convert;

namespace AdventOfCode2024.Days
{
    public class SolverDay9 : Solver
    {
        private DiskUsage _disk;
        public SolverDay9(bool verbose = false)
        {
            _verbose = verbose;
            _disk = new DiskUsage();
            ReadInputFile();
            
        }

        public sealed override void ReadInputFile()
        {
            bool isFileBlock = true;
            int fileId = 0;
            FileReader fileReader = new FileReader("day9.txt");
            while (!fileReader.EndOfStream)
            {
                char c = fileReader.ReadChar();
               
               int n = (int)char.GetNumericValue(c);
               int size = 0;
                for (int i = 0; i < n; i++)
                {
                    size++;
                    if (isFileBlock)
                        _disk.AddBlock(fileId);
                    else
                        _disk.AddFreeSpace();
                }

                _disk.AddBlock(fileId, size, !isFileBlock);
                size = 0;
                if (isFileBlock)
                {
                    fileId++;
                }
                isFileBlock = !isFileBlock;
            }

           // Console.WriteLine(_disk.ToString());
            fileReader.Close();
            
        }

        #region Star1

        public override long GetSolution1Star()
        {
            while (_disk.FirstFreeSpace < _disk.LastFileBlock)
            {
                _disk.Invert();
            }

            Console.WriteLine(_disk.ToString1Bis());
            return _disk.CheckSum();
        }

        #endregion

        #region Star2

        public override long GetSolution2Star()
        {
            return _disk.Compress();
        }

        #endregion
    }
}