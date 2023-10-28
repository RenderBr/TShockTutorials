using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TShockAPI;
using static TShockTutorials.TShockTutorialsPlugin;

namespace TShockTutorials.Commands
{
    public class ConfigExemplarCommand : Models.Command
    {
        public override string[] Aliases { get; set; } = { "confex" };
        public override string PermissionNode { get; set; } = Permissions.ConfigExemplarCommand;

        public override void Execute(CommandArgs args)
        {
            // retrieve our player
            var player = args.Player;

            // retrieve all of our fun facts
            var funFacts = Config.WeirdFacts;

            // retrieve one fun fact randomly
            var funFact = funFacts[Main.rand.Next(0, funFacts.Count)];

            // send the player the fact
            player.SendMessage($"{Config.GameName}", Color.LightCoral);
            player.SendMessage($"{funFact}", Color.PaleGoldenrod);

            // give the player a zenith with a quantity of our "cool" number
            player.GiveItem(ItemID.Zenith, Config.ChosenNumber, PrefixID.Annoying);
        }
    }
}
