using TShockTutorials.Commands;
using TShockTutorials.Models;

namespace TShockTutorials
{
    public class CommandManager
    {
        #region Commands
        public static TutorialCommand TutorialCommand = new();
        public static ConfigExemplarCommand ConfigExemplarCommand = new();
        #endregion

        public static List<Command> Commands = new()
        {
            TutorialCommand,
            ConfigExemplarCommand
        };

        public static void RegisterAll()
        {
            foreach (Command cmd in Commands)
            {
                TShockAPI.Commands.ChatCommands.Add(cmd);
            }
        }
    }
}
