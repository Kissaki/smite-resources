using System.Text.RegularExpressions;

namespace KCode.SMITEAPI
{
    internal static class MethodNameIdentifier
    {
        public static string GetName(string methodPath) => Regex.Match(methodPath, "^/(?<name>[a-zA-Z0-9]+)").Groups["name"].Value;
    }
}
