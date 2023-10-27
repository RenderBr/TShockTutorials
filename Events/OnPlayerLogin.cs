using Microsoft.Xna.Framework;
using Terraria.ID;
using TerrariaApi.Server;
using TShockAPI.Hooks;
using TShockTutorials.Models;

namespace TShockTutorials.Events
{
    public class OnPlayerLogin : Event
    {
        public override void Enable(TerrariaPlugin plugin)
        {
            PlayerHooks.PlayerPostLogin += ExecuteMethod;
        }
        public override void Disable(TerrariaPlugin plugin)
        {
            PlayerHooks.PlayerPostLogin -= ExecuteMethod;
        }

        private void ExecuteMethod(PlayerPostLoginEventArgs e)
        {
            // retrieve player from args
            var player = e.Player;

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
    }
}
