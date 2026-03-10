using System.Collections.Immutable;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Erlin.Lib.Common.SourceGenerator;

/// <summary>
///    Analyzer for enforcing DeSerialization rules
/// </summary>
[ DiagnosticAnalyzer( LanguageNames.CSharp ) ]
public class Analyzer : DiagnosticAnalyzer
{
	private HashSet< Guid > DeSerializeAttIds { get; } = [ ];

	/// <summary>
	///    List of errors this analyzer can rise
	/// </summary>
	public override ImmutableArray< DiagnosticDescriptor > SupportedDiagnostics { get; } =
	[
		DiagnosticsDescriptors.AnalyzerError,
		DiagnosticsDescriptors.MustBePartial,
		DiagnosticsDescriptors.MustHaveAttribute,
		DiagnosticsDescriptors.AttributeMustHaveGuid,
		DiagnosticsDescriptors.MethodInheritance,
		DiagnosticsDescriptors.ParameterlessCtorAccessibility,
		DiagnosticsDescriptors.IdentifierMustBeUnique
	];

	/// <summary>
	///    Initialization of the analyzer
	/// </summary>
	public override void Initialize( AnalysisContext context )
	{
		context.ConfigureGeneratedCodeAnalysis( GeneratedCodeAnalysisFlags.None );
		context.EnableConcurrentExecution();

		context.RegisterSymbolAction( AnalyzeNamedType, SymbolKind.NamedType );
	}

	/// <summary>
	///    Analysis of named type
	/// </summary>
	private void AnalyzeNamedType( SymbolAnalysisContext context )
	{
		try
		{
			INamedTypeSymbol type = ( INamedTypeSymbol )context.Symbol;

			if( ( type.TypeKind == TypeKind.Interface ) || !Generator.ImplementsIDeSerializable( type )
			)
			{
				return;
			}

			AnalyzeDeSerializableType( context, type );
		}
		catch( Exception ex )
		{
			context.ReportDiagnostic( Diagnostic.Create( DiagnosticsDescriptors.AnalyzerError, null, ex.ToString() ) );
		}
	}

	/// <summary>
	///    Analysis of DeSerializable type
	/// </summary>
	private void AnalyzeDeSerializableType( SymbolAnalysisContext context, INamedTypeSymbol type )
	{
		bool isPartial = false;
		Location? typeDeclarationLocation = null;
		foreach( SyntaxReference declaringSyntaxReference in type.DeclaringSyntaxReferences )
		{
			if( declaringSyntaxReference.GetSyntax() is not TypeDeclarationSyntax declaration )
			{
				continue;
			}

			isPartial = Generator.IsPartial( declaration );

			typeDeclarationLocation = declaration.Identifier.GetLocation();
			break;
		}

		Analyzer.CheckPartial( context, type, typeDeclarationLocation, isPartial );
		CheckAttribute( context, type, typeDeclarationLocation );
		Analyzer.CheckMethod( context, type, typeDeclarationLocation );
		Analyzer.CheckCtorAccess( context, type, typeDeclarationLocation );
	}

	/// <summary>
	///    Check if type is declared as partial
	/// </summary>
	private static void CheckPartial( SymbolAnalysisContext context, ISymbol type, Location? typeDeclarationLocation, bool isPartial )
	{
		if( !isPartial )
		{
			context.ReportDiagnostic( Diagnostic.Create( DiagnosticsDescriptors.MustBePartial, typeDeclarationLocation, type.Name ) );
		}
	}

	/// <summary>
	///    Check if type have correct DeSerializable attribute
	/// </summary>
	private void CheckAttribute( SymbolAnalysisContext context, ISymbol type, Location? typeDeclarationLocation )
	{
		AttributeData? deSerializeAtt = type.GetAttributes().FirstOrDefault( a => Generator.IsRuntimeType( a, Const.DE_SERIALIZABLE_ATT_NS, Const.DE_SERIALIZABLE_ATT_NAME ) );

		if( deSerializeAtt == null )
		{
			context.ReportDiagnostic( Diagnostic.Create( DiagnosticsDescriptors.MustHaveAttribute, typeDeclarationLocation, type.Name ) );
			return;
		}

		string? attGuidValue = deSerializeAtt.ConstructorArguments.FirstOrDefault( a => Generator.IsRuntimeType( a.Type, Const.STRING_NS, Const.STRING_NAME ) ).Value?.ToString();

		if( !Guid.TryParse( attGuidValue, out Guid dsId ) )
		{
			context.ReportDiagnostic( Diagnostic.Create( DiagnosticsDescriptors.AttributeMustHaveGuid, typeDeclarationLocation, attGuidValue ) );
			return;
		}

		if( !DeSerializeAttIds.Add( dsId ) )
		{
			context.ReportDiagnostic( Diagnostic.Create( DiagnosticsDescriptors.IdentifierMustBeUnique, typeDeclarationLocation, attGuidValue ) );
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
						&& methodSymbol.Parameters.SingleOrDefault( p => Generator.IsRuntimeType( p.Type, Const.I_DE_SERIALIZER_NS, Const.I_DE_SERIALIZER_NAME ) ) is not null )
			is IMethodSymbol deSerializeMethod
		)
		{
			if( !type.IsSealed
				&& !( deSerializeMethod.IsOverride || deSerializeMethod.IsVirtual || deSerializeMethod.IsAbstract ) )
			{
				context.ReportDiagnostic( Diagnostic.Create( DiagnosticsDescriptors.MethodInheritance, typeDeclarationLocation, type.Name ) );
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
				&& !paramLessCtor.GetAttributes().Any( a => Generator.IsRuntimeType( a, Const.GENERATED_CODE_ATT_NS, Const.GENERATED_CODE_ATT_NAME ) )
				&& ( paramLessCtor.DeclaredAccessibility < type.DeclaredAccessibility ) )
			{
				context.ReportDiagnostic( Diagnostic.Create( DiagnosticsDescriptors.ParameterlessCtorAccessibility, typeDeclarationLocation, type.Name, paramLessCtor.DeclaredAccessibility, type.DeclaredAccessibility ) );
			}
		}
	}
}
