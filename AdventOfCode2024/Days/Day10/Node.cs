using Toolbox.Datas;

namespace AdventOfCode2024.Days.Day10
{
    public class Node
    {
        public int Value { get; private set; }

        public Coord2D<int> Coord { get; private set; }
        
        public Node(int value, Coord2D<int> coord2D)
        {
            Value = value;
            Coord = coord2D;
        }

        public Node(int value, int i, int j)
        {
            Value = value;
            Coord = new Coord2D<int>(i, j);
        }
    }
}