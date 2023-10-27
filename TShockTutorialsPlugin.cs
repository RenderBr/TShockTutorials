using OTAPI;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;

namespace TShockTutorials
{
    [ApiVersion(2, 1)]
    public class TShockTutorialsPlugin : TerrariaPlugin
    {
        public static PluginSettings Config => PluginSettings.Config;
        public override string Author => "Average";
        public override string Name => "TShock Tutorial Plugin";
        public override string Description => "A sample plugin for educating aspiring TShock developers.";
        public override Version Version => new(1, 0);

        public TShockTutorialsPlugin(Main game) : base(game)
        {

        }
        public override void Initialize()
        {
            // register our config
            PluginSettings.Load();

            // new Command("permission.nodes", "add.as.many", "as.you.like", ourCommandMethod, "these", "are", "aliases");
            Commands.ChatCommands.Add(new Command("tutorial.command", TutorialCommand, "tutorial", "tcmd"));
            Commands.ChatCommands.Add(new Command("tutorial.configex", ConfigExemplarCommand, "confex"));

            // register our hooks
            EventManager.RegisterAll(this);

        }

        private void ConfigExemplarCommand(CommandArgs args)
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