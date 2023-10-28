using Terraria;
using TerrariaApi.Server;

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

            // register our commands
            CommandManager.RegisterAll();

            // register our hooks
            EventManager.RegisterAll(this);
        }
    }
}