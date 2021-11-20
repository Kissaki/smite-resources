using KCode.SMITEClient.Data;
using System.Collections.Immutable;

namespace KCode.SMITEClient.HtmlGenerating.ItemsTemplates
{
    public class ItemsViewModel
    {
        public IEnumerable<ItemModel> StarterItems { get; }
        public IEnumerable<ItemModel> Consumables { get; }
        public IEnumerable<ItemModel> Relics { get; }
        public IEnumerable<ItemModel> NormalItems { get; }

        public ItemsViewModel(ItemJsonModel[] items)
        {
            var all = items.Select(x => new ItemModel(x)).OrderBy(x => x.Name).ToImmutableArray();
            var rootItems = all.Where(x => x.RootItemId == x.ItemId);
            foreach (var item in rootItems)
            {
                AddChildren(item, all);
            }
            StarterItems = rootItems.Where(x => x.IsStartingItem).ToImmutableArray();
            Consumables = rootItems.Where(x => x.Type == "Consumable").ToImmutableArray();
            Relics = rootItems.Where(x => x.Type == "Active").ToImmutableArray();
            NormalItems = rootItems.Where(x => x.Type == "Item" && !x.IsStartingItem).ToImmutableArray();
        }

        private static void AddChildren(ItemModel parent, ImmutableArray<ItemModel> all)
        {
            parent.Children = all.Where(x => x.ParentItemId == parent.ItemId && x.ItemId != parent.ItemId);
            foreach (var child in parent.Children)
            {
                AddChildren(child, all);
                child.Parent = parent;
            }
        }
    }
}
