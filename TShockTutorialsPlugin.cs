using Terraria.ID;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;
using Terraria.Localization;

namespace TShockTutorials
{
    [ApiVersion(2,1)]
    public class TShockTutorialsPlugin : TerrariaPlugin
    {
        public override string Author => "Average";
        public override string Name => "TShock Tutorial Plugin";
        public override string Description => "A sample plugin for educating aspiring TShock developers.";
        public override Version Version => new(1,0);

        public TShockTutorialsPlugin(Main game) : base(game)
        {

        }

        public override void Initialize()
        {
            // new Command("permission.nodes", "add.as.many", "as.you.like", ourCommandMethod, "these", "are", "aliases");
            Commands.ChatCommands.Add(new Command("tutorial.command", TutorialCommand, "tutorial", "tcmd"));
        }

        private void TutorialCommand(CommandArgs args)
        {
            // retrieve our player from the CommandArgs object
            var player = args.Player;
            
            // if the player doesn't exist, return (this is done to prevent errors)
            // guards like this should be implemented frequently to avoid exceptions
            if (player == null) return;

            // make sure the player is active and not being run by the server
            if (player.Active && player.RealPlayer) return;

            // create a new Random class, for random number generation
            Random rand = new Random();

            // create a new empty item object
            Item randItem = new Item();

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