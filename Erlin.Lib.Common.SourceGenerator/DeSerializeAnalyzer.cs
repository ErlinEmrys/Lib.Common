using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Erlin.Lib.Common.SourceGenerator;

/// <summary>
///    Analyzer for enforcing DeSerialization rules
/// </summary>
[ DiagnosticAnalyzer( LanguageNames.CSharp ) ]
public class DeSerializeAnalyzer : DiagnosticAnalyzer
{
	/// <summary>
	///    List of errors this analyzer can rise
	/// </summary>
	public override ImmutableArray< DiagnosticDescriptor > SupportedDiagnostics { get; } =
	[
		DeSerializeDiagnosticsDescriptors.SourceGeneratorError,
		DeSerializeDiagnosticsDescriptors.MustBePartial,
		DeSerializeDiagnosticsDescriptors.MustHaveAttribute,
		DeSerializeDiagnosticsDescriptors.AttributeMustHaveGuid,
		DeSerializeDiagnosticsDescriptors.MethodInheritance,
		DeSerializeDiagnosticsDescriptors.ParameterlessCtorAccessibility
	];

	/// <summary>
	///    Initialization of the analyzer
	/// </summary>
	public override void Initialize( AnalysisContext context )
	{
		context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.None );
		context.EnableConcurrentExecution();

		context.RegisterSymbolAction( DeSerializeAnalyzer.AnalyzeNamedType, SymbolKind.NamedType );
	}

	/// <summary>
	///    Analysis of named type
	/// </summary>
	private static void AnalyzeNamedType( SymbolAnalysisContext context )
	{
		try
		{
			INamedTypeSymbol type = ( INamedTypeSymbol )context.Symbol;

			if( ( type.TypeKind == TypeKind.Interface ) || !DeSerializeConstructorGenerator.ImplementsIDeSerializable( type )
			)
			{
				return;
			}

			DeSerializeAnalyzer.AnalyzeDeSerializableType( context, type );
		}
		catch( Exception ex )
		{
			context.ReportDiagnostic( Diagnostic.Create( DeSerializeDiagnosticsDescriptors.SourceGeneratorError, null, ex.ToString() ) );
		}
	}

	/// <summary>
	///    Analysis of DeSerializable type
	/// </summary>
	private static void AnalyzeDeSerializableType( SymbolAnalysisContext context, INamedTypeSymbol type )
	{
		bool isPartial = false;
		Location? typeDeclarationLocation = null;
		foreach( SyntaxReference declaringSyntaxReference in type.DeclaringSyntaxReferences )
		{
			if( declaringSyntaxReference.GetSyntax() is not TypeDeclarationSyntax declaration )
			{
				continue;
			}

			isPartial = DeSerializeConstructorGenerator.IsPartial( declaration );

			typeDeclarationLocation = declaration.Identifier.GetLocation();
			break;
		}

		DeSerializeAnalyzer.CheckPartial( context, type, typeDeclarationLocation, isPartial );
		DeSerializeAnalyzer.CheckAttribute( context, type, typeDeclarationLocation );
		DeSerializeAnalyzer.CheckMethod( context, type, typeDeclarationLocation );
		DeSerializeAnalyzer.CheckCtorAccess( context, type, typeDeclarationLocation );
	}

	/// <summary>
	///    Check if type is declared as partial
	/// </summary>
	private static void CheckPartial( SymbolAnalysisContext context, ISymbol type, Location? typeDeclarationLocation, bool isPartial )
	{
		if( !isPartial )
		{
			context.ReportDiagnostic( Diagnostic.Create( DeSerializeDiagnosticsDescriptors.MustBePartial, typeDeclarationLocation, type.Name ) );
		}
	}

	/// <summary>
	///    Check if type have correct DeSerializable attribute
	/// </summary>
	private static void CheckAttribute( SymbolAnalysisContext context, ISymbol type, Location? typeDeclarationLocation )
	{
		AttributeData? deSerializeAtt = type.GetAttributes().FirstOrDefault( a => DeSerializeConstructorGenerator.IsRuntimeType( a, Const.DE_SERIALIZABLE_ATT_NS, Const.DE_SERIALIZABLE_ATT_NAME ) );

		if( deSerializeAtt == null )
		{
			context.ReportDiagnostic( Diagnostic.Create( DeSerializeDiagnosticsDescriptors.MustHaveAttribute, typeDeclarationLocation, type.Name ) );
			return;
		}

		string? attGuidValue = deSerializeAtt.ConstructorArguments.FirstOrDefault( a => DeSerializeConstructorGenerator.IsRuntimeType( a.Type, Const.STRING_NS, Const.STRING_NAME ) ).Value?.ToString();

		if( string.IsNullOrEmpty( attGuidValue ) || !Guid.TryParse( attGuidValue, out Guid _ ) )
		{
			context.ReportDiagnostic( Diagnostic.Create( DeSerializeDiagnosticsDescriptors.AttributeMustHaveGuid, typeDeclarationLocation, attGuidValue ) );
		}
	}

	/// <summary>
	///    Check DeSerializable method inheritance
	/// </summary>
	private static void CheckMethod( SymbolAnalysisContext context, INamespaceOrTypeSymbol type, Location? typeDeclarationLocation )
	{
		if( type.GetMembers()
				.FirstOrDefault( m => ( m.Kind == SymbolKind.Method )
						&& string.Equals( m.Name, Const.DE_SERIALIZABLE_METHOD_NAME, StringComparison.Ordinal )
						&& m is IMethodSymbol { MethodKind: MethodKind.Ordinary, Parameters.Length: 1 } methodSymbol
						&& methodSymbol.Parameters.SingleOrDefault( p => DeSerializeConstructorGenerator.IsRuntimeType( p.Type, Const.I_DE_SERIALIZER_NS, Const.I_DE_SERIALIZER_NAME ) ) is not null )
			is IMethodSymbol deSerializeMethod
		)
		{
			if( !type.IsSealed
				&& !( deSerializeMethod.IsOverride || deSerializeMethod.IsVirtual || deSerializeMethod.IsAbstract ) )
			{
				context.ReportDiagnostic( Diagnostic.Create( DeSerializeDiagnosticsDescriptors.MethodInheritance, typeDeclarationLocation, type.Name ) );
			}
		}
	}

	/// <summary>
	///    Check type constructor accessibility
	/// </summary>
	private static void CheckCtorAccess( SymbolAnalysisContext context, INamedTypeSymbol type, Location? typeDeclarationLocation )
	{
		if( !type.IsSealed )
		{
			IMethodSymbol? paramLessCtor = type.InstanceConstructors.FirstOrDefault( c => c.Parameters.Length == 0 );

			if( ( paramLessCtor != null )
				&& !paramLessCtor.GetAttributes().Any( a => DeSerializeConstructorGenerator.IsRuntimeType( a, Const.GENERATED_CODE_ATT_NS, Const.GENERATED_CODE_ATT_NAME ) )
				&& ( paramLessCtor.DeclaredAccessibility < type.DeclaredAccessibility ) )
			{
				context.ReportDiagnostic( Diagnostic.Create( DeSerializeDiagnosticsDescriptors.ParameterlessCtorAccessibility, typeDeclarationLocation, type.Name, paramLessCtor.DeclaredAccessibility, type.DeclaredAccessibility ) );
			}
		}
	}
}
