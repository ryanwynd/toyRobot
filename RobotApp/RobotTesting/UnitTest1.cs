using AngleSharp.Dom;
using Bunit;
using RobotApp.Pages;
using RobotApp.Shared;

namespace RobotTesting
{
    public class CommandValidatorTests : TestContext
    {
        [Fact]
        public void CommandValidatorEmptyStringReturnFalse()
        {
            bool result = Command.ValidateCommand("");

            Assert.False(result, "Empty commands are not valid");
        }

        [Fact]
        public void CommandValidatorCorrectPlaceReturnTrue()
        {
            bool result = Command.ValidateCommand("PLACE 1,1 North");

            Assert.True(result, "Command is correct");
        }

        [Fact]
        public void CommandValidatorCorrectMoveReturnTrue()
        {
            bool result = Command.ValidateCommand("Move");

            Assert.True(result, "Command is correct");
        }

        [Fact]
        public void CommandValidatorCorrectRightReturnTrue()
        {
            bool result = Command.ValidateCommand("Right");

            Assert.True(result, "Command is correct");
        }

        [Fact]
        public void CommandValidatorCorrectLeftReturnTrue()
        {
            bool result = Command.ValidateCommand("Left");

            Assert.True(result, "Command is correct");
        }

        [Fact]
        public void CommandValidatorCorrectReportReturnTrue()
        {
            bool result = Command.ValidateCommand("Report");

            Assert.True(result, "Command is correct");
        }

        [Fact]
        public void CommandValidatorNonCommandReturnFalse()
        {
            bool result = Command.ValidateCommand("JUMP");

            Assert.False(result, "Jump is a not valid command");
        }

        [Fact]
        public void CommandValidatorDoubleCommandReturnFalse()
        {
            bool result = Command.ValidateCommand("MOVE LEFT");

            Assert.False(result, "Only one command at a time");
        }
        [Fact]
        public void CommandValidatorDoublePlaceCommandReturnFalse()
        {
            bool result = Command.ValidateCommand("PLACE 1,2 South PLACE 1,2 South");

            Assert.False(result, "Only one command at a time");
        }
        [Fact]
        public void CommandValidatorLongPlaceCommandReturnFalse()
        {
            bool result = Command.ValidateCommand("PLACE 11,23456789123456789 NORTH");

            Assert.False(result, "Place command coordinates are too long");
        }
        [Fact]
        public void CommandValidatorIncorrectPlaceReturnFalse()
        {
            bool result = Command.ValidateCommand("PLACE1,1North");

            Assert.False(result, "Incorrectly formatted place");
        }
    }

    public class ComandResolverTests : TestContext
    {
        [Fact]
        public void CommandResolverLeftReturnLeft()
        {
            string result = Command.ResolveCommand("Left");

            Assert.Equal("LEFT", result);
        }
        [Fact]
        public void CommandResolverRightReturnRight()
        {
            string result = Command.ResolveCommand("Right");

            Assert.Equal("RIGHT", result);
        }
        [Fact]
        public void CommandResolverMoveReturnMove()
        {
            string result = Command.ResolveCommand("Move");

            Assert.Equal("MOVE", result);
        }
        [Fact]
        public void CommandResolverReportReturnReport()
        {
            string result = Command.ResolveCommand("Report");

            Assert.Equal("REPORT", result);
        }
        [Fact]
        public void CommandResolverPlaceReturnPlace()
        {
            string result = Command.ResolveCommand("Place 0,0 North");

            Assert.Equal("PLACE", result);
        }
        [Fact]
        public void CommandResolverInvalidReturnInvalid()
        {

            Assert.Throws<ArgumentException>(() => Command.ResolveCommand("Jump"));
        }
        [Fact]
        public void PlaceResolverReturnsCorrectPlaceComandRecord()
        {
            PlaceCommand result = Command.ResolvePlace("Place 3,2 West");

            Assert.Equal(3, result.xPos);
            Assert.Equal(2, result.yPos);
            Assert.Equal(Direction.WEST, result.facing);
        }
    }

    public class RobotPlaceTests : TestContext
    {
        [Fact]
        public void RobotPlaceExecutesFirstTime()
        {
            Robot robot = new Robot();
            robot.Place(3, 2, Direction.WEST);

            Assert.Equal("3,2,WEST", robot.Report());
        }
        [Fact]
        public void RobotPlaceDoesntExecuteAfterFirstTime()
        {
            Robot robot = new Robot();
            robot.Place(3, 2, Direction.WEST);
            robot.Place(4, 1, Direction.SOUTH);

            Assert.Equal("3,2,WEST", robot.Report());
        }
        [Fact]
        public void RobotNotPlacedOutOfBounds()
        {
            Robot robot = new Robot();
            robot.Place(3, 6, Direction.WEST);

            Assert.Equal("Robot has not yet been placed", robot.Report());
        }
        [Fact]
        public void RobotIsPlacedOnValidSecondTry()
        {
            Robot robot = new Robot();
            robot.Place(3, 6, Direction.WEST);
            robot.Place(1, 2, Direction.NORTH);

            Assert.Equal("1,2,NORTH", robot.Report());
        }
    }

    public class RobotMoveTests : TestContext
    {
        [Fact]
        public void RobotMovesNorth()
        {
            Robot robot = new Robot();
            robot.Place(1, 1, Direction.NORTH);
            robot.Move();

            Assert.Equal("1,2,NORTH", robot.Report());
        }
        [Fact]
        public void RobotMovesEast()
        {
            Robot robot = new Robot();
            robot.Place(1, 1, Direction.EAST);
            robot.Move();

            Assert.Equal("2,1,EAST", robot.Report());
        }
        [Fact]
        public void RobotMovesSouth()
        {
            Robot robot = new Robot();
            robot.Place(1, 1, Direction.SOUTH);
            robot.Move();

            Assert.Equal("1,0,SOUTH", robot.Report());
        }
        [Fact]
        public void RobotMovesWest()
        {
            Robot robot = new Robot();
            robot.Place(1, 1, Direction.WEST);
            robot.Move();

            Assert.Equal("0,1,WEST", robot.Report());
        }
        [Fact]
        public void RobotMovesTwice()
        {
            Robot robot = new Robot();
            robot.Place(1, 1, Direction.EAST);
            robot.Move();
            robot.Move();

            Assert.Equal("3,1,EAST", robot.Report());
        }
        [Fact]
        public void RobotStopsAtNorthEdge()
        {
            Robot robot = new Robot();
            robot.Place(0, 3, Direction.NORTH);
            robot.Move();
            robot.Move();

            Assert.Equal("0,4,NORTH", robot.Report());
        }
        [Fact]
        public void RobotStopsAtEastEdge()
        {
            Robot robot = new Robot();
            robot.Place(3, 0, Direction.EAST);
            robot.Move();
            robot.Move();

            Assert.Equal("4,0,EAST", robot.Report());
        }
        [Fact]
        public void RobotStopsAtSouthEdge()
        {
            Robot robot = new Robot();
            robot.Place(0, 3, Direction.SOUTH);
            robot.Move();
            robot.Move();

            Assert.Equal("0,1,SOUTH", robot.Report());
        }
        [Fact]
        public void RobotStopsAtWestEdge()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.WEST);
            robot.Move();
            robot.Move();

            Assert.Equal("0,0,WEST", robot.Report());
        }
    }
    public class RobotTurnTests : TestContext
    {
        [Fact]
        public void RobotTurnsLeftWest()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.WEST);
            robot.TurnLeft();

            Assert.Equal("1,0,SOUTH", robot.Report());
        }
        [Fact]
        public void RobotTurnsLeftSouth()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.SOUTH);
            robot.TurnLeft();

            Assert.Equal("1,0,EAST", robot.Report());
        }
        [Fact]
        public void RobotTurnsLeftEast()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.EAST);
            robot.TurnLeft();

            Assert.Equal("1,0,NORTH", robot.Report());
        }
        [Fact]
        public void RobotTurnsLeftNorth()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.NORTH);
            robot.TurnLeft();

            Assert.Equal("1,0,WEST", robot.Report());
        }
        [Fact]
        public void RobotTurnsRightWest()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.WEST);
            robot.TurnRight();

            Assert.Equal("1,0,NORTH", robot.Report());
        }
        [Fact]
        public void RobotTurnsRightNorth()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.NORTH);
            robot.TurnRight();

            Assert.Equal("1,0,EAST", robot.Report());
        }
        [Fact]
        public void RobotTurnsRightEast()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.EAST);
            robot.TurnRight();

            Assert.Equal("1,0,SOUTH", robot.Report());
        }
        [Fact]
        public void RobotTurnsRightSouth()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.SOUTH);
            robot.TurnRight();

            Assert.Equal("1,0,WEST", robot.Report());
        }
    }
    public class RobotReportTests : TestContext
    {
        [Fact]
        public void RobotReportShowsCurrentState()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.WEST);

            Assert.Equal("1,0,WEST", robot.Report());
        }
        [Fact]
        public void RobotCanReportMultipleTimes()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.WEST);
            string firstReport = robot.Report();
            string secondReport = robot.Report();

            Assert.Equal(firstReport, secondReport);
        }
        [Fact]
        public void RobotCanReportMultipleTimesAfterMoving()
        {
            Robot robot = new Robot();
            robot.Place(1, 0, Direction.WEST);
            string firstReport = robot.Report();
            robot.Move();
            string secondReport = robot.Report();

            Assert.Equal("1,0,WEST", firstReport);
            Assert.Equal("0,0,WEST", secondReport);
        }
    }
}