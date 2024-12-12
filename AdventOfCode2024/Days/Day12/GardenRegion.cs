using System.Collections.Generic;

namespace AdventOfCode2024.Days.Day12
{
    public class GardenRegion
    {
        public char Seed;
        public long Perimeter;
        public long Area;
        public List<Barrier> NorthBarrier = new List<Barrier>();
        public List<Barrier> SouthBarrier = new List<Barrier>();
        public List<Barrier> EastBarrier = new List<Barrier>();
        public List<Barrier> WestBarrier = new List<Barrier>();
    }
}