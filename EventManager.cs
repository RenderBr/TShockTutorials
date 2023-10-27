using TerrariaApi.Server;
using TShockTutorials.Events;
using TShockTutorials.Models;

namespace TShockTutorials
{
    public class EventManager
    {
        #region Events
        public static OnChestOpen OnChestOpen = new();
        public static OnDropLoot OnDropLoot = new();
        public static OnPlayerDisconnect OnPlayerDisconnect = new();
        public static OnPlayerLogin OnPlayerLogin = new();
        public static OnServerReload OnServerReload = new();
        #endregion

        public static List<Event> Events = new List<Event>()
        {
            OnChestOpen,
            OnDropLoot, 
            OnPlayerDisconnect,
            OnPlayerLogin, 
            OnServerReload,
        };

        public static void RegisterAll(TerrariaPlugin plugin)
        {
            foreach(Event _event in Events) 
            {
                _event.Enable(plugin);
            }
        }
    }
}
