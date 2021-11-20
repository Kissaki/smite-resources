using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace KCode.SMITEClient.HtmlGenerating
{
    public static class HtmlViewHelpers
    {
        public static string GetResourceRef(string filename) => $"{filename}?{File.GetLastWriteTimeUtc(filename):yyyyMMddHHmmss}";
        [SuppressMessage("Globalization", "CA1308:Normalize strings to uppercase", Justification = "<Pending>")]
        public static string ToCSSClass([DisallowNull] string value) => value?.Trim()?.Replace(" ", "", ignoreCase: false, culture: CultureInfo.InvariantCulture)?.ToLowerInvariant() ?? throw new ArgumentNullException(paramName: nameof(value));
    }
}
