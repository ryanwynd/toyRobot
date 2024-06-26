namespace RobotApp.Shared
{
    static class Command
    {
        public static bool ValidateCommand(string command)
        {
            if (!string.IsNullOrWhiteSpace(command))
            {
                return true;
            }
            else return false;
        }
    }
}
