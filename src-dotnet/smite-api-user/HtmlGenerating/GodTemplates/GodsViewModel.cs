using KCode.SMITEAPI.ResultTypes;
using System.Collections.Immutable;

namespace KCode.SMITEClient.HtmlGenerating.GodTemplates
{
    public class GodsViewModel
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public GodResult[] Gods { get; }
        public string[] Pantheons { get; }
        public string[] Roles { get; }
#pragma warning restore CA1819 // Properties should not return arrays
        public ImmutableArray<string> GodIconSpriteFiles { get; }
        public ImmutableDictionary<string, (int x, int y)> GodIconSpriteOffsets { get; }

        public GodsViewModel(GodResult[] gods, string[] pantheons, string[] roles, (ImmutableArray<string> spriteFiles, ImmutableDictionary<string, (int x, int y)> itemOffsets) godIconSprites)
        {
            Gods = gods;
            Pantheons = pantheons;
            Roles = roles;
            GodIconSpriteFiles = godIconSprites.spriteFiles;
            GodIconSpriteOffsets = godIconSprites.itemOffsets;
        }
    }
}
