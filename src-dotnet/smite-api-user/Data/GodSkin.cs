using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace KCode.SMITEClient.Data
{
    public class GodSkin : IComparable<GodSkin>
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        #region God
        [JsonPropertyName("god_id")]
        public int GodId { get; set; }
        [JsonPropertyName("godIcon_URL")]
        public Uri GodIconUri { get; set; }
        [JsonPropertyName("god_name")]
        public string GodName { get; set; }
        #endregion God

        [JsonPropertyName("skin_id1")]
        public int Id1 { get; set; }
        [JsonPropertyName("skin_id2")]
        public int Id2 { get; set; }
        [JsonPropertyName("skin_name")]
        public string Name { get; set; }

        [JsonPropertyName("godSkin_URL")]
        public Uri CardUri { get; set; }
        [JsonPropertyName("obtainability")]
        public string Obtainability { get; set; }

        [JsonPropertyName("price_favor")]
        public int PriceFavor { get; set; }
        [JsonPropertyName("price_gems")]
        public int PriceGems { get; set; }
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

        public int CompareTo([AllowNull] GodSkin other)
        {
            // If other is not a valid object reference, then this instance is greater
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8604 // Possible null reference argument.
            if (other == null) return 1;
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            var name1 = Name;
            var name2 = other.Name;
            if (name1 == "Standard") return -1;
            if (name2 == "Standard") return 1;
            if (name1 == "Golden") return -1;
            if (name2 == "Golden") return 1;
            if (name1 == "Legendary") return -1;
            if (name2 == "Legendary") return 1;
            if (name1 == "Diamond") return -1;
            if (name2 == "Diamond") return 1;

            var obt = Obtainability;
            var othobt = other.Obtainability;
            if (obt != othobt)
            {
                IEnumerable<string> obtOrder = ImmutableArray.Create("Normal", "Unlimited", "Crossover", "Exclusive", "Limtied");
                foreach (var x in obtOrder)
                {
                    // This obtainability comes before other
                    if (x == obt) return -1;
                    // Other obtainability comes before this
                    if (x == othobt) return 1;
                }
            }

            var aFavor = PriceFavor;
            var bFavor = other.PriceFavor;
            if (aFavor != bFavor)
            {
                if (aFavor == 0) return 1;
                if (bFavor == 0) return -1;
                return aFavor - bFavor;
            }

            var aGems = PriceGems;
            var bGems = other.PriceGems;
            if (aGems != bGems)
            {
                if (aGems == 0) return 1;
                if (bGems == 0) return -1;
                return aGems - bGems;
            }

            return string.Compare(name1, name2, ignoreCase: false, culture: null);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is null) return false;
            if (obj is GodSkin other) return this == other;
            return false;
        }

        public override int GetHashCode() => throw new NotImplementedException();
        public static bool operator ==(GodSkin left, GodSkin right) => left is null ? right is null : left.CompareTo(right) == 0;
        public static bool operator !=(GodSkin left, GodSkin right) => !(left == right);
        public static bool operator <(GodSkin left, GodSkin right) => left is null ? right is object : left.CompareTo(right) < 0;
        public static bool operator <=(GodSkin left, GodSkin right) => left is null || left.CompareTo(right) <= 0;
        public static bool operator >(GodSkin left, GodSkin right) => left is object && left.CompareTo(right) > 0;
        public static bool operator >=(GodSkin left, GodSkin right) => left is null ? right is null : left.CompareTo(right) >= 0;
    }
}
