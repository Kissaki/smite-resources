using RazorLight;
using System;
using System.IO;
using System.Threading.Tasks;

namespace KCode.SMITEClient.HtmlGenerating
{
    public static class RazorBase
    {
        internal static Task<string> Render(string templatePath, object model)
        {
            var engine = CreateEngine2(Path.Combine(Environment.CurrentDirectory, "src-dotnet", "smite-api-user", "HtmlGenerating"));
            return engine.CompileRenderAsync(templatePath, model);
        }

        internal static RazorLightEngine CreateEngine(string templateDir) => CreateEngine2(Path.Combine(Environment.CurrentDirectory, "src-dotnet", "smite-api-user", "HtmlGenerating", templateDir));

        private static RazorLightEngine CreateEngine2(string templatePath)
        {
            return new RazorLightEngineBuilder()
                .UseFileSystemProject(root: templatePath)
                .UseMemoryCachingProvider()
                .Build();
        }
    }
}
