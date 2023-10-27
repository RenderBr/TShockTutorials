using System;
using TerrariaApi.Server;
using TShockAPI;
using TShockAPI.Hooks;
using TShockTutorials.Models;

namespace TShockTutorials.Events
{
    public class OnServerReload : Event
    {
        public override void Enable(TerrariaPlugin plugin)
        {
            GeneralHooks.ReloadEvent += ExecuteMethod;
        }
        public override void Disable(TerrariaPlugin plugin)
        {
            GeneralHooks.ReloadEvent -= ExecuteMethod;
        }

        private void ExecuteMethod(ReloadEventArgs e)
        {
            var playerReloading = e.Player;

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
    }
}
