using System.Runtime.CompilerServices;

namespace Erlin.Lib.Common.DeSerialization.ReadWrite;

/// <summary>
///    DeSerializer for both complex and primitive data - primitives support
/// </summary>
partial class DeSerializer
{
	public bool ReadWriteBool( DeSerializeContext<bool> context )
	{
		return ReadWriteBool( context.GetValue(), context.ArgumentName );
	}

	public bool ReadWriteBool(
		bool value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteBool( argumentName, value );
		}
		else
		{
			value = Reader.ReadBool( argumentName );
		}

		return value;
	}

	public bool? ReadWriteBoolN( DeSerializeContext<bool?> context )
	{
		return ReadWriteBoolN( context.GetValue(), context.ArgumentName );
	}

	public bool? ReadWriteBoolN(
		bool? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteBoolN( argumentName, value );
		}
		else
		{
			value = Reader.ReadBoolN( argumentName );
		}

		return value;
	}

	public sbyte ReadWriteSByte( DeSerializeContext<sbyte> context )
	{
		return ReadWriteSByte( context.GetValue(), context.ArgumentName );
	}

	public sbyte ReadWriteSByte(
		sbyte value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteSByte( argumentName, value );
		}
		else
		{
			value = Reader.ReadSByte( argumentName );
		}

		return value;
	}

	public sbyte? ReadWriteSByteN( DeSerializeContext<sbyte?> context )
	{
		return ReadWriteSByteN( context.GetValue(), context.ArgumentName );
	}

	public sbyte? ReadWriteSByteN(
		sbyte? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteSByteN( argumentName, value );
		}
		else
		{
			value = Reader.ReadSByteN( argumentName );
		}

		return value;
	}

	public sbyte[] ReadWriteSByteArr( DeSerializeContext<sbyte[]> context )
	{
		return ReadWriteSByteArr( context.GetValue(), context.ArgumentName );
	}

	public sbyte[] ReadWriteSByteArr(
		sbyte[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteSByteArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadSByteArr( argumentName );
		}

		return value;
	}

	public sbyte[]? ReadWriteSByteArrN( DeSerializeContext<sbyte[]?> context )
	{
		return ReadWriteSByteArrN( context.GetValue(), context.ArgumentName );
	}

	public sbyte[]? ReadWriteSByteArrN(
		sbyte[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteSByteArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadSByteArrN( argumentName );
		}

		return value;
	}

	public sbyte?[] ReadWriteSByteNArr( DeSerializeContext<sbyte?[]> context )
	{
		return ReadWriteSByteNArr( context.GetValue(), context.ArgumentName );
	}

	public sbyte?[] ReadWriteSByteNArr(
		sbyte?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteSByteNArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadSByteNArr( argumentName );
		}

		return value;
	}

	public sbyte?[]? ReadWriteSByteNArrN( DeSerializeContext<sbyte?[]?> context )
	{
		return ReadWriteSByteNArrN( context.GetValue(), context.ArgumentName );
	}

	public sbyte?[]? ReadWriteSByteNArrN(
		sbyte?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteSByteNArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadSByteNArrN( argumentName );
		}

		return value;
	}

	public byte ReadWriteByte( DeSerializeContext<byte> context )
	{
		return ReadWriteByte( context.GetValue(), context.ArgumentName );
	}

	public byte ReadWriteByte(
		byte value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteByte( argumentName, value );
		}
		else
		{
			value = Reader.ReadByte( argumentName );
		}

		return value;
	}

	public byte? ReadWriteByteN( DeSerializeContext<byte?> context )
	{
		return ReadWriteByteN( context.GetValue(), context.ArgumentName );
	}

	public byte? ReadWriteByteN(
		byte? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteByteN( argumentName, value );
		}
		else
		{
			value = Reader.ReadByteN( argumentName );
		}

		return value;
	}

	public byte[] ReadWriteByteArr( DeSerializeContext<byte[]> context )
	{
		return ReadWriteByteArr( context.GetValue(), context.ArgumentName );
	}

	public byte[] ReadWriteByteArr(
		byte[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteByteArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadByteArr( argumentName );
		}

		return value;
	}

	public byte[]? ReadWriteByteArrN( DeSerializeContext<byte[]?> context )
	{
		return ReadWriteByteArrN( context.GetValue(), context.ArgumentName );
	}

	public byte[]? ReadWriteByteArrN(
		byte[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteByteArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadByteArrN( argumentName );
		}

		return value;
	}

	public byte?[] ReadWriteByteNArr( DeSerializeContext<byte?[]> context )
	{
		return ReadWriteByteNArr( context.GetValue(), context.ArgumentName );
	}

	public byte?[] ReadWriteByteNArr(
		byte?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteByteNArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadByteNArr( argumentName );
		}

		return value;
	}

	public byte?[]? ReadWriteByteNArrN( DeSerializeContext<byte?[]?> context )
	{
		return ReadWriteByteNArrN( context.GetValue(), context.ArgumentName );
	}

	public byte?[]? ReadWriteByteNArrN(
		byte?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteByteNArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadByteNArrN( argumentName );
		}

		return value;
	}

	public short ReadWriteInt16( DeSerializeContext<short> context )
	{
		return ReadWriteInt16( context.GetValue(), context.ArgumentName );
	}

	public short ReadWriteInt16(
		short value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt16( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt16( argumentName );
		}

		return value;
	}

	public short? ReadWriteInt16N( DeSerializeContext<short?> context )
	{
		return ReadWriteInt16N( context.GetValue(), context.ArgumentName );
	}

	public short? ReadWriteInt16N(
		short? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt16N( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt16N( argumentName );
		}

		return value;
	}

	public short[] ReadWriteInt16Arr( DeSerializeContext<short[]> context )
	{
		return ReadWriteInt16Arr( context.GetValue(), context.ArgumentName );
	}

	public short[] ReadWriteInt16Arr(
		short[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteInt16Arr( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt16Arr( argumentName );
		}

		return value;
	}

	public short[]? ReadWriteInt16ArrN( DeSerializeContext<short[]?> context )
	{
		return ReadWriteInt16ArrN( context.GetValue(), context.ArgumentName );
	}

	public short[]? ReadWriteInt16ArrN(
		short[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt16ArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt16ArrN( argumentName );
		}

		return value;
	}

	public short?[] ReadWriteInt16NArr( DeSerializeContext<short?[]> context )
	{
		return ReadWriteInt16NArr( context.GetValue(), context.ArgumentName );
	}

	public short?[] ReadWriteInt16NArr(
		short?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteInt16NArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt16NArr( argumentName );
		}

		return value;
	}

	public short?[]? ReadWriteInt16NArrN( DeSerializeContext<short?[]?> context )
	{
		return ReadWriteInt16NArrN( context.GetValue(), context.ArgumentName );
	}

	public short?[]? ReadWriteInt16NArrN(
		short?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt16NArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt16NArrN( argumentName );
		}

		return value;
	}

	public ushort ReadWriteUInt16( DeSerializeContext<ushort> context )
	{
		return ReadWriteUInt16( context.GetValue(), context.ArgumentName );
	}

	public ushort ReadWriteUInt16(
		ushort value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt16( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt16( argumentName );
		}

		return value;
	}

	public ushort? ReadWriteUInt16N( DeSerializeContext<ushort?> context )
	{
		return ReadWriteUInt16N( context.GetValue(), context.ArgumentName );
	}

	public ushort? ReadWriteUInt16N(
		ushort? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt16N( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt16N( argumentName );
		}

		return value;
	}

	public ushort[] ReadWriteUInt16Arr( DeSerializeContext<ushort[]> context )
	{
		return ReadWriteUInt16Arr( context.GetValue(), context.ArgumentName );
	}

	public ushort[] ReadWriteUInt16Arr(
		ushort[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteUInt16Arr( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt16Arr( argumentName );
		}

		return value;
	}

	public ushort[]? ReadWriteUInt16ArrN( DeSerializeContext<ushort[]?> context )
	{
		return ReadWriteUInt16ArrN( context.GetValue(), context.ArgumentName );
	}

	public ushort[]? ReadWriteUInt16ArrN(
		ushort[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt16ArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt16ArrN( argumentName );
		}

		return value;
	}

	public ushort?[] ReadWriteUInt16NArr( DeSerializeContext<ushort?[]> context )
	{
		return ReadWriteUInt16NArr( context.GetValue(), context.ArgumentName );
	}

	public ushort?[] ReadWriteUInt16NArr(
		ushort?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteUInt16NArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt16NArr( argumentName );
		}

		return value;
	}

	public ushort?[]? ReadWriteUInt16NArrN( DeSerializeContext<ushort?[]?> context )
	{
		return ReadWriteUInt16NArrN( context.GetValue(), context.ArgumentName );
	}

	public ushort?[]? ReadWriteUInt16NArrN(
		ushort?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt16NArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt16NArrN( argumentName );
		}

		return value;
	}

	public int ReadWriteInt32( DeSerializeContext<int> context )
	{
		return ReadWriteInt32( context.GetValue(), context.ArgumentName );
	}

	public int ReadWriteInt32(
		int value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt32( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt32( argumentName );
		}

		return value;
	}

	public int? ReadWriteInt32N( DeSerializeContext<int?> context )
	{
		return ReadWriteInt32N( context.GetValue(), context.ArgumentName );
	}

	public int? ReadWriteInt32N(
		int? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt32N( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt32N( argumentName );
		}

		return value;
	}

	public int[] ReadWriteInt32Arr( DeSerializeContext<int[]> context )
	{
		return ReadWriteInt32Arr( context.GetValue(), context.ArgumentName );
	}

	public int[] ReadWriteInt32Arr(
		int[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteInt32Arr( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt32Arr( argumentName );
		}

		return value;
	}

	public int[]? ReadWriteInt32ArrN( DeSerializeContext<int[]?> context )
	{
		return ReadWriteInt32ArrN( context.GetValue(), context.ArgumentName );
	}

	public int[]? ReadWriteInt32ArrN(
		int[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt32ArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt32ArrN( argumentName );
		}

		return value;
	}

	public int?[] ReadWriteInt32NArr( DeSerializeContext<int?[]> context )
	{
		return ReadWriteInt32NArr( context.GetValue(), context.ArgumentName );
	}

	public int?[] ReadWriteInt32NArr(
		int?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteInt32NArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt32NArr( argumentName );
		}

		return value;
	}

	public int?[]? ReadWriteInt32NArrN( DeSerializeContext<int?[]?> context )
	{
		return ReadWriteInt32NArrN( context.GetValue(), context.ArgumentName );
	}

	public int?[]? ReadWriteInt32NArrN(
		int?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt32NArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt32NArrN( argumentName );
		}

		return value;
	}

	public uint ReadWriteUInt32( DeSerializeContext<uint> context )
	{
		return ReadWriteUInt32( context.GetValue(), context.ArgumentName );
	}

	public uint ReadWriteUInt32(
		uint value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt32( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt32( argumentName );
		}

		return value;
	}

	public uint? ReadWriteUInt32N( DeSerializeContext<uint?> context )
	{
		return ReadWriteUInt32N( context.GetValue(), context.ArgumentName );
	}

	public uint? ReadWriteUInt32N(
		uint? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt32N( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt32N( argumentName );
		}

		return value;
	}

	public uint[] ReadWriteUInt32Arr( DeSerializeContext<uint[]> context )
	{
		return ReadWriteUInt32Arr( context.GetValue(), context.ArgumentName );
	}

	public uint[] ReadWriteUInt32Arr(
		uint[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteUInt32Arr( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt32Arr( argumentName );
		}

		return value;
	}

	public uint[]? ReadWriteUInt32ArrN( DeSerializeContext<uint[]?> context )
	{
		return ReadWriteUInt32ArrN( context.GetValue(), context.ArgumentName );
	}

	public uint[]? ReadWriteUInt32ArrN(
		uint[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt32ArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt32ArrN( argumentName );
		}

		return value;
	}

	public uint?[] ReadWriteUInt32NArr( DeSerializeContext<uint?[]> context )
	{
		return ReadWriteUInt32NArr( context.GetValue(), context.ArgumentName );
	}

	public uint?[] ReadWriteUInt32NArr(
		uint?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteUInt32NArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt32NArr( argumentName );
		}

		return value;
	}

	public uint?[]? ReadWriteUInt32NArrN( DeSerializeContext<uint?[]?> context )
	{
		return ReadWriteUInt32NArrN( context.GetValue(), context.ArgumentName );
	}

	public uint?[]? ReadWriteUInt32NArrN(
		uint?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt32NArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt32NArrN( argumentName );
		}

		return value;
	}

	public long ReadWriteInt64( DeSerializeContext<long> context )
	{
		return ReadWriteInt64( context.GetValue(), context.ArgumentName );
	}

	public long ReadWriteInt64(
		long value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt64( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt64( argumentName );
		}

		return value;
	}

	public long? ReadWriteInt64N( DeSerializeContext<long?> context )
	{
		return ReadWriteInt64N( context.GetValue(), context.ArgumentName );
	}

	public long? ReadWriteInt64N(
		long? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt64N( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt64N( argumentName );
		}

		return value;
	}

	public long[] ReadWriteInt64Arr( DeSerializeContext<long[]> context )
	{
		return ReadWriteInt64Arr( context.GetValue(), context.ArgumentName );
	}

	public long[] ReadWriteInt64Arr(
		long[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteInt64Arr( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt64Arr( argumentName );
		}

		return value;
	}

	public long[]? ReadWriteInt64ArrN( DeSerializeContext<long[]?> context )
	{
		return ReadWriteInt64ArrN( context.GetValue(), context.ArgumentName );
	}

	public long[]? ReadWriteInt64ArrN(
		long[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt64ArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt64ArrN( argumentName );
		}

		return value;
	}

	public long?[] ReadWriteInt64NArr( DeSerializeContext<long?[]> context )
	{
		return ReadWriteInt64NArr( context.GetValue(), context.ArgumentName );
	}

	public long?[] ReadWriteInt64NArr(
		long?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteInt64NArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt64NArr( argumentName );
		}

		return value;
	}

	public long?[]? ReadWriteInt64NArrN( DeSerializeContext<long?[]?> context )
	{
		return ReadWriteInt64NArrN( context.GetValue(), context.ArgumentName );
	}

	public long?[]? ReadWriteInt64NArrN(
		long?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteInt64NArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadInt64NArrN( argumentName );
		}

		return value;
	}

	public ulong ReadWriteUInt64( DeSerializeContext<ulong> context )
	{
		return ReadWriteUInt64( context.GetValue(), context.ArgumentName );
	}

	public ulong ReadWriteUInt64(
		ulong value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt64( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt64( argumentName );
		}

		return value;
	}

	public ulong? ReadWriteUInt64N( DeSerializeContext<ulong?> context )
	{
		return ReadWriteUInt64N( context.GetValue(), context.ArgumentName );
	}

	public ulong? ReadWriteUInt64N(
		ulong? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt64N( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt64N( argumentName );
		}

		return value;
	}

	public ulong[] ReadWriteUInt64Arr( DeSerializeContext<ulong[]> context )
	{
		return ReadWriteUInt64Arr( context.GetValue(), context.ArgumentName );
	}

	public ulong[] ReadWriteUInt64Arr(
		ulong[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteUInt64Arr( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt64Arr( argumentName );
		}

		return value;
	}

	public ulong[]? ReadWriteUInt64ArrN( DeSerializeContext<ulong[]?> context )
	{
		return ReadWriteUInt64ArrN( context.GetValue(), context.ArgumentName );
	}

	public ulong[]? ReadWriteUInt64ArrN(
		ulong[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt64ArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt64ArrN( argumentName );
		}

		return value;
	}

	public ulong?[] ReadWriteUInt64NArr( DeSerializeContext<ulong?[]> context )
	{
		return ReadWriteUInt64NArr( context.GetValue(), context.ArgumentName );
	}

	public ulong?[] ReadWriteUInt64NArr(
		ulong?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteUInt64NArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt64NArr( argumentName );
		}

		return value;
	}

	public ulong?[]? ReadWriteUInt64NArrN( DeSerializeContext<ulong?[]?> context )
	{
		return ReadWriteUInt64NArrN( context.GetValue(), context.ArgumentName );
	}

	public ulong?[]? ReadWriteUInt64NArrN(
		ulong?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteUInt64NArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadUInt64NArrN( argumentName );
		}

		return value;
	}

	public float ReadWriteFloat( DeSerializeContext<float> context )
	{
		return ReadWriteFloat( context.GetValue(), context.ArgumentName );
	}

	public float ReadWriteFloat(
		float value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteFloat( argumentName, value );
		}
		else
		{
			value = Reader.ReadFloat( argumentName );
		}

		return value;
	}

	public float? ReadWriteFloatN( DeSerializeContext<float?> context )
	{
		return ReadWriteFloatN( context.GetValue(), context.ArgumentName );
	}

	public float? ReadWriteFloatN(
		float? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteFloatN( argumentName, value );
		}
		else
		{
			value = Reader.ReadFloatN( argumentName );
		}

		return value;
	}

	public float[] ReadWriteFloatArr( DeSerializeContext<float[]> context )
	{
		return ReadWriteFloatArr( context.GetValue(), context.ArgumentName );
	}

	public float[] ReadWriteFloatArr(
		float[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteFloatArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadFloatArr( argumentName );
		}

		return value;
	}

	public float[]? ReadWriteFloatArrN( DeSerializeContext<float[]?> context )
	{
		return ReadWriteFloatArrN( context.GetValue(), context.ArgumentName );
	}

	public float[]? ReadWriteFloatArrN(
		float[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteFloatArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadFloatArrN( argumentName );
		}

		return value;
	}

	public float?[] ReadWriteFloatNArr( DeSerializeContext<float?[]> context )
	{
		return ReadWriteFloatNArr( context.GetValue(), context.ArgumentName );
	}

	public float?[] ReadWriteFloatNArr(
		float?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteFloatNArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadFloatNArr( argumentName );
		}

		return value;
	}

	public float?[]? ReadWriteFloatNArrN( DeSerializeContext<float?[]?> context )
	{
		return ReadWriteFloatNArrN( context.GetValue(), context.ArgumentName );
	}

	public float?[]? ReadWriteFloatNArrN(
		float?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteFloatNArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadFloatNArrN( argumentName );
		}

		return value;
	}

	public double ReadWriteDouble( DeSerializeContext<double> context )
	{
		return ReadWriteDouble( context.GetValue(), context.ArgumentName );
	}

	public double ReadWriteDouble(
		double value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDouble( argumentName, value );
		}
		else
		{
			value = Reader.ReadDouble( argumentName );
		}

		return value;
	}

	public double? ReadWriteDoubleN( DeSerializeContext<double?> context )
	{
		return ReadWriteDoubleN( context.GetValue(), context.ArgumentName );
	}

	public double? ReadWriteDoubleN(
		double? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDoubleN( argumentName, value );
		}
		else
		{
			value = Reader.ReadDoubleN( argumentName );
		}

		return value;
	}

	public double[] ReadWriteDoubleArr( DeSerializeContext<double[]> context )
	{
		return ReadWriteDoubleArr( context.GetValue(), context.ArgumentName );
	}

	public double[] ReadWriteDoubleArr(
		double[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteDoubleArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadDoubleArr( argumentName );
		}

		return value;
	}

	public double[]? ReadWriteDoubleArrN( DeSerializeContext<double[]?> context )
	{
		return ReadWriteDoubleArrN( context.GetValue(), context.ArgumentName );
	}

	public double[]? ReadWriteDoubleArrN(
		double[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDoubleArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadDoubleArrN( argumentName );
		}

		return value;
	}

	public double?[] ReadWriteDoubleNArr( DeSerializeContext<double?[]> context )
	{
		return ReadWriteDoubleNArr( context.GetValue(), context.ArgumentName );
	}

	public double?[] ReadWriteDoubleNArr(
		double?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteDoubleNArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadDoubleNArr( argumentName );
		}

		return value;
	}

	public double?[]? ReadWriteDoubleNArrN( DeSerializeContext<double?[]?> context )
	{
		return ReadWriteDoubleNArrN( context.GetValue(), context.ArgumentName );
	}

	public double?[]? ReadWriteDoubleNArrN(
		double?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDoubleNArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadDoubleNArrN( argumentName );
		}

		return value;
	}

	public decimal ReadWriteDecimal( DeSerializeContext<decimal> context )
	{
		return ReadWriteDecimal( context.GetValue(), context.ArgumentName );
	}

	public decimal ReadWriteDecimal(
		decimal value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDecimal( argumentName, value );
		}
		else
		{
			value = Reader.ReadDecimal( argumentName );
		}

		return value;
	}

	public decimal? ReadWriteDecimalN( DeSerializeContext<decimal?> context )
	{
		return ReadWriteDecimalN( context.GetValue(), context.ArgumentName );
	}

	public decimal? ReadWriteDecimalN(
		decimal? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDecimalN( argumentName, value );
		}
		else
		{
			value = Reader.ReadDecimalN( argumentName );
		}

		return value;
	}

	public decimal[] ReadWriteDecimalArr( DeSerializeContext<decimal[]> context )
	{
		return ReadWriteDecimalArr( context.GetValue(), context.ArgumentName );
	}

	public decimal[] ReadWriteDecimalArr(
		decimal[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteDecimalArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadDecimalArr( argumentName );
		}

		return value;
	}

	public decimal[]? ReadWriteDecimalArrN( DeSerializeContext<decimal[]?> context )
	{
		return ReadWriteDecimalArrN( context.GetValue(), context.ArgumentName );
	}

	public decimal[]? ReadWriteDecimalArrN(
		decimal[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDecimalArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadDecimalArrN( argumentName );
		}

		return value;
	}

	public decimal?[] ReadWriteDecimalNArr( DeSerializeContext<decimal?[]> context )
	{
		return ReadWriteDecimalNArr( context.GetValue(), context.ArgumentName );
	}

	public decimal?[] ReadWriteDecimalNArr(
		decimal?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteDecimalNArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadDecimalNArr( argumentName );
		}

		return value;
	}

	public decimal?[]? ReadWriteDecimalNArrN( DeSerializeContext<decimal?[]?> context )
	{
		return ReadWriteDecimalNArrN( context.GetValue(), context.ArgumentName );
	}

	public decimal?[]? ReadWriteDecimalNArrN(
		decimal?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDecimalNArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadDecimalNArrN( argumentName );
		}

		return value;
	}

	public Guid ReadWriteGuid( DeSerializeContext<Guid> context )
	{
		return ReadWriteGuid( context.GetValue(), context.ArgumentName );
	}

	public Guid ReadWriteGuid(
		Guid value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteGuid( argumentName, value );
		}
		else
		{
			value = Reader.ReadGuid( argumentName );
		}

		return value;
	}

	public Guid? ReadWriteGuidN( DeSerializeContext<Guid?> context )
	{
		return ReadWriteGuidN( context.GetValue(), context.ArgumentName );
	}

	public Guid? ReadWriteGuidN(
		Guid? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteGuidN( argumentName, value );
		}
		else
		{
			value = Reader.ReadGuidN( argumentName );
		}

		return value;
	}

	public string ReadWriteString( DeSerializeContext<string> context )
	{
		return ReadWriteString( context.GetValue(), context.ArgumentName );
	}

	public string ReadWriteString(
		string value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteString( argumentName, value );
		}
		else
		{
			value = Reader.ReadString( argumentName );
		}

		return value;
	}

	public string? ReadWriteStringN( DeSerializeContext<string?> context )
	{
		return ReadWriteStringN( context.GetValue(), context.ArgumentName );
	}

	public string? ReadWriteStringN(
		string? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteStringN( argumentName, value );
		}
		else
		{
			value = Reader.ReadStringN( argumentName );
		}

		return value;
	}

	public string[] ReadWriteStringArr( DeSerializeContext<string[]> context )
	{
		return ReadWriteStringArr( context.GetValue(), context.ArgumentName );
	}

	public string[] ReadWriteStringArr(
		string[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteStringArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadStringArr( argumentName );
		}

		return value;
	}

	public string[]? ReadWriteStringArrN( DeSerializeContext<string[]?> context )
	{
		return ReadWriteStringArrN( context.GetValue(), context.ArgumentName );
	}

	public string[]? ReadWriteStringArrN(
		string[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteStringArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadStringArrN( argumentName );
		}

		return value;
	}

	public string?[] ReadWriteStringNArr( DeSerializeContext<string?[]> context )
	{
		return ReadWriteStringNArr( context.GetValue(), context.ArgumentName );
	}

	public string?[] ReadWriteStringNArr(
		string?[] value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			if( value is null )
			{
				throw new DeSerializeException(
					$"Writing NULL value on expected instance! ArgName: {argumentName}" );
			}

			Writer.WriteStringNArr( argumentName, value );
		}
		else
		{
			value = Reader.ReadStringNArr( argumentName );
		}

		return value;
	}

	public string?[]? ReadWriteStringNArrN( DeSerializeContext<string?[]?> context )
	{
		return ReadWriteStringNArrN( context.GetValue(), context.ArgumentName );
	}

	public string?[]? ReadWriteStringNArrN(
		string?[]? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteStringNArrN( argumentName, value );
		}
		else
		{
			value = Reader.ReadStringNArrN( argumentName );
		}

		return value;
	}

	public DateTime ReadWriteDateTime( DeSerializeContext<DateTime> context )
	{
		return ReadWriteDateTime( context.GetValue(), context.ArgumentName );
	}

	public DateTime ReadWriteDateTime(
		DateTime value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDateTime( argumentName, value );
		}
		else
		{
			value = Reader.ReadDateTime( argumentName );
		}

		return value;
	}

	public DateTime? ReadWriteDateTimeN( DeSerializeContext<DateTime?> context )
	{
		return ReadWriteDateTimeN( context.GetValue(), context.ArgumentName );
	}

	public DateTime? ReadWriteDateTimeN(
		DateTime? value,
		[CallerArgumentExpression( nameof( value ) )]
		string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDateTimeN( argumentName, value );
		}
		else
		{
			value = Reader.ReadDateTimeN( argumentName );
		}

		return value;
	}

	public DateTimeOffset ReadWriteDateTimeOffset( DeSerializeContext<DateTimeOffset> context )
	{
		return ReadWriteDateTimeOffset( context.GetValue(), context.ArgumentName );
	}

	public DateTimeOffset ReadWriteDateTimeOffset(
		DateTimeOffset value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDateTimeOffset( argumentName, value );
		}
		else
		{
			value = Reader.ReadDateTimeOffset( argumentName );
		}

		return value;
	}

	public DateTimeOffset? ReadWriteDateTimeOffsetN( DeSerializeContext<DateTimeOffset?> context )
	{
		return ReadWriteDateTimeOffsetN( context.GetValue(), context.ArgumentName );
	}

	public DateTimeOffset? ReadWriteDateTimeOffsetN(
		DateTimeOffset? value,
		[CallerArgumentExpression( nameof( value ) )]
		string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteDateTimeOffsetN( argumentName, value );
		}
		else
		{
			value = Reader.ReadDateTimeOffsetN( argumentName );
		}

		return value;
	}

	public TimeSpan ReadWriteTimeSpan( DeSerializeContext<TimeSpan> context )
	{
		return ReadWriteTimeSpan( context.GetValue(), context.ArgumentName );
	}

	public TimeSpan ReadWriteTimeSpan(
		TimeSpan value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteTimeSpan( argumentName, value );
		}
		else
		{
			value = Reader.ReadTimeSpan( argumentName );
		}

		return value;
	}

	public TimeSpan? ReadWriteTimeSpanN( DeSerializeContext<TimeSpan?> context )
	{
		return ReadWriteTimeSpanN( context.GetValue(), context.ArgumentName );
	}

	public TimeSpan? ReadWriteTimeSpanN(
		TimeSpan? value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
	{
		if( IsWrite )
		{
			Writer.WriteTimeSpanN( argumentName, value );
		}
		else
		{
			value = Reader.ReadTimeSpanN( argumentName );
		}

		return value;
	}

	public TEnum ReadWriteEnum<TEnum>( DeSerializeContext<TEnum> context )
		where TEnum : Enum
	{
		return ReadWriteEnum( context.GetValue(), context.ArgumentName );
	}

	public TEnum ReadWriteEnum<TEnum>(
		TEnum value, [CallerArgumentExpression( nameof( value ) )]string? argumentName = null )
		where TEnum : Enum
	{
		if( IsWrite )
		{
			int number = UniversalConvert.Convert<int>( value );
			Writer.WriteInt32( argumentName, number );
		}
		else
		{
			value = UniversalConvert.Convert<TEnum>( Reader.ReadInt32( argumentName ) );
		}

		return value;
	}
}
