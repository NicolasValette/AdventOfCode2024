using System;
using System.Collections.Generic;
using Toolbox.Datas;

namespace AdventOfCode2024.Days.Day16
{
    public class Reindeer
    {
        public Coord2D<int> Position { get; private set; }
        public Directions Facing { get; set; }
         
         public Reindeer(int i, int j, Directions facing)
        {
            Position = new Coord2D<int>(i, j);
            Facing = facing;
        }

        public void MoveTo(int i, int j)
        {
            Position.X = i;
            Position.Y = j;
        }

        public void Turn(bool clockwise)
        {
            Facing = Facing switch
            {
                Directions.NORTH => clockwise ? Directions.EAST : Directions.WEST,
                Directions.SOUTH => clockwise ? Directions.WEST : Directions.EAST,
                Directions.EAST => clockwise ? Directions.SOUTH : Directions.NORTH,
                Directions.WEST => clockwise ? Directions.NORTH : Directions.SOUTH,
                _ => Facing
            };
        }
    }

    public class Move
    {
        public Coord2D<int> Position;
        public Directions direction;
    }
}