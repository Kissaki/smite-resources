using KCode.SMITEClient.Data;
using System;
using System.IO;

namespace KCode.SMITEClient.HtmlGenerating
{
    public static class GodsSkinsHtml
	{
		public static void GenerateGodsHtml(string targetFile, GodJsonModel[] gods)
		{
			if (gods == null) throw new ArgumentNullException(nameof(gods));
			foreach (var god in gods)
            {
				if (god.Skins == null) throw new InvalidOperationException();
				foreach (var skin in god.Skins)
                {
					if (skin.Name == $"Standard {god.Name}")
                    {
						skin.Name = "Standard";
                    }
                }
            }
			var content = RazorBase.Render(templatePath: "GodSkinsTemplates/GodSkinsHtml.cshtml", gods).Result;
			File.WriteAllText(targetFile, content);
		}
	}
}
