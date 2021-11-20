using KCode.SMITEClient.Data;
using KCode.SMITEClient.HtmlGenerating.ItemsTemplates;

namespace KCode.SMITEClient.HtmlGenerating
{
    internal static class ItemsHtml
    {
        public static void Generate(string targetFile, ItemJsonModel[] items)
        {
            var model = new ItemsViewModel(items);
            var content = RazorBase.Render(templatePath: "ItemsTemplates/ItemsHtml.cshtml", model).Result;
            File.WriteAllText(targetFile, content);
        }
    }
}
