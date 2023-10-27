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

        private void OnPlayerLogin(PlayerPostLoginEventArgs eventArgs)
        {
            // retrieve player from args
            var player = eventArgs.Player;

            // normally i'd recommend checking if the player is valid anytime a player is accessed
            // however, since we know the player just logged in we can assume they are real & 
            // should be logged in

            // send the player a nice little greeting
            player.SendMessage($"Welcome to our server, {player.Name}, thanks for logging in!" +
                $"Here are some nice buffs :)", Color.Aquamarine);

            // apply some buffs to the player
            player.SetBuff(BuffID.Swiftness);
            player.SetBuff(BuffID.Regeneration, 1200);
        }

        private void OnServerReload(ReloadEventArgs eventArgs)
        {
            var playerReloading = eventArgs.Player;

            try
            {
                PluginSettings.Load();
                playerReloading.SendSuccessMessage("[TutorialPlugin] Config reloaded!");
            }
            catch (Exception ex)
            {
                playerReloading.SendErrorMessage("There was an issue loading the config!");
                TShock.Log.ConsoleError(ex.ToString());
            }
        }

        public override void Initialize()
        {
            // register our config
            PluginSettings.Load();

            // new Command("permission.nodes", "add.as.many", "as.you.like", ourCommandMethod, "these", "are", "aliases");
            Commands.ChatCommands.Add(new Command("tutorial.command", TutorialCommand, "tutorial", "tcmd"));

            GeneralHooks.ReloadEvent += OnServerReload;
            PlayerHooks.PlayerPostLogin += OnPlayerLogin;

            Hooks.NPC.DropLoot += DropLoot;

            GetDataHandlers.ChestOpen += OnChestOpen;
        }

        private void OnPlayerDisconnect(LeaveEventArgs args)
        {
            // retrieve our player
            var player = Main.player[args.Who];

            // let's see if we can access the player
            if(player == null) return;

            // let's announce to everyone they are about to die
            TSPlayer.All.SendMessage($"{player.name} has left, so you will all die! For some reason...", Color.Tomato);

            // kill every player
            foreach(TSPlayer plr in TShock.Players)
            {
                // skip player if cant be accessed
                if(plr == null) continue;

                // kill valid players muahahaah
                plr.KillPlayer();
            }
        }

        private void DropLoot(object _, Hooks.NPC.DropLootEventArgs e)
        {
            TSPlayer.All.SendMessage($"Cancelled loot drop for: {e.Npc.FullName}", Color.IndianRed);
            e.Result = HookResult.Cancel;
        }

        public void OnChestOpen(object _, GetDataHandlers.ChestOpenEventArgs args)
        {
            // find the chest at the coordinates
            Chest chest = Main.chest.FirstOrDefault(x => x.x == args.X && x.y == args.Y);

            // if we cannot find the chest, return the method (stop executing any further)
            if (chest == null) return;

            // set the sixth item in the chest to an empty item
            chest.item[5] = new Item();

            // send appropriate packet to update chest item for all players
            TSPlayer.All.SendData(PacketTypes.ChestItem, "", Main.chest.ToList().IndexOf(chest), 5, 0, 0, new Item().netID);
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