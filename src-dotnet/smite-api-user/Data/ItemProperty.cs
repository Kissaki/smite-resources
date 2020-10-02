namespace KCode.SMITEClient.Data
{
    public class ItemProperty
    {
        public string Caption { get; set; } = default!;
        public string FullLabel { get; set; } = default!;
        public string Value { get; set; } = default!;
        public string? Color { get; internal set; } = default;
        public string CssClass { get; internal set; } = default!;
    }
}
