using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;
using SquidLabs.Domain.Analyzers;
using SquidLabs.Tentacles.Domain.Objects;

namespace SquidLabs.Tentacles.Domain.Analyzers.Tests;

public class ValueObjectRecordTypeEnforcementAnalyzerTests
{
    private static readonly ReferenceAssemblies referenceAssemblies =
        new(
            "net8.0",
            new PackageIdentity(
                "Microsoft.NETCore.App.Ref",
                "8.0.0"),
            Path.Combine("ref", "net8.0"));

    private readonly ImmutableArray<string> assemblyNames =
        [typeof(IValueObject<>).Assembly.FullName, typeof(ValueObjectRecordTypeEnforcementAnalyzer).Assembly.FullName];

    [Fact]
    public async Task Analyzer_Should_Report_Diagnostic_For_Non_Record_Type()
    {
        const string testCode = """

                                using SquidLabs.Tentacles.Domain.TestObjects;

                                public class TestClass : IValueObject<int>
                                {
                                    public int Key { get; }
                                    public int GetKey()
                                    {
                                        return Key;
                                    }
                                    public bool Equals(IDomainObject<int>? other)
                                    {
                                        return this.GetKey() == other?.GetKey();
                                    }
                                }
                                """;

        var expectedDiagnostic =
            new DiagnosticResult(ValueObjectRecordTypeEnforcementAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
                .WithLocation(4, 14)
                .WithMessage("Type 'TestClass' is implementing 'IValueObject<TKey>' and must be declared as a record");


        var analyzerTest = new CSharpAnalyzerTest<ValueObjectRecordTypeEnforcementAnalyzer, XUnitVerifier>
        {
            TestCode = testCode,
            ExpectedDiagnostics = { expectedDiagnostic },
            TestState =
            {
                AdditionalReferences = { MetadataReference.CreateFromFile(typeof(IValueObject<>).Assembly.Location) }
            },
            ReferenceAssemblies = referenceAssemblies
        };

        await analyzerTest.RunAsync();
    }

    [Fact]
    public async Task Analyzer_Should_Not_Report_Diagnostic_For_Record_Type()
    {
        const string testCode = """

                                using SquidLabs.Tentacles.Domain.TestObjects;

                                public record TestClass : IValueObject<int>
                                {
                                    public int Key { get; }
                                    public int GetKey()
                                    {
                                        return Key;
                                    }
                                    public bool Equals(IDomainObject<int>? other)
                                    {
                                        return this.GetKey() == other?.GetKey();
                                    }
                                }
                                """;

        var analyzerTest = new CSharpAnalyzerTest<ValueObjectRecordTypeEnforcementAnalyzer, XUnitVerifier>
        {
            TestCode = testCode,
            TestState =
            {
                AdditionalReferences = { MetadataReference.CreateFromFile(typeof(IValueObject<>).Assembly.Location) }
            },
            ReferenceAssemblies = referenceAssemblies
        };

        await analyzerTest.RunAsync();
    }

    [Fact]
    public async Task Analyzer_Should_Report_Diagnostic_For_Struct_Type()
    {
        const string testCode = """

                                using SquidLabs.Tentacles.Domain.TestObjects;

                                public struct TestStruct : IValueObject<int>
                                {
                                    public int Key { get; }
                                    public int GetKey()
                                    {
                                        return Key;
                                    }
                                    public bool Equals(IDomainObject<int>? other)
                                    {
                                        return this.GetKey() == other?.GetKey();
                                    }
                                }
                                """;

        var expectedDiagnostic =
            new DiagnosticResult(ValueObjectRecordTypeEnforcementAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
                .WithSpan(4, 15, 4, 25)
                .WithMessage("Type 'TestStruct' is implementing 'IValueObject<TKey>' and must be declared as a record")
                .WithArguments("TestClass");

        await new CSharpAnalyzerTest<ValueObjectRecordTypeEnforcementAnalyzer, XUnitVerifier>
        {
            TestCode = testCode,
            ExpectedDiagnostics = { expectedDiagnostic },
            TestState =
            {
                AdditionalReferences = { MetadataReference.CreateFromFile(typeof(IValueObject<>).Assembly.Location) }
            },
            ReferenceAssemblies = referenceAssemblies
        }.RunAsync();
    }

    [Fact]
    public async Task Analyzer_Should_Not_Report_Diagnostic_For_Generic_Record_Type()
    {
        const string testCode = """

                                using SquidLabs.Tentacles.Domain.TestObjects;

                                public record TestRecord<T> : IValueObject<int>
                                {
                                    public int Key { get; }
                                    public int GetKey()
                                    {
                                        return Key;
                                    }
                                    public bool Equals(IDomainObject<int>? other)
                                    {
                                        return this.GetKey() == other?.GetKey();
                                    }
                                }
                                """;

        await new CSharpAnalyzerTest<ValueObjectRecordTypeEnforcementAnalyzer, XUnitVerifier>
        {
            TestCode = testCode,
            TestState =
            {
                AdditionalReferences = { MetadataReference.CreateFromFile(typeof(IValueObject<>).Assembly.Location) }
            },
            ReferenceAssemblies = referenceAssemblies
        }.RunAsync();
    }

    [Fact]
    public async Task Analyzer_Should_Report_Diagnostic_For_Partial_Class()
    {
        const string testCode = """

                                using SquidLabs.Tentacles.Domain.TestObjects;

                                public partial class TestClass : IValueObject<int>
                                {
                                    public int Key { get; }
                                    public int GetKey()
                                    {
                                        return Key;
                                    }
                                    public bool Equals(IDomainObject<int>? other)
                                    {
                                        return this.GetKey() == other?.GetKey();
                                    }
                                }
                                """;

        var expectedDiagnostic =
            new DiagnosticResult(ValueObjectRecordTypeEnforcementAnalyzer.DiagnosticId, DiagnosticSeverity.Error)
                .WithLocation(4, 22)
                .WithMessage("Type 'TestClass' is implementing 'IValueObject<TKey>' and must be declared as a record");

        await new CSharpAnalyzerTest<ValueObjectRecordTypeEnforcementAnalyzer, XUnitVerifier>
        {
            TestCode = testCode,
            ExpectedDiagnostics = { expectedDiagnostic },
            TestState =
            {
                AdditionalReferences = { MetadataReference.CreateFromFile(typeof(IValueObject<>).Assembly.Location) }
            },
            ReferenceAssemblies = referenceAssemblies
        }.RunAsync();
    }
}