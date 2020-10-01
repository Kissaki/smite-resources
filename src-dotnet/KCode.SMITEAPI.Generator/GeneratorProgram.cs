using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;

namespace KCode.SMITEAPI.Generator
{
    class Program
    {
        private enum Mode
        {
            FindMethodsHeadline,
            FindPath,
            FindDescription,
        }

        public static void Main()
        {
            var filepath = @"Smite _ Paladins _ Realm API Developer Guide.txt";
            var identifiedMethods = ReadMethods(filepath: filepath);

            WriteMethods(identifiedMethods, filepath: "methods");

            CreateOpenApiDocument(identifiedMethods, filepath: "openapi.yaml");

            Console.WriteLine("Completed!");
        }

        private static IEnumerable<ApiMethod> ReadMethods(string filepath)
        {
            if (!File.Exists(filepath))
            {
                Console.Error.WriteLine($"Input file does not exist or is not accessible at {filepath}");
            }
            Console.WriteLine($"Reading {filepath}…");

            using var sr = new StreamReader(filepath, Encoding.UTF8);

            var identifiedMethods = new List<ApiMethod>();

            var mode = Mode.FindMethodsHeadline;
            string? line;
            string? path = null;
            while ((line = sr.ReadLine()) != null)
            {
                switch (mode)
                {
                    case Mode.FindMethodsHeadline:
                        if (line != "API METHODS & PARAMETERS") continue;

                        Console.WriteLine("Found methods header");

                        mode = Mode.FindPath;
                        break;
                    case Mode.FindPath:
                        if (!line.StartsWith("/", StringComparison.InvariantCulture)) continue;

                        path = line;

                        mode = Mode.FindDescription;
                        break;
                    case Mode.FindDescription:
                        if (line.Length == 0) throw new InvalidOperationException("Empty line when expecting description");
                        if (path == null) throw new InvalidOperationException();

                        var description = line;

                        Console.WriteLine($"Identified method {path}");
                        identifiedMethods.Add(new ApiMethod(path: path, desc: description));
                        mode = Mode.FindPath;
                        break;
                }
            }

            return identifiedMethods.ToArray();
        }

        private static void WriteMethods(IEnumerable<ApiMethod> identifiedMethods, string filepath)
        {
            var fi = new FileInfo(filepath);
            Console.WriteLine($"Writing methods file to {fi.FullName}…");
            using var methodWriter = File.CreateText(fi.FullName);
            foreach (var method in identifiedMethods)
            {
                methodWriter.WriteLine(method.Name);
                methodWriter.WriteLine(method.Path);
                methodWriter.WriteLine(method.Description);
                methodWriter.WriteLine();
            }
        }

        private static void CreateOpenApiDocument(IEnumerable<ApiMethod> identifiedMethods, string filepath)
        {
            var fi = new FileInfo(filepath);
            Console.WriteLine($"Creating OpenAPI document at {fi.FullName}…");
            var document = CreateOpenApiDocument(identifiedMethods);
            var targetYaml = File.CreateText(fi.FullName);
            var w = new OpenApiYamlWriter(targetYaml);
            document.SerializeAsV3(w);
            targetYaml.Flush();
        }

        private static OpenApiDocument CreateOpenApiDocument(IEnumerable<ApiMethod> methods)
        {
            var document = new OpenApiDocument
            {
                Info = new OpenApiInfo
                {
                    Version = "2020.02.20",
                    Title = "SMITE API",
                },
                Servers = new List<OpenApiServer>
                {
                    new OpenApiServer { Url = "http://api.smitegame.com/smiteapi.svc" }
                },
                Paths = new OpenApiPaths(),
            };
            foreach (var method in methods)
            {
                document.Paths.Add(method.Path, CreateOpenApiPath(method.Description));
            }
            return document;
        }

        private static OpenApiPathItem CreateOpenApiPath(string description)
        {
            return new OpenApiPathItem
            {
                Operations = new Dictionary<OperationType, OpenApiOperation>
                {
                    [OperationType.Get] = new OpenApiOperation
                    {
                        Description = description,
                        Responses = new OpenApiResponses
                        {
                            ["200"] = new OpenApiResponse
                            {
                                Description = "OK"
                            }
                        }
                    }
                },
            };
        }
    }
}
