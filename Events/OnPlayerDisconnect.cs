using TerrariaApi.Server;
using TShockAPI;
using TShockTutorials.Models;
using Terraria;
using Microsoft.Xna.Framework;

namespace TShockTutorials.Events
{
    public class OnPlayerDisconnect : Event
    {
        public override void Disable(TerrariaPlugin plugin)
        {
            ServerApi.Hooks.ServerLeave.Deregister(plugin, ExecuteMethod);
        }

        public override void Enable(TerrariaPlugin plugin)
        {
            ServerApi.Hooks.ServerLeave.Register(plugin, ExecuteMethod);
        }

        private void ExecuteMethod(LeaveEventArgs args)
        {
            // retrieve our player
            var player = Main.player[args.Who];

            // let's see if we can access the player
            if (player == null) return;

            // let's announce to everyone they are about to die
            TSPlayer.All.SendMessage($"{player.name} has left, so you will all die! For some reason...", Color.Tomato);

            // kill every player
            foreach (TSPlayer plr in TShock.Players)
            {
                // skip player if cant be accessed
                if (plr == null) continue;

                // kill valid players muahahaah
                plr.KillPlayer();
            }
        }
    }
}
