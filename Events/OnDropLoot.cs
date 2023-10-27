using Microsoft.Xna.Framework;
using OTAPI;
using TerrariaApi.Server;
using TShockAPI;
using TShockTutorials.Models;

namespace TShockTutorials.Events
{
    public class OnDropLoot : Event
    {
        public override void Disable(TerrariaPlugin plugin)
        {
            Hooks.NPC.DropLoot -= DropLoot;
        }

        public override void Enable(TerrariaPlugin plugin)
        {
            Hooks.NPC.DropLoot += DropLoot;
        }

        private void DropLoot(object _, Hooks.NPC.DropLootEventArgs e)
        {
            TSPlayer.All.SendMessage($"Cancelled loot drop for: {e.Npc.FullName}", Color.IndianRed);
            e.Result = HookResult.Cancel;
        }
    }
}
