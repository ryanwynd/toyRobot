using System.Text.RegularExpressions;

namespace RobotApp.Shared
{
    public static class Command
    {
        private static readonly string regex = "^(?:(?<command>PLACE)\\s(?<xPos>\\d),(?<yPos>\\d)\\s(?<direction>North|South|East|West)|(?<command>MOVE|LEFT|RIGHT|REPORT))$";

        public static bool ValidateCommand(string command)
        {
            if (!string.IsNullOrWhiteSpace(command))
            {
                var match = Regex.Match(command, regex, RegexOptions.IgnoreCase);
                return match.Success;
            }
            else return false;
        }

        public static string ResolveCommand(string fullCommand)
        {
            var match = Regex.Match(fullCommand, regex, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string command = match.Groups["command"].Value.ToUpper();
                return command;
            }
            else throw new ArgumentException("Invalid argument passed to ResolveCommand");
        }

        public static PlaceCommand ResolvePlace(string fullCommand)
        {
            var match = Regex.Match(fullCommand, regex, RegexOptions.IgnoreCase);

            int xPos;
            Int32.TryParse(match.Groups["xPos"].Value, out xPos);
            int yPos;
            Int32.TryParse(match.Groups["yPos"].Value, out yPos);
            Direction facing;
            Enum.TryParse(match.Groups["direction"].Value.ToUpper(), out facing);

            return new PlaceCommand(xPos, yPos, facing);
        }
    }

    public record PlaceCommand (int xPos, int yPos, Direction facing);
}
