using System.Collections.Immutable;

namespace KCode.SMITEClient.Data
{
    public class ItemModel
    {
        public ItemModel? Parent { get; set; }
        public IEnumerable<ItemModel> Children { get; set; } = Enumerable.Empty<ItemModel>();

        public int RootItemId { get; set; }
        public int ItemId { get; set; }
        public int ParentItemId { get; set; }

        public int ItemTier { get; set; }
        public string Name { get; set; } = default!;

        // "Item"
        public string Type { get; set; } = default!;
        public bool IsStartingItem { get; set; }
        public bool IsActive { get; set; } = default!;
        public string RestrictedRoles { get; set; } = default!;

        public int IconId { get; set; }
        public string IconUrl { get; set; } = default!;

        public string ShortDesc { get; set; } = default!;
        // e.g. "Physical Protection and Health."
        public string Description { get; set; } = default!;
        public string? SecondaryDescription { get; set; } = default!;
        // e.g. Health: +75
        public IEnumerable<ItemProperty> Properties { get; set; } = default!;
        public int Price { get; set; }

        public int BranchWidth => Children.Select(x => x.BranchWidth).DefaultIfEmpty(1).Sum();
        public int Depth => (Parent?.Depth ?? 0) + 1;
        public ItemModel Root => Parent?.Root ?? this;
        public int CrossPos => GetCrossPos() ?? throw new InvalidOperationException($"Failed to determine {nameof(CrossPos)}");
        public IEnumerable<ItemModel> ChildrenRecursive => Children.Union(Children.SelectMany(x => x.ChildrenRecursive));
        public IEnumerable<ItemModel> AllTreeItems => Root.ChildrenRecursive.Prepend(Root);

        private int? GetCrossPos()
        {
            if (Parent == null) return 0;
            return GetCrossPos(Root, this);
        }

        private static int? GetCrossPos(ItemModel parent, ItemModel item)
        {
            var pos = 0;
            foreach (var child in parent.Children)
            {
                if (child == item) return pos;
                var match = GetCrossPos(child, item);
                if (match != null) return pos + match;
                pos += child.BranchWidth;
            }
            return null;
        }

        public ItemModel(ItemJsonModel model)
        {
            if (model == null) throw new ArgumentNullException(paramName: nameof(model));

            RootItemId = model.RootItemId;
            ItemId = model.ItemId;
            ParentItemId = model.ChildItemId;

            ItemTier = model.ItemTier;
            Name = model.DeviceName;

            Type = model.Type;
            IsStartingItem = model.StartingItem;
            IsActive = model.IsActive == "y";
            RestrictedRoles = model.RestrictedRoles;

            IconId = model.IconId;
            IconUrl = model.IconUrl;

            ShortDesc = model.ShortDesc;
            Description = model.Description.Description;
            SecondaryDescription = model.Description.SecondaryDescription;
            Properties = model.Description.Properties
                .ToDictionary(x => FixItemPropertyName(x.Description), x => x.Value)
                .Where(x => !string.IsNullOrEmpty(x.Key))
                .Select(x => ConvertProperty(x)).OrderBy(x => GetPropIndex(x)).ToImmutableArray();
            Price = model.Price;
        }

        private static string FixItemPropertyName(string description)
        {
            return description.Trim(' ', ':').Replace("protection", "Protection").Replace("CCR", "Crowd Control Reduction");
        }

        private static int GetPropIndex(ItemProperty x)
        {
            var needle = x.FullLabel.ToUpperInvariant();
            var index = Array.FindIndex(DataReference.ItemProperties, x => x.ToUpperInvariant() == needle);
            if (index == -1) throw new NotImplementedException($"Could not get property index for unknown property `{x.FullLabel}`");
            return index;
        }

        private static ItemProperty ConvertProperty(KeyValuePair<string, string> pair)
        {
            var (name, value) = pair;
            return new ItemProperty
            {
                Caption = name switch
                {
                    "Health" => "HP",
                    "Mana" => "MP",
                    "Movement Speed" => "MvSpd",
                    "Attack Speed" => "AtkSpd",
                    "Cooldown Reduction" => "CdRd",
                    "Physical Power" => "Phys Pwr",
                    "Magical Power" => "Mag Pwr",
                    "Physical Penetration" => "Phys Pen",
                    "Magical Penetration" => "Mag Pen",
                    "Physical Protection" => "Phys Prot",
                    "Magical Protection" => "Mag Prot",
                    "Crowd Control Reduction" => "CrwdCtrlRd",
                    "Physical Critical Strike Chance" => "Phys Crit",
                    _ => name,
                },
                FullLabel = name,
                Value = value,
                Color = name switch
                {
                    "Health" => "green",
                    "HP5" => "green",
                    "Mana" => "blue",
                    "MP5" => "blue",
                    _ => null,
                },
                CssClass = name.Replace(" ", "", StringComparison.InvariantCultureIgnoreCase).ToLowerInvariant(),
            };
        }

        public string ToPrint(int indent = 0)
        {
            var sb = new StringBuilder();
            sb.AppendLine(new string('.', indent) + $"{Name} ({Depth} {ItemTier} {CrossPos} w{BranchWidth})");
            foreach (var child in Children)
            {
                sb.Append(child.ToPrint(indent + 1));
            }
            return sb.ToString();
        }
    }
}
