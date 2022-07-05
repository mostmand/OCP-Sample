using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace OpenClosedPrinciple.SourceGeneration.ShapeFactoryGeneration;

public class SyntaxReceiver : ISyntaxReceiver
{
    public ICollection<ClassDeclarationSyntax> CandidateClasses { get; } = new List<ClassDeclarationSyntax>();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax)
        {
            CandidateClasses.Add(classDeclarationSyntax);
        }
    }
}