using TShockAPI;

namespace TShockTutorials.Models
{
    public abstract class Command
    {
        public abstract string[] Aliases { get; set; }
        public abstract string PermissionNode { get; set; }
        public abstract void Execute(CommandArgs args);
        public static implicit operator TShockAPI.Command(Command cmd) => new(cmd.PermissionNode,
            cmd.Execute, cmd.Aliases);
    }
}
