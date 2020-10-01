using System.Text.RegularExpressions;

namespace KCode.SMITEAPI.Generator
{
    public class ApiMethod
    {
        public string Name { get; }
        public string Path { get; }
        public string Description { get; }

        public ApiMethod(string path, string desc)
        {
            Path = path;
            Description = desc;
            Name = Regex.Match(Path, "^/(?<name>[a-zA-Z]+)").Groups["name"].Value;
        }
    }
}
