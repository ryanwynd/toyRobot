using System.Text.RegularExpressions;

namespace RobotApp.Shared
{
    public static class Command
    {
        public static bool ValidateCommand(string command)
        {
            if (!string.IsNullOrWhiteSpace(command))
            {
                //Thanks for the regex chatGPT :)
                var regex = "^(?:(?<command>PLACE)\\s(?<xPos>\\d),(?<yPos>\\d)\\s(?<direction>North|South|East|West)|(?<command>MOVE|LEFT|RIGHT|REPORT))$";
                var match = Regex.Match(command,regex, RegexOptions.IgnoreCase);
                if(match.Success)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
    }
}
