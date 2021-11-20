namespace KCode.SMITEClient.Data
{
    /// <summary>skinId1 => theme</summary>
    public class SkinThemeMapping : Dictionary<int, string>
    {
        public SkinThemeMapping(Dictionary<int, string> other)
            : base(other)
        {
        }
    }
}
