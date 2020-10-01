using KCode.SMITEClient.Data;
using System;
using System.IO;

namespace KCode.SMITEClient.HtmlGenerating
{
    internal static class GodSkinThemeHtml
	{
		public static void Generate(string targetFile, GodJsonModel[] gods, SkinThemeMapping mapping)
		{
			if (mapping == null) throw new ArgumentNullException(nameof(mapping));

			foreach (var god in gods)
			{
				foreach (var skin in god.Skins!)
                {
					if (skin.Name == $"Standard {god.Name}")
                    {
						skin.Name = "Standard";
                    }

					switch (skin.Name)
                    {
						case "Standard":
							mapping.Add(skin.Id1, "standard");
							break;
						case "Golden":
							mapping.Add(skin.Id1, "golden");
							break;
						case "Legendary":
							mapping.Add(skin.Id1, "legendary");
							break;
						case "Diamond":
							mapping.Add(skin.Id1, "diamond");
							break;
						case "Shadow":
							mapping.Add(skin.Id1, "shadow");
							break;
						default:
							break;
                    }
                }
			}

			var content = RazorBase.Render(templatePath: "GodSkinThemeTemplates/GodSkinThemesHtml.cshtml", (gods, mapping)).Result;
			File.WriteAllText(targetFile, content);
		}
	}
}
