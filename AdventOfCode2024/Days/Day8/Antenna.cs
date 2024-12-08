using Toolbox.Datas;

namespace AdventOfCode2024.Days.Day8
{
    public class Antenna
    {
        private Coord2D<long> _coordinate;
        private char _frequency;

        public Coord2D<long> Coord => _coordinate;
        public Antenna(Coord2D<long> coord, char freq)
        {
            _coordinate = new Coord2D<long>(coord.X, coord.Y);
            _frequency = freq;
        }

        public override string ToString()
        {
            return $"#{_frequency}-{_coordinate}";
        }
    }
}