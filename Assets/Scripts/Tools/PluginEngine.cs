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
public interface IProgram : IPlugin {  // Для виртуальных компов
    Form Form { get; set; }
}

public static class PluginEngine
{
    public delegate void CompiledPluginStateHandler(string programName);
    public static CompiledPluginStateHandler onPluginCompiled;
    public static CompiledPluginStateHandler onProgramCompiled;
    // public static event CompiledPluginStateHandler notify;

    // private static readonly string runtimePath = @"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\{0}.dll";
    private static readonly string programFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestProgramFolder");
    private static readonly string pluginFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TestPluginFolder");
    
    public static List<IPlugin> PluginsAndPrograms { get; private set; } = new List<IPlugin>();
    // public static List<IProgram> Programs { get; private set; } = new List<IProgram>();









    public static bool Compile(string code, bool isProgramCompilation, out string errorMessage) {
        if (CreateCompilation(code, out CSharpCompilation compilation, out string compilationName, out errorMessage)) {
            EmitResult emitResult = compilation.Emit(Path.Combine(isProgramCompilation ? programFolderPath : pluginFolderPath, $"{compilationName}.dll"));

            Debug.Log("<color=Red>Plugin engine: Success</color>");
            onProgramCompiled(compilationName);
            return true;
        } else {
            Debug.LogError("PE: " + errorMessage);
            return false;
        }
    }

    private static bool CreateCompilation(string code, out CSharpCompilation compilation, out string compilationName, out string errorMessage) {
        SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

        if (CheckTree(syntaxTree, out compilationName, out errorMessage)) {
            compilation = CSharpCompilation.Create(
                compilationName,
                syntaxTrees: new[] { syntaxTree },
                references: new[] {
                    // MetadataReference.CreateFromFile(string.Format(runtimePath, "mscorlib")),
                    // MetadataReference.CreateFromFile(string.Format(runtimePath, "System")),
                    // MetadataReference.CreateFromFile(string.Format(runtimePath, "System.Core")),

                    MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(UnityEngine.UI.Button).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(UnityEngine.Transform).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(GameLevel).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(PluginEngine).Assembly.Location),
                    MetadataReference.CreateFromFile(typeof(FormButtonHandler).Assembly.Location)
                },
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
            );

            IEnumerable<Diagnostic> failures = compilation.GetDiagnostics()
                .Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);
            if (failures.Count<Diagnostic>() > 0) {
                errorMessage = string.Empty;
                foreach (var diagnostic in failures) {
                    errorMessage += $"Line {diagnostic.Location.GetLineSpan().StartLinePosition.Line}: {diagnostic.GetMessage()}\n"; // есть если что, на всякий, diagnostic.Id
                }
                return false;
            } else {
                return true;
            }
        } else {
            compilation = null;
            return false;
        }
    }

    private static bool CheckTree(SyntaxTree tree, out string compilationName, out string errorMessage) {
        SyntaxNode root = tree.GetRoot();

        IEnumerable<Diagnostic> failures = root.GetDiagnostics()
            .Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);
        if (failures.Count<Diagnostic>() > 0) {
            errorMessage = string.Empty;
            foreach (var diagnostic in failures) {
                errorMessage += $"Line {diagnostic.Location.GetLineSpan().StartLinePosition.Line}: {diagnostic.GetMessage()}\n"; // есть если что, на всякий, diagnostic.Id
            }
            compilationName = null;
            return false;
        }


        var usingNames = root.DescendantNodes().OfType<UsingDirectiveSyntax>();
        foreach (var usingName in usingNames) {
            Debug.Log("<color=Red>PE - Usings:</color> " + usingName.Name);
            compilationName = null;
            // return false;
        }

        var compilationNameDeclaration = root.DescendantNodes().OfType<PropertyDeclarationSyntax>().Where(name => name.Identifier.Value.ToString() == "Name");
        compilationName = compilationNameDeclaration.First<PropertyDeclarationSyntax>().Initializer.Value.ToString();
        compilationName = compilationName.Remove(0, 1);
        compilationName = compilationName.Remove(compilationName.Length - 1, 1);

        errorMessage = null;
        return true;
    }

    // public static bool CheckTreeFor<T>(SyntaxTree tree, out string syntaxError) where T: IEnumerable<SyntaxToken> {
    //     SyntaxNode root = tree.GetRoot();

    //     IEnumerable<SyntaxToken> declaredIdentifers = root.DescendantNodes()
    //         .Where(an => an is T)
    //         .Cast<T>()
    //         .Select(vd => vd.Identifier);
        
    //     syntaxError = string.Empty;
    //     return true;
    // }

    public static T GetLibrary<T>(string name) where T: IPlugin {
        foreach (var library in PluginsAndPrograms) {
            if (library.Name == name) {
                return (T)library;
            }
        }
        return default(T);
    }

    public static void RefreshLibraries<T>() where T: IPlugin {
        bool isPlugin = typeof(T) == typeof(IPlugin); // false == IProgram
        string libraryFolder = isPlugin ? pluginFolderPath : programFolderPath;
        PluginsAndPrograms.Clear();

        DirectoryInfo libraryDirectory = new DirectoryInfo(libraryFolder);
        if (!libraryDirectory.Exists) {
            libraryDirectory.Create();
            return;
        }

        string[] files = Directory.GetFiles(libraryFolder, "*.dll");
        foreach (var file in files) {
            Assembly assembly = Assembly.Load(File.ReadAllBytes(file));
            // Assembly assembly = Assembly.LoadFrom(file);
            IEnumerable<Type> types = assembly.GetTypes()
                .Where(t => t.GetInterfaces()
                .Where(i => i.FullName == typeof(IPlugin).FullName).Any());

            foreach (var type in types) {
                IPlugin plugin = isPlugin ? plugin = assembly.CreateInstance(type.FullName) as IPlugin : plugin = assembly.CreateInstance(type.FullName) as IProgram;
                PluginsAndPrograms.Add(plugin);
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
