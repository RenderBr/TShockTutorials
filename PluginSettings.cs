using Newtonsoft.Json;
using System.IO;
using TShockAPI;

namespace TShockTutorials
{
    public class PluginSettings
    {
        private static string filePath = Path.Combine(TShock.SavePath, "tutorial.json");
        public static PluginSettings Config { get; set; } = new();
        public string GameName { get; set; } = "Weird Fact of the Day";
        public List<string> WeirdFacts { get; set; } = new List<string>()
        {
            "Octopuses have three hearts, two pump blood to the gills, and one pumps it to the rest of the body",
            "Honey never spoils; archaeologists have found pots of honey in ancient Egyptian tombs that are over 3,000 years old.",
            "Bananas are berries, but strawberries are not.",
            "The world's largest desert is not the Sahara; it's Antarctica.",
            "A group of flamingos is called a 'flamboyance.'",
            "The Eiffel Tower can be 6 inches taller during the summer due to the expansion of iron in the heat.",
            "Wombat feces are cube-shaped.",
            "There's a town in Norway called 'Hell,' and it occasionally freezes over.",
            "In Switzerland, it is illegal to own just one guinea pig because they get lonely.",
            "The oldest known sample of the smallpox virus was found in the teeth of a 17th-century child buried in Lithuania."
        };

        [JsonProperty("CoolestNumber", Order = 3)]
        public int ChosenNumber { get; set; } = 666;

        public static void Load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                try
                {
                    Config = JsonConvert.DeserializeObject<PluginSettings>(json);
                }
                catch (Exception ex)
                {
                    TShock.Log.ConsoleError("Config could not load: " + ex.Message);
                    TShock.Log.ConsoleError(ex.StackTrace);
                }
            }
            else
            {
                Save();
            }
        }

        public static void Save()
        {
            string json = JsonConvert.SerializeObject(Config, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }

    }
}
