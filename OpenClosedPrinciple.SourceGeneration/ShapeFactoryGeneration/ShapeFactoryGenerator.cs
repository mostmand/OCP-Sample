using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace OpenClosedPrinciple.SourceGeneration.ShapeFactoryGeneration;

[Generator]
public class ShapeFactoryGenerator : ISourceGenerator
{
    private const string Namespace = "OpenClosedPrinciple.SourceGeneration.ShapeFactoryGeneration";

    private const string GeneratedPrefix = "_generated";
    private const string ShapeAttributeNameText = "ShapeAttribute";
    private const string ShapeTypeNamedArgument = "ShapeType"; 
    
    private const string ShapeAttributeText = @"using System;

namespace " + Namespace + @"
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class " + "ShapeAttribute" + @" : Attribute
    {
        public " + ShapeAttributeNameText + $@"()
        {{
        }}

        public string {ShapeTypeNamedArgument} {{ get; set; }}
    }}
}}
";

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        if (context.SyntaxReceiver is not SyntaxReceiver syntaxReceiver)
        {
            return;
        }

        context.AddSource($"{ShapeAttributeNameText}{GeneratedPrefix}.cs", SourceText.From(ShapeAttributeText, Encoding.UTF8));

        var compilation = GetCompilation(context);
        var shapeAttribute = compilation.GetTypeByMetadataName($"{Namespace}.ShapeAttribute")!;

        var classSymbols = GetClassSymbols(compilation, syntaxReceiver);

        var cases = new StringBuilder();

        foreach (var classSymbol in classSymbols)
        {
            if (!classSymbol.TryGetAttribute(shapeAttribute, out var attributes)) continue;
            var attribute = attributes.Single();
            var shapeType = (string?)attribute.NamedArguments.FirstOrDefault(e => e.Key.Equals("ShapeType")).Value.Value;
            if (string.IsNullOrWhiteSpace(shapeType))
            {
                throw new Exception($"Shape type for type \"{classSymbol.Name}\" is null or whitespace.");
            }

            cases.Append(@$"case ""{shapeType}"":
                return new {classSymbol.ContainingNamespace}.{classSymbol.Name}();
");
        }
        
        var shapeFactoryCode = @$"using System;

namespace {Namespace};

public class GeneratedShapeFactory : OpenClosedPrinciple.IShapeFactory
{{
    public IShape CreateShape(string shapeType)
    {{
        switch (shapeType) 
        {{
            {cases}
            default:
                throw new NotSupportedException();
        }}
    }}
}}
";
        
        context.AddSource($"GeneratedShapeFactory_generated.cs", SourceText.From(shapeFactoryCode, Encoding.UTF8));
    }
    
    private static IEnumerable<INamedTypeSymbol> GetClassSymbols(Compilation compilation, SyntaxReceiver receiver)
    {
        return receiver.CandidateClasses.Select(clazz => GetClassSymbol(compilation, clazz));
    }

    private static INamedTypeSymbol GetClassSymbol(Compilation compilation, ClassDeclarationSyntax clazz)
    {
        var model = compilation.GetSemanticModel(clazz.SyntaxTree);
        var classSymbol = model.GetDeclaredSymbol(clazz)!;
        return classSymbol;
    }
    
    private static Compilation GetCompilation(GeneratorExecutionContext context)
    {
        var options = context.Compilation.SyntaxTrees.First().Options as CSharpParseOptions;

        var compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(ShapeAttributeText, Encoding.UTF8), options));
        return compilation;
    }
}