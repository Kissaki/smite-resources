using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace KCode.SMITEClient.Data
{
    [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "<Pending>")]
    [SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>")]
    [SuppressMessage("Design", "CA1034:Nested types should not be visible", Justification = "<Pending>")]
    public class ItemJsonModel
    {
        public class ItemDescription
        {
            public class ItemProperty
            {
                // e.g. "Health"
                public string Description { get; set; } = default!;
                // e.g. "+75"
                public string Value { get; set; } = default!;
            }

            // e.g. "Physical Protection and Health."
            [NotNull, DisallowNull]
            public string Description { get; set; } = default!;
            public string? SecondaryDescription { get; set; }
            [NotNull, DisallowNull]
            [JsonPropertyName("Menuitems")]
            public ItemProperty[] Properties { get; set; } = default!;
        }

        public int RootItemId { get; set; }
        public int ItemId { get; set; }
        // Parent ID, "0"
        public int ChildItemId { get; set; }

        public int ItemTier { get; set; }
        public string DeviceName { get; set; } = default!;

        // "Item", Consumable, Active
        public string Type { get; set; } = default!;
        /// <remarks>"y"</remarks>
        [JsonPropertyName("ActiveFlag")]
        public string IsActive { get; set; } = default!;
        public string RestrictedRoles { get; set; } = default!;
        public bool StartingItem { get; set; }

        public int IconId { get; set; }
        [JsonPropertyName("itemIcon_URL")]
        public string IconUrl { get; set; } = default!;

        public string ShortDesc { get; set; } = default!;
        [JsonPropertyName("ItemDescription")]
        public ItemDescription Description { get; set; } = default!;
        public int Price { get; set; }
    }
}
