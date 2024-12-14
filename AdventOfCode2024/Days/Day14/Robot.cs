using Toolbox.Datas;

namespace AdventOfCode2024.Days.Day14
{
    public class Robot
    {
        public Coord2D<long> Vel { get; private set; }

        public Coord2D<long> Pos { get; private set; }

        public Robot(Coord2D<long> pos, Coord2D<long> vel)
        {
            Vel = vel;
            Pos = pos;
        }

        public Robot(long posx, long posy, long velx, long vely)
        {
            Pos = new Coord2D<long>(posx, posy);
            Vel = new Coord2D<long>(velx, vely);
        }

        public void UpdatePosition(Coord2D<long> newPosition)
        {
            Pos = newPosition;
        }
    }
    
}