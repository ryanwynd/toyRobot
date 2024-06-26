using System.Collections.Generic;

namespace RobotApp.Shared
{
    public class Robot
    {
        private int xPos { get; set; }
        private int yPos { get; set; }
        private Direction facing { get; set; }
        private Table table { get; }

        public Robot(int x, int y, Direction facingDirection, Table tableOn)
        {
            xPos = x;
            yPos = y;
            facing = facingDirection;
            table = tableOn;
        }


        public void move()
        {
            int[] destinationPosition = [xPos, yPos];
            switch (facing)
            {
                case Direction.North:
                    destinationPosition[1] = yPos + 1;
                    break;
                case Direction.South:
                    destinationPosition[1] = yPos - 1;
                    break;
                case Direction.West:
                    destinationPosition[0] = xPos - 1;
                    break;
                case Direction.East:
                    destinationPosition[0] = xPos + 1;
                    break;
            }
            if (!(destinationPosition[0] < 0 || destinationPosition[0] > table.width || destinationPosition[1] < 0 || destinationPosition[0] > table.length))
            {
                xPos = destinationPosition[0];
                yPos = destinationPosition[1];
            }
        }

        public void turnLeft()
        {
            switch (facing)
            {
                case Direction.North:
                    facing = Direction.West;
                    break;
                case Direction.South:
                    facing = Direction.East;
                    break;
                case Direction.West:
                    facing = Direction.South;
                    break;
                case Direction.East:
                    facing = Direction.North;
                    break;
            }
        }
        public void turnRight()
        {
            switch (facing)
            {
                case Direction.North:
                    facing = Direction.East;
                    break;
                case Direction.South:
                    facing = Direction.West;
                    break;
                case Direction.West:
                    facing = Direction.North;
                    break;
                case Direction.East:
                    facing = Direction.South;
                    break;
            }
        }
    }

    public enum Direction
    {
        North,
        South,
        East,
        West
    }
}
