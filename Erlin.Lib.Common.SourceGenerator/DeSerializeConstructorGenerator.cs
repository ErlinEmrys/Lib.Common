using Microsoft.CodeAnalysis;

using System.Text;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Erlin.Lib.Common.SourceGenerator;

/// <summary>
///    Helper generator for DeSerialize constructors
/// </summary>
[Generator]
public class DeSerializeConstructorGenerator : IIncrementalGenerator
{
	/// <summary>
	///    Initialization of source generation
	/// </summary>
	/// <param name="context"></param>
	public void Initialize( IncrementalGeneratorInitializationContext context )
	{
		IncrementalValuesProvider<DeSerializableInfo?> deSerializableTypes =
			context
			.SyntaxProvider
			.CreateSyntaxProvider(
					DeSerializeConstructorGenerator.CheckIsTypeDeclaration,
					DeSerializeConstructorGenerator.GetDeSerializableSymbol )
			.Where( type => type is not null )
			.Collect()
			.SelectMany( ( types, _ ) => types.Distinct() );

		context.RegisterSourceOutput( deSerializableTypes, DeSerializeConstructorGenerator.GenerateSource );
	}

	/// <summary>
	///    Filter all partial type declarations
	/// </summary>
	/// <param name="syntaxNode"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	private static bool CheckIsTypeDeclaration(
		SyntaxNode syntaxNode,
		CancellationToken cancellationToken )
	{
		if( syntaxNode is not TypeDeclarationSyntax declaration
			|| syntaxNode is InterfaceDeclarationSyntax )
		{
			return false;
		}

		return DeSerializeConstructorGenerator.IsPartial( declaration );
	}

	/// <summary>
	///    Check if type declaration have partial modifier
	/// </summary>
	/// <param name="declaration"></param>
	/// <returns></returns>
	public static bool IsPartial( TypeDeclarationSyntax declaration )
	{
		return declaration.Modifiers.Any( m => m.IsKind( SyntaxKind.PartialKeyword ) );
	}

	/// <summary>
	///    Check if the namespace and name are the same on the symbol
	/// </summary>
	/// <param name="symbol">Runtime symbol</param>
	/// <param name="ns">Namespace</param>
	/// <param name="name">Name</param>
	/// <returns>True - name and namespace equals the one on symbol</returns>
	public static bool IsRuntimeType( ISymbol? symbol, string ns, string name )
	{
		return ( symbol != null )
			&& string.Equals( symbol.ContainingNamespace.ToString(), ns, StringComparison.Ordinal )
			&& string.Equals( symbol.Name, name, StringComparison.Ordinal );
	}

	/// <summary>
	///    Check if the namespace and name are the same as the attribute
	/// </summary>
	/// <param name="att">Attribute symbol</param>
	/// <param name="ns">Namespace</param>
	/// <param name="name">Name</param>
	/// <returns>True - name and namespace equals the one on symbol</returns>
	public static bool IsRuntimeType( AttributeData? att, string ns, string name )
	{
		return ( att != null ) && DeSerializeConstructorGenerator.IsRuntimeType( att.AttributeClass, ns, name );
	}

	/// <summary>
	///    Returns type symbol for type declaration
	/// </summary>
	/// <param name="context"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	private static DeSerializableInfo? GetDeSerializableSymbol(
		GeneratorSyntaxContext context,
		CancellationToken cancellationToken )
	{
		if( context.SemanticModel.GetDeclaredSymbol( context.Node ) is not ITypeSymbol symbol
			|| !DeSerializeConstructorGenerator.ImplementsIDeSerializable( symbol ) )
		{
			return null;
		}

		bool implemetDeSerializeCtor =
			!DeSerializeConstructorGenerator.ImplementsDeSerializableConstructor( symbol );

		bool baseHaveParamlessCtor = ( symbol.BaseType == null )
			|| DeSerializeConstructorGenerator.ImplementsConstructor( symbol.BaseType, true );

		bool implemetParamlessCtor = baseHaveParamlessCtor
			&& !DeSerializeConstructorGenerator.ImplementsConstructor( symbol );

		if( !implemetDeSerializeCtor && !implemetParamlessCtor )
		{
			return null;
		}

		bool isBottomImplementation = symbol.BaseType is null
			|| !DeSerializeConstructorGenerator.ImplementsIDeSerializable( symbol.BaseType );

		DeSerializableInfo info = new(
			symbol.Name, symbol.ContainingNamespace.ToString(), isBottomImplementation,
			symbol.IsAbstract, implemetDeSerializeCtor, implemetParamlessCtor );

		return info;
	}

	/// <summary>
	///    Check if type symbol implements DeSerializable constructor
	/// </summary>
	public static bool ImplementsDeSerializableConstructor( ITypeSymbol symbol )
	{
		return symbol.GetMembers()
						.Any(
								m =>
								{
									return ( m.Kind == SymbolKind.Method )
										&& m is IMethodSymbol
										{
											MethodKind: MethodKind.Constructor, Parameters.Length: 1
										} methodSymbol
										&& methodSymbol.Parameters.SingleOrDefault(
												p =>
													DeSerializeConstructorGenerator.IsRuntimeType(
														p.Type, Const.I_DE_SERIALIZER_NS, Const.I_DE_SERIALIZER_NAME ) )
											is not null;
								} );
	}

	/// <summary>
	///    Check if type symbol implements parameterless constructor
	/// </summary>
	public static bool ImplementsConstructor( ITypeSymbol symbol, bool onlyParamless = false )
	{
		return symbol.GetMembers()
						.Any(
								m => ( m.Kind == SymbolKind.Method )
									&& m is IMethodSymbol
									{
										MethodKind: MethodKind.Constructor
									} ctor
									&& ( onlyParamless || ( m.DeclaringSyntaxReferences.Length > 0 ) )
									&& ( !onlyParamless || ( ctor.Parameters.Length <= 0 ) )
									&& !ctor.GetAttributes()
											.Any(
													a =>
														DeSerializeConstructorGenerator.IsRuntimeType(
															a, Const.GENERATED_CODE_ATT_NS,
															Const.GENERATED_CODE_ATT_NAME ) ) );
	}

	/// <summary>
	///    Check if type symbol implements IDeSerializable interface
	/// </summary>
	/// <param name="symbol"></param>
	/// <returns></returns>
	public static bool ImplementsIDeSerializable( ITypeSymbol symbol )
	{
		return symbol.AllInterfaces.Any(
			i => DeSerializeConstructorGenerator.IsRuntimeType(
				i, Const.I_DE_SERIALIZABLE_NS, Const.I_DE_SERIALIZABLE_NAME ) );
	}

	/// <summary>
	///    Generate source files for selected type declarations
	/// </summary>
	/// <param name="context"></param>
	/// <param name="deSerializable"></param>
	private static void GenerateSource(
		SourceProductionContext context,
		DeSerializableInfo? deSerializable )
	{
		if( deSerializable is null )
		{
			return;
		}

		string code = DeSerializeConstructorGenerator.GenerateCode( deSerializable );

		context.AddSource( $"{deSerializable.NameSpace}.{deSerializable.Name}.g.cs", code );
	}

	/// <summary>
	///    Generates code for selected source
	/// </summary>
	private static string GenerateCode( DeSerializableInfo type )
	{
		StringBuilder code = new();
		code.Append(
			$$"""
				// Generated: {{DateTime.Now:yyyy.MM.dd HH:mm:ss:ffff}}
				// <auto-generated />

				using {{Const.GENERATED_CODE_ATT_NS}};
				using System.Diagnostics.CodeAnalysis;
				using {{Const.I_DE_SERIALIZER_NS}};

				namespace {{type.NameSpace}};

				partial class {{type.Name}}
				{

				""" );

		if( type.ImplementDeSerializeCtor )
		{
			code.Append(
				$$"""
						/// <summary>
						///  DeSerializable constructor
						/// </summary>
						/// <param name="ds">Current deserialization provider</param>
					#pragma warning disable CS8618
						[SuppressMessage( "ReSharper", "VirtualMemberCallInConstructor" )]
						[{{Const.GENERATED_CODE_ATT_NAME}}("Erlin.Lib.Common.SourceGenerator", "1.0.0")]
						{{( type.IsAbstract ? "protected" : "public" )}} {{type.Name}}( {{Const.I_DE_SERIALIZER_NAME}} ds ){{(
								type.IsBottomImplementation ? string.Empty : $" : base( ({Const.I_DE_SERIALIZER_NAME})null )" )}}
						{
							if( ds != null )
							{
								DeSerialize( ds );
							}
						}
					#pragma warning restore CS8618


					""" );
		}

		if( type.ImplementParamlessCtor )
		{
			code.Append(
				$$"""
						/// <summary>
						///  DeSerializable constructor
						/// </summary>
						[{{Const.GENERATED_CODE_ATT_NAME}}("Erlin.Lib.Common.SourceGenerator", "1.0.0")]
						public {{type.Name}}(){{( type.IsBottomImplementation ? string.Empty : " : base()" )}}
						{
						}

					""" );
		}

		code.AppendLine( "}" );
		return code.ToString();
	}

	public static void Log( string message )
	{
		File.AppendAllText(
			@"E:\Temp\Dbg.txt", $"[{DateTime.Now:yyyy.MM.dd HH:mm:ss:ffff}] {message}{Environment.NewLine}",
			Encoding.UTF8 );
	}
}
