using Microsoft.CodeAnalysis;

namespace Erlin.Lib.Common.SourceGenerator;

/// <summary>
///    Compilation time error messages
/// </summary>
public static class DiagnosticsDescriptors
{
	public const string CATEGORY = "DeSerializeAnalyzer";

	public static readonly DiagnosticDescriptor AnalyzerError
		= new( "DeSerialize_000",
			"Analyzer error",
			"Erlin.Lib.Common.SourceGenerator.DeSerializeAnalyzer error: '{0}'",
			CATEGORY, DiagnosticSeverity.Error, true );

	public static readonly DiagnosticDescriptor MustBePartial
		= new( "DeSerialize_001",
			"DeSerializable type must be partial",
			"The type '{0}' implementing IDeSerializable must be marked as partial",
			CATEGORY, DiagnosticSeverity.Error, true );

	public static readonly DiagnosticDescriptor MustHaveAttribute
		= new( "DeSerialize_002",
			"DeSerializable attribute missing",
			"The type '{0}' implementing IDeSerializable must have DeSerializable attribute",
			CATEGORY, DiagnosticSeverity.Error, true );

	public static readonly DiagnosticDescriptor AttributeMustHaveGuid
		= new( "DeSerialize_003",
			"DeSerializable attribute invalid unique identifier",
			"DeSerializable attribute identifier value '{0}' is not valid unique identifier (Guid)",
			CATEGORY, DiagnosticSeverity.Error, true );

	public static readonly DiagnosticDescriptor MethodInheritance
		= new( "DeSerialize_004",
			"DeSerializable method must be overridable",
			"DeSerializable method on type '{0}' must be overridable or type must be declared as sealed",
			CATEGORY, DiagnosticSeverity.Error, true );

	public static readonly DiagnosticDescriptor ParameterlessCtorAccessibility
		= new( "DeSerialize_005",
			"Parameterless constructor accessibility too low",
			"Parameterless constructor accessibility '{1}' on type '{0}' must be same or higher than the type itself: '{2}'",
			CATEGORY, DiagnosticSeverity.Error, true );

	public static readonly DiagnosticDescriptor IdentifierMustBeUnique
		= new( "DeSerialize_006",
			"DeSerializable attribute identifier is not unique",
			"DeSerializable attribute identifier value '{0}' is not unique it exists both on '{1}' and '{2}'",
			CATEGORY, DiagnosticSeverity.Error, true );
}
