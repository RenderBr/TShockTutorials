using TerrariaApi.Server;
using TShockAPI;
using TShockTutorials.Models;
using Terraria;

namespace TShockTutorials.Events
{
    public class OnChestOpen : Event
    {
        public override void Disable(TerrariaPlugin plugin)
        {
            GetDataHandlers.ChestOpen -= ExecuteMethod;
        }

        public override void Enable(TerrariaPlugin plugin)
        {
            GetDataHandlers.ChestOpen += ExecuteMethod;
        }

        public void ExecuteMethod(object _, GetDataHandlers.ChestOpenEventArgs args)
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
    }
}
