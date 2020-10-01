using KCode.SMITEAPI.ResultTypes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace KCode.SMITEClient.Data
{
    public class DataStore
    {
        public string BasePath { get; }

        public DataStore(string basePath = "data")
        {
            BasePath = basePath;
        }

        public GodResult[] ReadGods() => JsonSerializer.Deserialize<GodResult[]>(File.ReadAllText(GetPath("gods.json")));
        public GodSkin[] ReadGodSkins(int godId) => JsonSerializer.Deserialize<GodSkin[]>(File.ReadAllText(GetPath($"godskins-{godId}.json")));
        public GodJsonModel[] ReadGodsWithSkins() => JsonSerializer.Deserialize<GodJsonModel[]>(File.ReadAllText(GetPath("godswithskins.json")));
        public SkinThemeMapping ReadGodSkinThemes() => new SkinThemeMapping(JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(GetPath("godskin-theme.json"))).ToDictionary(x => int.Parse(x.Key, provider: null), x => x.Value));
        internal ItemJsonModel[] ReadItems() => JsonSerializer.Deserialize<ItemJsonModel[]>(File.ReadAllText(GetPath(filename: "items.json")));
        public void WriteGodsWithSkins(GodJsonModel[] values) => WriteIfChanged(GetPath("godswithskins.json"), JsonSerializer.Serialize(values));

        private string GetPath(string filename) => Path.Combine(BasePath, filename);

        private static void WriteIfChanged(string filepath, string contents)
        {
            if (File.ReadAllText(path: filepath) != contents)
            {
                File.WriteAllText(path: filepath, contents: contents);
            }
        }
    }
}
