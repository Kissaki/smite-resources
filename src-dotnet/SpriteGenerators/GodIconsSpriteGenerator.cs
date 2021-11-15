using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace KCode.SMITEClient;

[Generator]
internal class GodIconsSpriteGenerator : ISourceGenerator
{
    public void Initialize(InitializationContext context)
    {
        // No initialization required
    }

    public void Execute(SourceGeneratorContext context)
    {
        var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

        var source = $@"
using System;

namespace {mainMethod!.ContainingNamespace.ToDisplayString()};

public static partial class {mainMethod.ContainingType.Name}
{{
    static partial void HelloFrom(string name)
    {{
        Console.WriteLine($""Generator says: Hi from '{{name}}'"");
    }}
}}
";
        // add the source code to the compilation
        context.AddSource("generatedSource", SourceText.From(source, Encoding.UTF8));
    }
}
