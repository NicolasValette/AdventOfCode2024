using System.Threading;
using Toolbox.Datas;

namespace AdventOfCode2024.Days.Day12
{
    public class Barrier
    {
        public Coord2D<int> Start;
        public Coord2D<int> End;

        public Barrier(Coord2D<int> start, Coord2D<int> end)
        {
            Start = start;
            End = end;
        }

        public void FusionBarrier(Barrier barrier)
        {
            if (Start.X - 1 == barrier.End.X)
            {
                Start = barrier.Start;
            }
            else if (End.X + 1 == barrier.Start.X)
            {
                End = barrier.End;
            }
            else if (Start.Y - 1 == barrier.End.Y)
            {
                Start = barrier.Start;
            }
            else if (End.Y + 1 == barrier.Start.Y)
            {
                End = barrier.End;
            }
        }
    }
}