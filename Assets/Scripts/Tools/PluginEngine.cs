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
public interface IProgram : IPlugin { // Для виртуальных компов
    Form Form { get; set; }
}

public static class PluginEngine
{
    // private static readonly string runtimePath = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\{0}.dll";
    private static readonly string programFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestProgramFolder");
    private static readonly string pluginFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestPluginFolder");
    
    public static List<IProgram> Programs { get; private set; } = new List<IProgram>();
    
    public static List<IPlugin> Plugins { get; private set; } = new List<IPlugin>();

    public static bool Compile(string code, string assemblyName, bool isProgramCompilation)
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
                MetadataReference.CreateFromFile(typeof(UnityEngine.Transform).Assembly.Location),
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
            EmitResult emitResult = compilation.Emit(Path.Combine(isProgramCompilation ? programFolderPath : pluginFolderPath, $"{assemblyName}.dll"));

            if (!emitResult.Success) {
                string errorMessage = string.Empty;

                IEnumerable<Diagnostic> failures = emitResult.Diagnostics
                    .Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);

                foreach (var diagnostic in failures.OrderBy(o => o.Location.GetLineSpan().StartLinePosition.Line))
                {
                    errorMessage += $"({diagnostic.Location.GetLineSpan().StartLinePosition.Line}) {diagnostic.Id}: {diagnostic.GetMessage()}\n";
                }

                Debug.LogError(errorMessage);
                return false;
            } else {
                Debug.Log("Success");
                return true;
            }
        } catch (Exception e) {
            Debug.LogError(e.Message);
            return false;
        }
    }

    public static IProgram GetProgram(string programName) {
        foreach (var program in Programs) {
            if (program.Name == programName) {
                return program;
            }
        }
        return null;
    }

    public static void GetPlugin() {

    }

    public static void RefreshPrograms() { // TODO: Необходимо объединить с RefreshPlugins()
        Programs.Clear();

        DirectoryInfo programsDirectory = new DirectoryInfo(programFolderPath);
        if (!programsDirectory.Exists) {
            programsDirectory.Create();
            return;
        }

        string[] files = Directory.GetFiles(programFolderPath, "*.dll");
        foreach (var file in files) {
            Assembly assembly = Assembly.LoadFrom(file);
            IEnumerable<Type> types = assembly.GetTypes()
                .Where(t => t.GetInterfaces()
                .Where(i => i.FullName == typeof(IProgram).FullName).Any());

            foreach (var type in types) {
                IProgram program = assembly.CreateInstance(type.FullName) as IProgram;
                Plugins.Add(program);
            }
        }
    }

    public static void RefreshPlugins() { // TODO: Необходимо объединить с RefreshPrograms()
        Plugins.Clear();

        DirectoryInfo pluginsDirectory = new DirectoryInfo(pluginFolderPath);
        if (!pluginsDirectory.Exists) {
            pluginsDirectory.Create();
            return;
        }

        string[] files = Directory.GetFiles(pluginFolderPath, "*.dll");
        foreach (var file in files) {
            Assembly assembly = Assembly.LoadFrom(file);
            IEnumerable<Type> types = assembly.GetTypes()
                .Where(t => t.GetInterfaces()
                .Where(i => i.FullName == typeof(IPlugin).FullName).Any());

            foreach (var type in types) {
                IPlugin plugin = assembly.CreateInstance(type.FullName) as IPlugin;
                Plugins.Add(plugin);
            }
        }
    }

    public static void DeleteProgram(string programName) { // TODO: Необходимо объединить с DeletePlugin()

    }

    public static void DeletePlugin(string pluginName) { // TODO: Необходимо объединить с DeleteProgram()

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
                typeof(UnityEngine.Transform).Assembly,
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
