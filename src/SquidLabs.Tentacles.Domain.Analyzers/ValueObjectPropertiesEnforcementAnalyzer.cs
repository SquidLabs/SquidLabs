using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace SquidLabs.Domain.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class ValueObjectPropertiesEnforcementAnalyzer: DiagnosticAnalyzer
{
    public const string DiagnosticId = "SL002";

    private static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(
        DiagnosticId,
        "Non-value type property detected",
        "Property '{0}' in type '{1}' is not a value type",
        "Usage",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeType, SyntaxKind.ClassDeclaration, SyntaxKind.StructDeclaration);
    }

    private void AnalyzeType(SyntaxNodeAnalysisContext context)
    {
        var typeDeclaration = (TypeDeclarationSyntax)context.Node;
        var semanticModel = context.SemanticModel;

        if (ModelExtensions.GetDeclaredSymbol(semanticModel, typeDeclaration) is INamedTypeSymbol typeSymbol &&
            typeSymbol.Interfaces.Any(i => i.Name == "IValueObject" && i.TypeArguments.Length == 1))
        {
            foreach (var member in typeSymbol.GetMembers().OfType<IPropertySymbol>())
            {
                if (!IsValueType(member.Type))
                {
                    var diagnostic = Diagnostic.Create(Rule, member.Locations[0], member.Name, typeSymbol.Name);
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }
    }

    private bool IsValueType(ITypeSymbol typeSymbol)
    {
        // Check if the type is a value type. This includes all struct types and some special types like string and DateTime.
        return typeSymbol.IsValueType || typeSymbol.SpecialType == SpecialType.System_String || typeSymbol.SpecialType == SpecialType.System_DateTime;
    }
}