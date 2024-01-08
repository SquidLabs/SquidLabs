using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SquidLabs.Domain.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ValueObjectRecordTypeEnforcementAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "SL001";

    private static readonly DiagnosticDescriptor Rule = new(
        DiagnosticId,
        "Type must be a record",
        "Type '{0}' is implementing 'IValueObject<TKey>' and must be declared as a record",
        "Usage",
        DiagnosticSeverity.Error,
        true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration, SyntaxKind.StructDeclaration);
    }

    private void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        var typeDeclaration = (TypeDeclarationSyntax)context.Node;
        var semanticModel = context.SemanticModel;

        // Cast the returned ISymbol to INamedTypeSymbol
        if (semanticModel.GetDeclaredSymbol(typeDeclaration) is not INamedTypeSymbol typeSymbol) return;

        // Check if the symbol implements IValueObject<TKey>
        if (!typeSymbol.Interfaces.Any(i => i is { Name: "IValueObject", TypeArguments.Length: 1 })) return;

        // Check if the symbol is not a record type
        if (typeSymbol.IsRecord) return;

        var diagnostic = Diagnostic.Create(Rule, typeDeclaration.Identifier.GetLocation(), typeSymbol.Name);
        context.ReportDiagnostic(diagnostic);
    }
}