using AdventOfCode2024.Utilities;

namespace AdventOfCode2024.Days
{
    public class SolverDay23 : Solver
    {
        public SolverDay23(bool verbose = false)
        {
            _verbose = verbose;
            ReadInputFile();
        }

        public sealed override void ReadInputFile()
        {
            FileReader fileReader = new FileReader("day23.txt");
            fileReader.Close();
        }

        #region Star1

        public override long GetSolution1Star()
        {
            return base.GetSolution1Star();
        }

        #endregion

        #region Star2

        public override long GetSolution2Star()
        {
            return base.GetSolution2Star();
        }

        #endregion
    }
}