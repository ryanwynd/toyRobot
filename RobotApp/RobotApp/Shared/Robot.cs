using System.Collections.Generic;
using System.Text;

namespace RobotApp.Shared
{
    public class Robot
    {
        private int? xPos { get; set; }
        private int? yPos { get; set; }
        private Direction? facing { get; set; }
        private Table table { get; }

        public Robot()
        {
            table = new Table();
        }

        public void Place(int x, int y, Direction facingDirection)
        {
            if(!IsPlaced() && IsDestinationOnTable(x,y))
            {
                xPos = x;
                yPos = y;
                facing = facingDirection;
            }
        }

        public void Move()
        {
            if (IsPlaced())
            {
                int[] destinationPosition = [xPos.Value, yPos.Value];
                switch (facing)
                {
                    case Direction.WEST:
                        destinationPosition[0] = xPos.Value - 1;
                        break;
                    case Direction.EAST:
                        destinationPosition[0] = xPos.Value + 1;
                        break;
                    case Direction.NORTH:
                        destinationPosition[1] = yPos.Value + 1;
                        break;
                    case Direction.SOUTH:
                        destinationPosition[1] = yPos.Value - 1;
                        break;
                }
                if (IsDestinationOnTable(destinationPosition[0], destinationPosition[1]))
                {
                    xPos = destinationPosition[0];
                    yPos = destinationPosition[1];
                }
            }
        }

        public void TurnLeft()
        {
            if (IsPlaced())
            {
                switch (facing)
                {
                    case Direction.NORTH:
                        facing = Direction.WEST;
                        break;
                    case Direction.SOUTH:
                        facing = Direction.EAST;
                        break;
                    case Direction.WEST:
                        facing = Direction.SOUTH;
                        break;
                    case Direction.EAST:
                        facing = Direction.NORTH;
                        break;
                }
            }
        }
        public void TurnRight()
        {
            if (IsPlaced())
            {
                switch (facing)
                {
                    case Direction.NORTH:
                        facing = Direction.EAST;
                        break;
                    case Direction.SOUTH:
                        facing = Direction.WEST;
                        break;
                    case Direction.WEST:
                        facing = Direction.NORTH;
                        break;
                    case Direction.EAST:
                        facing = Direction.SOUTH;
                        break;
                }
            }
        }
        public string Report()
        {
            if (IsPlaced())
            {
                return xPos + "," + yPos + "," + facing;
            }
            return "Robot has not yet been placed";
        }

        private bool IsPlaced()
        {
            if (xPos.HasValue && yPos.HasValue)
            {
                return true;
            }
            else return false;
        }

        private bool IsDestinationOnTable(int x, int y)
        {
            if ((!(x < 0 || x > table.width) && !(y < 0 || y > table.length)))
            {
                return true;
            }
            else return false;
        }
    }

    public enum Direction
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }
}
