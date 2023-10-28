using Terraria;
using Terraria.ID;
using TShockAPI;

namespace TShockTutorials.Commands
{
    public class TutorialCommand : Models.Command
    {
        public override string[] Aliases { get; set; } = { "tutorial", "tcmd" };
        public override string PermissionNode { get; set; } = Permissions.TutorialCommand;

        public override void Execute(CommandArgs args)
        {
            // retrieve our player from the CommandArgs object
            var player = args.Player;

            // if the player doesn't exist, return (this is done to prevent errors)
            // guards like this should be implemented frequently to avoid exceptions
            if (player == null) return;

            // make sure the player is active and not being run by the server
            if (player.Active && player.RealPlayer) return;

            // create a new Random class, for random number generation
            Random rand = new();

            // create a new empty item object
            Item randItem = new();

            // get a random item id
            // you will need to add 'using Terraria.ID' to use ItemID
            int randItemID = rand.Next(0, ItemID.Count);

            // turn our newly created item class into our desired item
            randItem.SetDefaults(randItemID);

            // we can also specify a prefix and stack size if we so desire:
            randItem.stack = 128;
            randItem.prefix = PrefixID.Legendary;

            // give our player the item
            player.GiveItem(randItem.type, randItem.stack, randItem.prefix);

            // tell our player
            player.SendSuccessMessage($"You have been given {randItem.stack}x {randItem.Name} ({Lang.prefix[randItem.prefix].Value}!");
        }
    }
}
