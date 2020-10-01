using KCode.SMITEAPI.ResultTypes;

namespace KCode.SMITEClient.HtmlGenerating.GodTemplates
{
    public class GodsViewModel
    {
#pragma warning disable CA1819 // Properties should not return arrays
        public GodResult[] Gods { get; }
        public string[] Pantheons { get; }
        public string[] Roles { get; }
#pragma warning restore CA1819 // Properties should not return arrays

        public GodsViewModel(GodResult[] gods, string[] pantheons, string[] roles)
        {
            Gods = gods;
            Pantheons = pantheons;
            Roles = roles;
        }
    }
}
