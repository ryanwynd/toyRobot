using AngleSharp.Dom;
using Bunit;
using RobotApp.Pages;
using RobotApp.Shared;

namespace RobotTesting
{
    public class UnitTest1 : TestContext
    {
        /* Couldnt figure out text input, may come back to
        [Fact]
        public void InputShouldntAcceptFirstEmptyInput()
        {
            var cut = RenderComponent<Home>();
            var button = cut.Find("button");

            button.Click();

            cut.Find("p").MarkupMatches("<p>Welcome to the Toy Robot App</p>");
        }
        */

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
        public void CommandValidatorIncorrectPlaceReturnFalse()
        {
            bool result = Command.ValidateCommand("PLACE1,1North");

            Assert.False(result, "Incorrectly formatted place");
        }
    }
}