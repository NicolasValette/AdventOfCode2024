using Toolbox.Datas;

namespace AdventOfCode2024.Days.Day8
{
    // TODO: Ajouter à la toolbox
    /// <summary>
    /// Represent a Line : y = ax + b
    /// </summary>
    public class Line
    {
        private float _a;
        private long _b;

        public Line(float a, long b)
        {
            _a = a;
            _b = b;
        }
        public Line(Coord2D<long> pointA, Coord2D<long> pointB)
        {
            _a = (pointB.Y - pointA.Y) / (float)(pointB.X - pointB.X);
        }
    }
}