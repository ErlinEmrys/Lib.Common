using System.Runtime.CompilerServices;

using Erlin.Lib.Common.DeSerialization.ReadWrite;

namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    De/Serializer of both complex objects and primitive values
/// </summary>
public interface IDeSerializer : IDisposable
{
	/// <summary>
	///    Reader or Writer of elementary values
	/// </summary>
	IDeSerializeReader Reader { get; }

	/// <summary>
	///    Reader or Writer of elementary values
	/// </summary>
	IDeSerializeWriter Writer { get; }

	/// <summary>
	///    Indicates that DeSerializer is reading
	/// </summary>
	bool IsRead { get; }

	/// <summary>
	///    Indicates that DeSerializer is writing
	/// </summary>
	bool IsWrite { get; }

	/// <summary>
	///    Provider of runtime types
	/// </summary>
	IDeSerializeTypeProvider TypeProvider { get; }

	/// <summary>
	///    Optional parameters of De/Serialization
	/// </summary>
	Dictionary< string, string > Params { get; }

	bool ReadWriteBool( DeSerializeContext< bool > context );

	bool ReadWriteBool( bool value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	bool? ReadWriteBoolN( DeSerializeContext< bool? > context );

	bool? ReadWriteBoolN( bool? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	sbyte ReadWriteSByte( DeSerializeContext< sbyte > context );

	sbyte ReadWriteSByte( sbyte value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	sbyte? ReadWriteSByteN( DeSerializeContext< sbyte? > context );

	sbyte? ReadWriteSByteN( sbyte? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	sbyte[] ReadWriteSByteArr( DeSerializeContext< sbyte[] > context );

	sbyte[] ReadWriteSByteArr( sbyte[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	sbyte[]? ReadWriteSByteArrN( DeSerializeContext< sbyte[]? > context );

	sbyte[]? ReadWriteSByteArrN( sbyte[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	sbyte?[] ReadWriteSByteNArr( DeSerializeContext< sbyte?[] > context );

	sbyte?[] ReadWriteSByteNArr( sbyte?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	sbyte?[]? ReadWriteSByteNArrN( DeSerializeContext< sbyte?[]? > context );

	sbyte?[]? ReadWriteSByteNArrN( sbyte?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< sbyte > ReadWriteSByteHashSet( DeSerializeContext< HashSet< sbyte > > context );

	HashSet< sbyte > ReadWriteSByteHashSet( HashSet< sbyte > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< sbyte >? ReadWriteSByteHashSetN( DeSerializeContext< HashSet< sbyte >? > context );

	HashSet< sbyte >? ReadWriteSByteHashSetN( HashSet< sbyte >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< sbyte? > ReadWriteSByteNHashSet( DeSerializeContext< HashSet< sbyte? > > context );

	HashSet< sbyte? > ReadWriteSByteNHashSet( HashSet< sbyte? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< sbyte? >? ReadWriteSByteNHashSetN( DeSerializeContext< HashSet< sbyte? >? > context );

	HashSet< sbyte? >? ReadWriteSByteNHashSetN( HashSet< sbyte? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< sbyte > ReadWriteSByteList( DeSerializeContext< List< sbyte > > context );

	List< sbyte > ReadWriteSByteList( List< sbyte > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< sbyte >? ReadWriteSByteListN( DeSerializeContext< List< sbyte >? > context );

	List< sbyte >? ReadWriteSByteListN( List< sbyte >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< sbyte? > ReadWriteSByteNList( DeSerializeContext< List< sbyte? > > context );

	List< sbyte? > ReadWriteSByteNList( List< sbyte? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< sbyte? >? ReadWriteSByteNListN( DeSerializeContext< List< sbyte? >? > context );

	List< sbyte? >? ReadWriteSByteNListN( List< sbyte? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	byte ReadWriteByte( DeSerializeContext< byte > context );

	byte ReadWriteByte( byte value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	byte? ReadWriteByteN( DeSerializeContext< byte? > context );

	byte? ReadWriteByteN( byte? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	byte[] ReadWriteByteArr( DeSerializeContext< byte[] > context );

	byte[] ReadWriteByteArr( byte[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	byte[]? ReadWriteByteArrN( DeSerializeContext< byte[]? > context );

	byte[]? ReadWriteByteArrN( byte[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	byte?[] ReadWriteByteNArr( DeSerializeContext< byte?[] > context );

	byte?[] ReadWriteByteNArr( byte?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	byte?[]? ReadWriteByteNArrN( DeSerializeContext< byte?[]? > context );

	byte?[]? ReadWriteByteNArrN( byte?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< byte > ReadWriteByteHashSet( DeSerializeContext< HashSet< byte > > context );

	HashSet< byte > ReadWriteByteHashSet( HashSet< byte > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< byte >? ReadWriteByteHashSetN( DeSerializeContext< HashSet< byte >? > context );

	HashSet< byte >? ReadWriteByteHashSetN( HashSet< byte >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< byte? > ReadWriteByteNHashSet( DeSerializeContext< HashSet< byte? > > context );

	HashSet< byte? > ReadWriteByteNHashSet( HashSet< byte? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< byte? >? ReadWriteByteNHashSetN( DeSerializeContext< HashSet< byte? >? > context );

	HashSet< byte? >? ReadWriteByteNHashSetN( HashSet< byte? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< byte > ReadWriteByteList( DeSerializeContext< List< byte > > context );

	List< byte > ReadWriteByteList( List< byte > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< byte >? ReadWriteByteListN( DeSerializeContext< List< byte >? > context );

	List< byte >? ReadWriteByteListN( List< byte >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< byte? > ReadWriteByteNList( DeSerializeContext< List< byte? > > context );

	List< byte? > ReadWriteByteNList( List< byte? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< byte? >? ReadWriteByteNListN( DeSerializeContext< List< byte? >? > context );

	List< byte? >? ReadWriteByteNListN( List< byte? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	short ReadWriteInt16( DeSerializeContext< short > context );

	short ReadWriteInt16( short value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	short? ReadWriteInt16N( DeSerializeContext< short? > context );

	short? ReadWriteInt16N( short? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	short[] ReadWriteInt16Arr( DeSerializeContext< short[] > context );

	short[] ReadWriteInt16Arr( short[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	short[]? ReadWriteInt16ArrN( DeSerializeContext< short[]? > context );

	short[]? ReadWriteInt16ArrN( short[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	short?[] ReadWriteInt16NArr( DeSerializeContext< short?[] > context );

	short?[] ReadWriteInt16NArr( short?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	short?[]? ReadWriteInt16NArrN( DeSerializeContext< short?[]? > context );

	short?[]? ReadWriteInt16NArrN( short?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< short > ReadWriteInt16HashSet( DeSerializeContext< HashSet< short > > context );

	HashSet< short > ReadWriteInt16HashSet( HashSet< short > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< short >? ReadWriteInt16HashSetN( DeSerializeContext< HashSet< short >? > context );

	HashSet< short >? ReadWriteInt16HashSetN( HashSet< short >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< short? > ReadWriteInt16NHashSet( DeSerializeContext< HashSet< short? > > context );

	HashSet< short? > ReadWriteInt16NHashSet( HashSet< short? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< short? >? ReadWriteInt16NHashSetN( DeSerializeContext< HashSet< short? >? > context );

	HashSet< short? >? ReadWriteInt16NHashSetN( HashSet< short? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< short > ReadWriteInt16List( DeSerializeContext< List< short > > context );

	List< short > ReadWriteInt16List( List< short > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< short >? ReadWriteInt16ListN( DeSerializeContext< List< short >? > context );

	List< short >? ReadWriteInt16ListN( List< short >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< short? > ReadWriteInt16NList( DeSerializeContext< List< short? > > context );

	List< short? > ReadWriteInt16NList( List< short? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< short? >? ReadWriteInt16NListN( DeSerializeContext< List< short? >? > context );

	List< short? >? ReadWriteInt16NListN( List< short? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ushort ReadWriteUInt16( DeSerializeContext< ushort > context );

	ushort ReadWriteUInt16( ushort value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ushort? ReadWriteUInt16N( DeSerializeContext< ushort? > context );

	ushort? ReadWriteUInt16N( ushort? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ushort[] ReadWriteUInt16Arr( DeSerializeContext< ushort[] > context );

	ushort[] ReadWriteUInt16Arr( ushort[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ushort[]? ReadWriteUInt16ArrN( DeSerializeContext< ushort[]? > context );

	ushort[]? ReadWriteUInt16ArrN( ushort[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ushort?[] ReadWriteUInt16NArr( DeSerializeContext< ushort?[] > context );

	ushort?[] ReadWriteUInt16NArr( ushort?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ushort?[]? ReadWriteUInt16NArrN( DeSerializeContext< ushort?[]? > context );

	ushort?[]? ReadWriteUInt16NArrN( ushort?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< ushort > ReadWriteUInt16HashSet( DeSerializeContext< HashSet< ushort > > context );

	HashSet< ushort > ReadWriteUInt16HashSet( HashSet< ushort > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< ushort >? ReadWriteUInt16HashSetN( DeSerializeContext< HashSet< ushort >? > context );

	HashSet< ushort >? ReadWriteUInt16HashSetN( HashSet< ushort >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< ushort? > ReadWriteUInt16NHashSet( DeSerializeContext< HashSet< ushort? > > context );

	HashSet< ushort? > ReadWriteUInt16NHashSet( HashSet< ushort? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< ushort? >? ReadWriteUInt16NHashSetN( DeSerializeContext< HashSet< ushort? >? > context );

	HashSet< ushort? >? ReadWriteUInt16NHashSetN( HashSet< ushort? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< ushort > ReadWriteUInt16List( DeSerializeContext< List< ushort > > context );

	List< ushort > ReadWriteUInt16List( List< ushort > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< ushort >? ReadWriteUInt16ListN( DeSerializeContext< List< ushort >? > context );

	List< ushort >? ReadWriteUInt16ListN( List< ushort >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< ushort? > ReadWriteUInt16NList( DeSerializeContext< List< ushort? > > context );

	List< ushort? > ReadWriteUInt16NList( List< ushort? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< ushort? >? ReadWriteUInt16NListN( DeSerializeContext< List< ushort? >? > context );

	List< ushort? >? ReadWriteUInt16NListN( List< ushort? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	int ReadWriteInt32( DeSerializeContext< int > context );

	int ReadWriteInt32( int value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	int? ReadWriteInt32N( DeSerializeContext< int? > context );

	int? ReadWriteInt32N( int? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	int[] ReadWriteInt32Arr( DeSerializeContext< int[] > context );

	int[] ReadWriteInt32Arr( int[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	int[]? ReadWriteInt32ArrN( DeSerializeContext< int[]? > context );

	int[]? ReadWriteInt32ArrN( int[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	int?[] ReadWriteInt32NArr( DeSerializeContext< int?[] > context );

	int?[] ReadWriteInt32NArr( int?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	int?[]? ReadWriteInt32NArrN( DeSerializeContext< int?[]? > context );

	int?[]? ReadWriteInt32NArrN( int?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< int > ReadWriteInt32HashSet( DeSerializeContext< HashSet< int > > context );

	HashSet< int > ReadWriteInt32HashSet( HashSet< int > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< int >? ReadWriteInt32HashSetN( DeSerializeContext< HashSet< int >? > context );

	HashSet< int >? ReadWriteInt32HashSetN( HashSet< int >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< int? > ReadWriteInt32NHashSet( DeSerializeContext< HashSet< int? > > context );

	HashSet< int? > ReadWriteInt32NHashSet( HashSet< int? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< int? >? ReadWriteInt32NHashSetN( DeSerializeContext< HashSet< int? >? > context );

	HashSet< int? >? ReadWriteInt32NHashSetN( HashSet< int? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< int > ReadWriteInt32List( DeSerializeContext< List< int > > context );

	List< int > ReadWriteInt32List( List< int > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< int >? ReadWriteInt32ListN( DeSerializeContext< List< int >? > context );

	List< int >? ReadWriteInt32ListN( List< int >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< int? > ReadWriteInt32NList( DeSerializeContext< List< int? > > context );

	List< int? > ReadWriteInt32NList( List< int? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< int? >? ReadWriteInt32NListN( DeSerializeContext< List< int? >? > context );

	List< int? >? ReadWriteInt32NListN( List< int? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	uint ReadWriteUInt32( DeSerializeContext< uint > context );

	uint ReadWriteUInt32( uint value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	uint? ReadWriteUInt32N( DeSerializeContext< uint? > context );

	uint? ReadWriteUInt32N( uint? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	uint[] ReadWriteUInt32Arr( DeSerializeContext< uint[] > context );

	uint[] ReadWriteUInt32Arr( uint[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	uint[]? ReadWriteUInt32ArrN( DeSerializeContext< uint[]? > context );

	uint[]? ReadWriteUInt32ArrN( uint[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	uint?[] ReadWriteUInt32NArr( DeSerializeContext< uint?[] > context );

	uint?[] ReadWriteUInt32NArr( uint?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	uint?[]? ReadWriteUInt32NArrN( DeSerializeContext< uint?[]? > context );

	uint?[]? ReadWriteUInt32NArrN( uint?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< uint > ReadWriteUInt32HashSet( DeSerializeContext< HashSet< uint > > context );

	HashSet< uint > ReadWriteUInt32HashSet( HashSet< uint > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< uint >? ReadWriteUInt32HashSetN( DeSerializeContext< HashSet< uint >? > context );

	HashSet< uint >? ReadWriteUInt32HashSetN( HashSet< uint >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< uint? > ReadWriteUInt32NHashSet( DeSerializeContext< HashSet< uint? > > context );

	HashSet< uint? > ReadWriteUInt32NHashSet( HashSet< uint? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< uint? >? ReadWriteUInt32NHashSetN( DeSerializeContext< HashSet< uint? >? > context );

	HashSet< uint? >? ReadWriteUInt32NHashSetN( HashSet< uint? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< uint > ReadWriteUInt32List( DeSerializeContext< List< uint > > context );

	List< uint > ReadWriteUInt32List( List< uint > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< uint >? ReadWriteUInt32ListN( DeSerializeContext< List< uint >? > context );

	List< uint >? ReadWriteUInt32ListN( List< uint >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< uint? > ReadWriteUInt32NList( DeSerializeContext< List< uint? > > context );

	List< uint? > ReadWriteUInt32NList( List< uint? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< uint? >? ReadWriteUInt32NListN( DeSerializeContext< List< uint? >? > context );

	List< uint? >? ReadWriteUInt32NListN( List< uint? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	long ReadWriteInt64( DeSerializeContext< long > context );

	long ReadWriteInt64( long value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	long? ReadWriteInt64N( DeSerializeContext< long? > context );

	long? ReadWriteInt64N( long? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	long[] ReadWriteInt64Arr( DeSerializeContext< long[] > context );

	long[] ReadWriteInt64Arr( long[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	long[]? ReadWriteInt64ArrN( DeSerializeContext< long[]? > context );

	long[]? ReadWriteInt64ArrN( long[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	long?[] ReadWriteInt64NArr( DeSerializeContext< long?[] > context );

	long?[] ReadWriteInt64NArr( long?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	long?[]? ReadWriteInt64NArrN( DeSerializeContext< long?[]? > context );

	long?[]? ReadWriteInt64NArrN( long?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< long > ReadWriteInt64HashSet( DeSerializeContext< HashSet< long > > context );

	HashSet< long > ReadWriteInt64HashSet( HashSet< long > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< long >? ReadWriteInt64HashSetN( DeSerializeContext< HashSet< long >? > context );

	HashSet< long >? ReadWriteInt64HashSetN( HashSet< long >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< long? > ReadWriteInt64NHashSet( DeSerializeContext< HashSet< long? > > context );

	HashSet< long? > ReadWriteInt64NHashSet( HashSet< long? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< long? >? ReadWriteInt64NHashSetN( DeSerializeContext< HashSet< long? >? > context );

	HashSet< long? >? ReadWriteInt64NHashSetN( HashSet< long? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< long > ReadWriteInt64List( DeSerializeContext< List< long > > context );

	List< long > ReadWriteInt64List( List< long > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< long >? ReadWriteInt64ListN( DeSerializeContext< List< long >? > context );

	List< long >? ReadWriteInt64ListN( List< long >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< long? > ReadWriteInt64NList( DeSerializeContext< List< long? > > context );

	List< long? > ReadWriteInt64NList( List< long? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< long? >? ReadWriteInt64NListN( DeSerializeContext< List< long? >? > context );

	List< long? >? ReadWriteInt64NListN( List< long? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ulong ReadWriteUInt64( DeSerializeContext< ulong > context );

	ulong ReadWriteUInt64( ulong value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ulong? ReadWriteUInt64N( DeSerializeContext< ulong? > context );

	ulong? ReadWriteUInt64N( ulong? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ulong[] ReadWriteUInt64Arr( DeSerializeContext< ulong[] > context );

	ulong[] ReadWriteUInt64Arr( ulong[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ulong[]? ReadWriteUInt64ArrN( DeSerializeContext< ulong[]? > context );

	ulong[]? ReadWriteUInt64ArrN( ulong[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ulong?[] ReadWriteUInt64NArr( DeSerializeContext< ulong?[] > context );

	ulong?[] ReadWriteUInt64NArr( ulong?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	ulong?[]? ReadWriteUInt64NArrN( DeSerializeContext< ulong?[]? > context );

	ulong?[]? ReadWriteUInt64NArrN( ulong?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< ulong > ReadWriteUInt64HashSet( DeSerializeContext< HashSet< ulong > > context );

	HashSet< ulong > ReadWriteUInt64HashSet( HashSet< ulong > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< ulong >? ReadWriteUInt64HashSetN( DeSerializeContext< HashSet< ulong >? > context );

	HashSet< ulong >? ReadWriteUInt64HashSetN( HashSet< ulong >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< ulong? > ReadWriteUInt64NHashSet( DeSerializeContext< HashSet< ulong? > > context );

	HashSet< ulong? > ReadWriteUInt64NHashSet( HashSet< ulong? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< ulong? >? ReadWriteUInt64NHashSetN( DeSerializeContext< HashSet< ulong? >? > context );

	HashSet< ulong? >? ReadWriteUInt64NHashSetN( HashSet< ulong? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< ulong > ReadWriteUInt64List( DeSerializeContext< List< ulong > > context );

	List< ulong > ReadWriteUInt64List( List< ulong > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< ulong >? ReadWriteUInt64ListN( DeSerializeContext< List< ulong >? > context );

	List< ulong >? ReadWriteUInt64ListN( List< ulong >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< ulong? > ReadWriteUInt64NList( DeSerializeContext< List< ulong? > > context );

	List< ulong? > ReadWriteUInt64NList( List< ulong? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< ulong? >? ReadWriteUInt64NListN( DeSerializeContext< List< ulong? >? > context );

	List< ulong? >? ReadWriteUInt64NListN( List< ulong? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	float ReadWriteFloat( DeSerializeContext< float > context );

	float ReadWriteFloat( float value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	float? ReadWriteFloatN( DeSerializeContext< float? > context );

	float? ReadWriteFloatN( float? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	float[] ReadWriteFloatArr( DeSerializeContext< float[] > context );

	float[] ReadWriteFloatArr( float[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	float[]? ReadWriteFloatArrN( DeSerializeContext< float[]? > context );

	float[]? ReadWriteFloatArrN( float[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	float?[] ReadWriteFloatNArr( DeSerializeContext< float?[] > context );

	float?[] ReadWriteFloatNArr( float?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	float?[]? ReadWriteFloatNArrN( DeSerializeContext< float?[]? > context );

	float?[]? ReadWriteFloatNArrN( float?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< float > ReadWriteFloatHashSet( DeSerializeContext< HashSet< float > > context );

	HashSet< float > ReadWriteFloatHashSet( HashSet< float > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< float >? ReadWriteFloatHashSetN( DeSerializeContext< HashSet< float >? > context );

	HashSet< float >? ReadWriteFloatHashSetN( HashSet< float >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< float? > ReadWriteFloatNHashSet( DeSerializeContext< HashSet< float? > > context );

	HashSet< float? > ReadWriteFloatNHashSet( HashSet< float? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< float? >? ReadWriteFloatNHashSetN( DeSerializeContext< HashSet< float? >? > context );

	HashSet< float? >? ReadWriteFloatNHashSetN( HashSet< float? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< float > ReadWriteFloatList( DeSerializeContext< List< float > > context );

	List< float > ReadWriteFloatList( List< float > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< float >? ReadWriteFloatListN( DeSerializeContext< List< float >? > context );

	List< float >? ReadWriteFloatListN( List< float >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< float? > ReadWriteFloatNList( DeSerializeContext< List< float? > > context );

	List< float? > ReadWriteFloatNList( List< float? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< float? >? ReadWriteFloatNListN( DeSerializeContext< List< float? >? > context );

	List< float? >? ReadWriteFloatNListN( List< float? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	double ReadWriteDouble( DeSerializeContext< double > context );

	double ReadWriteDouble( double value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	double? ReadWriteDoubleN( DeSerializeContext< double? > context );

	double? ReadWriteDoubleN( double? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	double[] ReadWriteDoubleArr( DeSerializeContext< double[] > context );

	double[] ReadWriteDoubleArr( double[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	double[]? ReadWriteDoubleArrN( DeSerializeContext< double[]? > context );

	double[]? ReadWriteDoubleArrN( double[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	double?[] ReadWriteDoubleNArr( DeSerializeContext< double?[] > context );

	double?[] ReadWriteDoubleNArr( double?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	double?[]? ReadWriteDoubleNArrN( DeSerializeContext< double?[]? > context );

	double?[]? ReadWriteDoubleNArrN( double?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< double > ReadWriteDoubleHashSet( DeSerializeContext< HashSet< double > > context );

	HashSet< double > ReadWriteDoubleHashSet( HashSet< double > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< double >? ReadWriteDoubleHashSetN( DeSerializeContext< HashSet< double >? > context );

	HashSet< double >? ReadWriteDoubleHashSetN( HashSet< double >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< double? > ReadWriteDoubleNHashSet( DeSerializeContext< HashSet< double? > > context );

	HashSet< double? > ReadWriteDoubleNHashSet( HashSet< double? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< double? >? ReadWriteDoubleNHashSetN( DeSerializeContext< HashSet< double? >? > context );

	HashSet< double? >? ReadWriteDoubleNHashSetN( HashSet< double? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< double > ReadWriteDoubleList( DeSerializeContext< List< double > > context );

	List< double > ReadWriteDoubleList( List< double > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< double >? ReadWriteDoubleListN( DeSerializeContext< List< double >? > context );

	List< double >? ReadWriteDoubleListN( List< double >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< double? > ReadWriteDoubleNList( DeSerializeContext< List< double? > > context );

	List< double? > ReadWriteDoubleNList( List< double? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< double? >? ReadWriteDoubleNListN( DeSerializeContext< List< double? >? > context );

	List< double? >? ReadWriteDoubleNListN( List< double? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	decimal ReadWriteDecimal( DeSerializeContext< decimal > context );

	decimal ReadWriteDecimal( decimal value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	decimal? ReadWriteDecimalN( DeSerializeContext< decimal? > context );

	decimal? ReadWriteDecimalN( decimal? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	decimal[] ReadWriteDecimalArr( DeSerializeContext< decimal[] > context );

	decimal[] ReadWriteDecimalArr( decimal[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	decimal[]? ReadWriteDecimalArrN( DeSerializeContext< decimal[]? > context );

	decimal[]? ReadWriteDecimalArrN( decimal[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	decimal?[] ReadWriteDecimalNArr( DeSerializeContext< decimal?[] > context );

	decimal?[] ReadWriteDecimalNArr( decimal?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	decimal?[]? ReadWriteDecimalNArrN( DeSerializeContext< decimal?[]? > context );

	decimal?[]? ReadWriteDecimalNArrN( decimal?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< decimal > ReadWriteDecimalHashSet( DeSerializeContext< HashSet< decimal > > context );

	HashSet< decimal > ReadWriteDecimalHashSet( HashSet< decimal > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< decimal >? ReadWriteDecimalHashSetN( DeSerializeContext< HashSet< decimal >? > context );

	HashSet< decimal >? ReadWriteDecimalHashSetN( HashSet< decimal >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< decimal? > ReadWriteDecimalNHashSet( DeSerializeContext< HashSet< decimal? > > context );

	HashSet< decimal? > ReadWriteDecimalNHashSet( HashSet< decimal? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< decimal? >? ReadWriteDecimalNHashSetN( DeSerializeContext< HashSet< decimal? >? > context );

	HashSet< decimal? >? ReadWriteDecimalNHashSetN( HashSet< decimal? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< decimal > ReadWriteDecimalList( DeSerializeContext< List< decimal > > context );

	List< decimal > ReadWriteDecimalList( List< decimal > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< decimal >? ReadWriteDecimalListN( DeSerializeContext< List< decimal >? > context );

	List< decimal >? ReadWriteDecimalListN( List< decimal >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< decimal? > ReadWriteDecimalNList( DeSerializeContext< List< decimal? > > context );

	List< decimal? > ReadWriteDecimalNList( List< decimal? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< decimal? >? ReadWriteDecimalNListN( DeSerializeContext< List< decimal? >? > context );

	List< decimal? >? ReadWriteDecimalNListN( List< decimal? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	Guid ReadWriteGuid( DeSerializeContext< Guid > context );

	Guid ReadWriteGuid( Guid value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	Guid? ReadWriteGuidN( DeSerializeContext< Guid? > context );

	Guid? ReadWriteGuidN( Guid? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	DateTime ReadWriteDateTime( DeSerializeContext< DateTime > context );

	DateTime ReadWriteDateTime( DateTime value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	DateTime? ReadWriteDateTimeN( DeSerializeContext< DateTime? > context );

	DateTime? ReadWriteDateTimeN( DateTime? value,
		[ CallerArgumentExpression( nameof( value ) ) ]
		string? argumentName = null );

	DateTimeOffset ReadWriteDateTimeOffset( DeSerializeContext< DateTimeOffset > context );

	DateTimeOffset ReadWriteDateTimeOffset( DateTimeOffset value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	DateTimeOffset? ReadWriteDateTimeOffsetN( DeSerializeContext< DateTimeOffset? > context );

	DateTimeOffset? ReadWriteDateTimeOffsetN( DateTimeOffset? value,
		[ CallerArgumentExpression( nameof( value ) ) ]
		string? argumentName = null );

	TimeSpan ReadWriteTimeSpan( DeSerializeContext< TimeSpan > context );

	TimeSpan ReadWriteTimeSpan( TimeSpan value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	TimeSpan? ReadWriteTimeSpanN( DeSerializeContext< TimeSpan? > context );

	TimeSpan? ReadWriteTimeSpanN( TimeSpan? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	TEnum ReadWriteEnum< TEnum >( DeSerializeContext< TEnum > context )
		where TEnum : Enum;

	TEnum ReadWriteEnum< TEnum >( TEnum value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
		where TEnum : Enum;

	string ReadWriteString( DeSerializeContext< string > context );

	string ReadWriteString( string value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	string? ReadWriteStringN( DeSerializeContext< string? > context );

	string? ReadWriteStringN( string? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	string[] ReadWriteStringArr( DeSerializeContext< string[] > context );

	string[] ReadWriteStringArr( string[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	string[]? ReadWriteStringArrN( DeSerializeContext< string[]? > context );

	string[]? ReadWriteStringArrN( string[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	string?[] ReadWriteStringNArr( DeSerializeContext< string?[] > context );

	string?[] ReadWriteStringNArr( string?[] value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	string?[]? ReadWriteStringNArrN( DeSerializeContext< string?[]? > context );

	string?[]? ReadWriteStringNArrN( string?[]? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< string > ReadWriteStringList( DeSerializeContext< List< string > > context );

	List< string > ReadWriteStringList( List< string > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< string >? ReadWriteStringListN( DeSerializeContext< List< string >? > context );

	List< string >? ReadWriteStringListN( List< string >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< string? > ReadWriteStringNList( DeSerializeContext< List< string? > > context );

	List< string? > ReadWriteStringNList( List< string? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< string? >? ReadWriteStringNListN( DeSerializeContext< List< string? >? > context );

	List< string? >? ReadWriteStringNListN( List< string? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< string > ReadWriteStringHashSet( DeSerializeContext< HashSet< string > > context );

	HashSet< string > ReadWriteStringHashSet( HashSet< string > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< string >? ReadWriteStringHashSetN( DeSerializeContext< HashSet< string >? > context );

	HashSet< string >? ReadWriteStringHashSetN( HashSet< string >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< string? > ReadWriteStringNHashSet( DeSerializeContext< HashSet< string? > > context );

	HashSet< string? > ReadWriteStringNHashSet( HashSet< string? > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< string? >? ReadWriteStringNHashSetN( DeSerializeContext< HashSet< string? >? > context );

	HashSet< string? >? ReadWriteStringNHashSetN( HashSet< string? >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	T ReadWrite< T >( DeSerializeContext< T > context )
		where T : IDeSerializable;

	T ReadWrite< T >( T value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null, int? valueIndex = null )
		where T : IDeSerializable;

	T ReadWrite< T >( T value, Func< DeSerializeContext< T >, T > objectDeSerialization, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null, int? valueIndex = null );

	T? ReadWriteN< T >( DeSerializeContext< T? > context )
		where T : IDeSerializable;

	T? ReadWriteN< T >( T? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null, int? valueIndex = null )
		where T : IDeSerializable;

	T? ReadWriteN< T >( T? value, Func< DeSerializeContext< T >, T > objectDeSerialization, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null, int? valueIndex = null );

	List< T > ReadWriteList< T >( DeSerializeContext< List< T > > context )
		where T : IDeSerializable;

	List< T > ReadWriteList< T >( List< T > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
		where T : IDeSerializable;

	List< T > ReadWriteList< T >( List< T > value, Func< DeSerializeContext< T >, T > itemDeSerialization, bool isPrimitive = false, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	List< T >? ReadWriteListN< T >( DeSerializeContext< List< T >? > context )
		where T : IDeSerializable;

	List< T >? ReadWriteListN< T >( List< T >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
		where T : IDeSerializable;

	List< T >? ReadWriteListN< T >( List< T >? value, Func< DeSerializeContext< T >, T > itemDeSerialization, bool isPrimitive = false, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< T > ReadWriteHashSet< T >( HashSet< T > value, Func< DeSerializeContext< T >, T > itemDeSerialization, bool isPrimitive = false, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< T > ReadWriteHashSet< T >( DeSerializeContext< HashSet< T > > context )
		where T : IDeSerializable;

	HashSet< T > ReadWriteHashSet< T >( HashSet< T > value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
		where T : IDeSerializable;

	HashSet< T >? ReadWriteHashSetN< T >( HashSet< T >? value, Func< DeSerializeContext< T >, T > itemDeSerialization, bool isPrimitive = false, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null );

	HashSet< T >? ReadWriteHashSetN< T >( DeSerializeContext< HashSet< T >? > context )
		where T : IDeSerializable;

	HashSet< T >? ReadWriteHashSetN< T >( HashSet< T >? value, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
		where T : IDeSerializable;

	Dictionary< TKey, TValue > ReadWriteDic< TKey, TValue >( Dictionary< TKey, TValue > value, Func< DeSerializeContext< TKey >, TKey > keyDeSerialization, Func< DeSerializeContext< TValue >, TValue > valueDeSerialization, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
		where TKey : notnull;

	Dictionary< TKey, TValue >? ReadWriteDicN< TKey, TValue >( Dictionary< TKey, TValue >? value, Func< DeSerializeContext< TKey >, TKey > keyDeSerialization, Func< DeSerializeContext< TValue >, TValue > valueDeSerialization, [ CallerArgumentExpression( nameof( value ) ) ]string? argumentName = null )
		where TKey : notnull;

	ushort GetVersion< T >()
		where T : IDeSerializable;
}
