using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public interface IPlugin
{
    string Name { get; }
    string Description { get; }
    void Main();
}
public interface IProgram : IPlugin { } // Для красоты xD

public static class PluginEngine
{
    // private static readonly string runtimePath = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\{0}.dll";
    private static readonly string pluginFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestPluginFolder");
    public static List<IPlugin> Plugins { get; private set; } = new List<IPlugin>();
    public static string[] GetPluginsName
    {
        get {
            string[] pluginsName = new string[Plugins.Count];
            for (int i = 0; i < Plugins.Count; i++) {
                pluginsName[i] = Plugins[i].Name;
            }
            return pluginsName;
        }
    }

    public static void Compile(string code, string assemblyName, bool toSave = true)
    {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

        CSharpCompilation compilation = CSharpCompilation.Create(
            assemblyName,
            syntaxTrees: new[] { syntaxTree },
            references: new[] {
                // MetadataReference.CreateFromFile(string.Format(runtimePath, "mscorlib")),
                // MetadataReference.CreateFromFile(string.Format(runtimePath, "System")),
                // MetadataReference.CreateFromFile(string.Format(runtimePath, "System.Core")),

                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                // MetadataReference.CreateFromFile(typeof(UnityEngine.Transform).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(GameLevel).Assembly.Location)
            },
            options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        
        // using (MemoryStream dllStram = new MemoryStream()) {
        //     using (MemoryStream pdbStream = new MemoryStream()) {
        //         EmitResult emitResults = compilation.Emit(dllStram, pdbStream);
        //         if (!emitResults.Success) {
        // 
        //         }
        //     }
        // }

        try {
            EmitResult emitResult = compilation.Emit(Path.Combine(pluginFolderPath, $"{assemblyName}.dll"));
            if (!emitResult.Success) {
                string errorMessage = string.Empty;

                IEnumerable<Diagnostic> failures = emitResult.Diagnostics
                    .Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);

                foreach (var diagnostic in failures.OrderBy(o => o.Location.GetLineSpan().StartLinePosition.Line))
                {
                    errorMessage += $"({diagnostic.Location.GetLineSpan().StartLinePosition.Line}) {diagnostic.Id}: {diagnostic.GetMessage()}\n";
                }

                Debug.LogError(errorMessage);
            } else {
                Debug.Log("Success");
            }
        } catch (Exception e) {
            Debug.LogError(e.Message);
        }
    }

    public static void RefreshPlugins()
    {
        Plugins.Clear();

        DirectoryInfo pluginsDirectory = new DirectoryInfo(pluginFolderPath);
        if (!pluginsDirectory.Exists) {
            pluginsDirectory.Create();
            return;
        }

        string[] pluginFilesPath = Directory.GetFiles(pluginFolderPath, "*.dll");
        foreach (var filePath in pluginFilesPath) {
            Assembly assembly = Assembly.LoadFrom(filePath);
            IEnumerable<Type> types = assembly.GetTypes()
                .Where(t => t.GetInterfaces()
                .Where(i => i.FullName == typeof(IPlugin).FullName).Any());

            foreach (var type in types) {
                IPlugin plugin = assembly.CreateInstance(type.FullName) as IPlugin;
                Plugins.Add(plugin);
            }
        }
    }

    public static class InteractiveSharp
    {
        // private static Script script;
        // private static ScriptState scriptState;
        private static ScriptOptions scriptOptions = ScriptOptions.Default
            .WithImports(
                "System",
                "System.IO",
                "System.Collections",
                "System.Collections.Generic",
                "UnityEngine"
            ).AddReferences(
                typeof(Server).Assembly,
                typeof(ControllerList).Assembly,
                typeof(BotBehaviorList).Assembly
            );

        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public static void Execute(string code) {
            // try {
            //     scriptState = scriptState == null ? CSharpScript.RunAsync(code, scriptOptions).Result : scriptState.ContinueWithAsync(code, scriptOptions).Result;

            //     if (scriptState.ReturnValue != null) {
            //         // Console.WriteLine("Debug: kek is " + scriptState.GetVariable("kek").Value);
            //         return scriptState.ReturnValue;
            //     } else {
            //         return "Команда выполнена";
            //     }
            // }
            // catch (Exception e) {
            //     return e.Message;
            // }

            try {
                CSharpScript.RunAsync(code, options: scriptOptions, cancellationToken: cancellationTokenSource.Token);
            } catch (Exception e) {
                Debug.LogError("Error: " + e.Message);
            }
        }

        public static void ExecuteAsync(string code)
        {
            Task.Run(() => Execute(code), cancellationTokenSource.Token);
        }

        public static void Break()
        {
            cancellationTokenSource.Cancel();
        }
    }
}
