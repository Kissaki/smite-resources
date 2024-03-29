﻿using KCode.SMITEAPI.ResultTypes;
using KCode.SMITEClient.HtmlGenerating.GodTemplates;
using System.Collections.Immutable;

namespace KCode.SMITEClient.HtmlGenerating
{
    internal static class GodsHtml
	{
		public static void GenerateGodsHtml(string targetFile, GodResult[] gods, (ImmutableArray<string> spriteFiles, ImmutableDictionary<string, (int x, int y)> itemOffsets) godIconSprites)
		{
			var (pantheons, roles) = ParseGods(gods);

			var model = new GodsViewModel(gods, pantheons, roles, godIconSprites);
			var content = RazorBase.Render(templatePath: "GodTemplates/GodsHtml.cshtml", model).Result;
			File.WriteAllText(targetFile, content);
		}

		/// <summary>
		/// Parse gods into pantheons and roles, sanitized for CSS use
		/// </summary>
		private static (string[] pantheons, string[] roles) ParseGods(GodResult[] gods)
		{
			var pantheons = gods.Select(x => x.Pantheon!).Distinct().OrderBy(x => x).ToArray();
			var roles = gods.Select(x => x.Roles!.Trim()).Distinct().OrderBy(x => x).ToArray();
			return (pantheons, roles);
		}
	}
}
