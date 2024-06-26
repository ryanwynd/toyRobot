using AngleSharp.Dom;
using Bunit;
using RobotApp.Pages;

namespace RobotTesting
{
    public class UnitTest1 : TestContext
    {
        [Fact]
        public void InputShouldntAcceptFirstEmptyInput()
        {
            var cut = RenderComponent<Home>();
            var button = cut.Find("button");

            button.Click();

            cut.Find("p").MarkupMatches("<p>Welcome to the Toy Robot App</p>");
        }
    }
}