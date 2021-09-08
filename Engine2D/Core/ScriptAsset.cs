using Microsoft.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using System.Runtime.Loader;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using System.Linq;
using System.Runtime;
namespace Engine2D {

    public class ScriptAsset
    {

        public string SourcePath { get; set; }
        public string TypeName { get; set; }

        public Type Type { get; set; }

        public ScriptAsset(string sourcePath)
        {
            this.SourcePath = sourcePath;
        }


        //TODO: Clean up
        public bool Compile()
        {
            string source = "";

            try { 
                source = File.ReadAllText(SourcePath);
            }
            catch
            {
                return false;
            }

            var refPaths = new[] {
                typeof(System).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location,
                typeof(List<>).GetTypeInfo().Assembly.Location,
                typeof(GCSettings).GetTypeInfo().Assembly.Location,
                Path.Combine(Path.GetDirectoryName(typeof(Engine2D.EngineGame).GetTypeInfo().Assembly.Location), "Engine2D.dll"),
                Path.Combine(Path.GetDirectoryName(typeof(Framework2D.Game).GetTypeInfo().Assembly.Location), "Framework2D.dll"),
                Path.Combine(Path.GetDirectoryName(typeof(Newtonsoft.Json.Linq.JObject).GetTypeInfo().Assembly.Location), "Newtonsoft.Json.dll"),
                Path.Combine(Path.GetDirectoryName(typeof(OpenTK.Core.Utils).GetTypeInfo().Assembly.Location), "OpenTK.Core.dll"),
                Path.Combine(Path.GetDirectoryName(typeof(OpenTK.Mathematics.Vector3).GetTypeInfo().Assembly.Location), "OpenTK.Mathematics.dll"),
            };

            MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();
            List<MetadataReference> rf = references.ToList();
            Assembly.GetEntryAssembly().GetReferencedAssemblies().ToList().ForEach(a => rf.Add(MetadataReference.CreateFromFile(Assembly.Load(a).Location)));

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(source);
            CSharpCompilation compilation = CSharpCompilation.Create(
                Path.GetRandomFileName(),
                syntaxTrees: new[] { syntaxTree },
                references: rf.ToArray(),
                options: new CSharpCompilationOptions( Microsoft.CodeAnalysis.OutputKind.DynamicallyLinkedLibrary));

            using(MemoryStream ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("\t{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                    return false;
                } else
                {
                    Assembly assembly = Assembly.Load(ms.ToArray());

                    Type[] types = assembly.GetTypes();

                    if (types.Length > 1)
                    {
                        throw new Exception("Only 1 type per file allowed");
                    }

                    if (!types[0].IsSubclassOf(typeof(GameScript)))
                    {
                        throw new Exception("Type must inherit from type GameScript");
                    }

                    this.Type = types[0];
                    this.TypeName = this.Type.Name;


                    return true;
                }
            }
        }
    }
}
