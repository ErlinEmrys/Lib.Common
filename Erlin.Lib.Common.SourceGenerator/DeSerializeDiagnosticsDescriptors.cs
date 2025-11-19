using Microsoft.CodeAnalysis;

namespace Erlin.Lib.Common.SourceGenerator;

/// <summary>
///    Compilation time error messages
/// </summary>
public class DeSerializeDiagnosticsDescriptors
{
	public static readonly DiagnosticDescriptor SourceGeneratorError
		= new( "DeSerialize_000",
			"Source generator error",
			"Erlin.Lib.Common.SourceGenerator error: '{0}'",
			"DeSerializeAnalyser",
			DiagnosticSeverity.Error,
			true );

	public static readonly DiagnosticDescriptor MustBePartial
		= new( "DeSerialize_001",
			"DeSerializable type must be partial",
			"The type '{0}' implementing IDeSerializable must be marked as partial",
			"DeSerializeAnalyser",
			DiagnosticSeverity.Error,
			true );

	public static readonly DiagnosticDescriptor MustHaveAttribute
		= new( "DeSerialize_002",
			"DeSerializable attribute missing",
			"The type '{0}' implementing IDeSerializable must have DeSerializable attribute",
			"DeSerializeAnalyser",
			DiagnosticSeverity.Error,
			true );

	public static readonly DiagnosticDescriptor AttributeMustHaveGuid
		= new( "DeSerialize_003",
			"DeSerializable attribute invalid unique identifier",
			"DeSerializable attribute identifier value '{0}' is not valid unique identifier (Guid)",
			"DeSerializeAnalyser",
			DiagnosticSeverity.Error,
			true );

	public static readonly DiagnosticDescriptor MethodInheritance
		= new( "DeSerialize_004",
			"DeSerializable method must be overridable",
			"DeSerializable method on type '{0}' must be overridable or type must be declared as sealed",
			"DeSerializeAnalyser",
			DiagnosticSeverity.Error,
			true );

	public static readonly DiagnosticDescriptor ParameterlessCtorAccessibility
		= new( "DeSerialize_005",
			"Parameterless constructor accessibility too low",
			"Parameterless constructor accessibility '{1}' on type '{0}' must be same or higher than the type itself: '{2}'",
			"DeSerializeAnalyser",
			DiagnosticSeverity.Error,
			true );
}
