using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace KCode.SMITEClient.Data
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<Pending>")]
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
        public ImmutableDictionary<string, string> Properties { get; set; } = default!;
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
            Properties = ImmutableDictionary.CreateRange(model.Description.Properties.Select(x => KeyValuePair.Create(x.Description, x.Value)));
            Price = model.Price;
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
