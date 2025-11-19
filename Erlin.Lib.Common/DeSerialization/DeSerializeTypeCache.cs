using System.Reflection;

using Erlin.Lib.Common.Collections;

namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    Cache for all types that can be De/Serialized
/// </summary>
public static class DeSerializeTypeCache
{
	private static DictionaryMap< Guid, Type > Map { get; } = new();

	private static Dictionary< Type, ushort > Versions { get; } = new();

	/// <summary>
	///    Collection of all registered DeSerializable types
	/// </summary>
	public static IEnumerable< Type > RegisteredTypes
	{
		get { return DeSerializeTypeCache.Versions.Keys; }
	}

	/// <summary>
	///    Initialize cache with selected assemblies
	/// </summary>
	/// <param name="assemblies"></param>
	/// <returns></returns>
	public static void Initialize( params Assembly[] assemblies )
	{
		foreach( Assembly fAssembly in assemblies.Distinct() )
		{
			DeSerializeTypeCache.ExtractDeSerializableTypes( fAssembly );
		}
	}

	public static void InitializeType( Type runtimeType, ushort version, Guid identifier )
	{
		if( DeSerializeTypeCache.Map.ToValue.TryGetValue( identifier, out Type? existingValue ) )
		{
			throw new DeSerializeException( $"Two DeSerializable types shares same DeSerializableAttribute Identifier value: {runtimeType.FullName} VS {existingValue.FullName}{Environment.NewLine}Value: {identifier}" );
		}

		DeSerializeTypeCache.Map.Add( identifier, runtimeType );
		DeSerializeTypeCache.Versions.Add( runtimeType, version );
	}

	/// <summary>
	///    Retrieve all De/Serializable types from assembly
	/// </summary>
	private static void ExtractDeSerializableTypes( Assembly assembly )
	{
		Type[] allTypes = assembly.GetTypes();
		foreach( Type fType in allTypes )
		{
			if( !fType.IsClass && !fType.IsValueType )
			{
				continue;
			}

			if( DeSerializeTypeCache.CheckTypeIsDeSerializable( fType ) )
			{
				DeSerializableAttribute? att = fType.GetOneCustomAttributeN< DeSerializableAttribute >();
				if( att is null )
				{
					throw new DeSerializeException( $"Missing DeSerializableAttribute on type '{fType.FullName}'" );
				}

				DeSerializeTypeCache.InitializeType( fType, att.Version, att.Identifier );
			}
		}
	}

	/// <summary>
	///    Check if runtime type implements IDeSerializable interface
	/// </summary>
	public static bool CheckTypeIsDeSerializable( Type type )
	{
		return DeSerializeTypeCache.Versions.ContainsKey( type ) || type.GetInterfaces().Any( i => i == TypeHelper.TypeIDeSerializable );
	}

	/// <summary>
	///    Returns De/Serializable runtime type by Guid identifier
	/// </summary>
	public static Type GetRuntimeType( Guid identifier, string oldTypeName )
	{
		if( !DeSerializeTypeCache.Map.ToValue.TryGetValue( identifier, out Type? type ) )
		{
			throw new DeSerializeException( $"RuntimeType not found in DeSerializable type cache by Guid: {identifier}{Environment.NewLine}Old type name was: {oldTypeName}" );
		}

		return type;
	}

	public static ushort GetVersion( Type runtimeType )
	{
		if( !DeSerializeTypeCache.Versions.TryGetValue( runtimeType, out ushort version ) )
		{
			throw new DeSerializeException( $"Version of RuntimeType not found in DeSerializable type cache: {runtimeType.FullName}" );
		}

		return version;
	}

	public static Guid GetIdentifier( Type runtimeType )
	{
		if( !DeSerializeTypeCache.Map.ToKey.TryGetValue( runtimeType, out Guid identifier ) )
		{
			throw new DeSerializeException( $"Identifier not found in DeSerializable type cache by RuntimeType: {runtimeType.FullName}" );
		}

		return identifier;
	}
}
