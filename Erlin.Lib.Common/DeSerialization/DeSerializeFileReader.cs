using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

using Erlin.Lib.Common.DeSerialization.ReadWrite;

namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    File reader for DeSerialization
/// </summary>
public class DeSerializeFileReader : IDisposable
{
	/// <summary>
	///    Whether current file is in binary format
	/// </summary>
	private bool IsBinary { get; set; }

	/// <summary>
	///    Path to file that we are reading
	/// </summary>
	private string FilePath { get; }

	/// <summary>
	///    Underlying file stream
	/// </summary>
	private FileStream FileStream { get; }

	/// <summary>
	///    Underlying ZIP compression stream
	/// </summary>
	private GZipStream? ZipStream { get; set; }

	/// <summary>
	///    Main deserializer
	/// </summary>
	public IDeSerializer DS { get; private set; }

	/// <summary>
	///    Type table
	/// </summary>
	private List<DeSerializeType> TypeTable { get; } = new();

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="filePath">File path to write</param>
	public DeSerializeFileReader( string filePath )
	{
		FilePath = filePath;

		FileStream = File.Open( FilePath, FileMode.Open, FileAccess.Read, FileShare.Read );

		ReadFileHeader();
	}

	/// <summary>
	///    Read file header
	/// </summary>
	[MemberNotNull( nameof( DeSerializeFileReader.DS ) )]
	private void ReadFileHeader()
	{
		try
		{
			ResolveFileFormat();
			if( IsBinary )
			{
				ReadBinaryFileHeader();
			}
			else
			{
				ReadJsonFileHeader();
			}
		}
		catch( Exception e )
		{
			throw new DeSerializeException( $"File {FilePath} is not in correct Binary or JSON format!", e );
		}
	}

	/// <summary>
	///    Resolve whether the reading file is in binary or JSON format
	/// </summary>
	private void ResolveFileFormat()
	{
		int formatFlag = FileStream.ReadByte();
		IsBinary = formatFlag == DeSerializeConstants.FILE_HEADER_BINARY;
		FileStream.Seek( 0, SeekOrigin.Begin );
	}

	/// <summary>
	///    Read binary file header
	/// </summary>
	[MemberNotNull( nameof( DeSerializeFileReader.DS ) )]
	private void ReadBinaryFileHeader()
	{
		_ = FileStream.ReadByte();// File format flag
		_ = FileStream.ReadByte();// Version
		_ = FileStream.ReadByte();// Version
		int compressFlag = FileStream.ReadByte();//Compress sign
		bool isCompressed = compressFlag == 1;

		byte[] typeTablePointerArr = new byte[ 8 ];
		_ = FileStream.Read( typeTablePointerArr, 0, typeTablePointerArr.Length );
		long typeTablePointer = BitConverter.ToInt64( typeTablePointerArr, 0 );

		DeSerializeBinaryReader reader = new( FileStream );
		DS = new DeSerializer(
			new DeSerializeMemoryTypeProvider( TypeTable ), new DeSerializeEmptyWriter(), reader );

		long dataPointer = FileStream.Position;

		FileStream.Seek( typeTablePointer, SeekOrigin.Begin );
		if( isCompressed )
		{
			ZipStream = new GZipStream( FileStream, CompressionMode.Decompress, true );
			reader.SwitchStream( ZipStream );
		}

		DeserializeTypeTable( reader );
		FileStream.Seek( dataPointer, SeekOrigin.Begin );

		if( isCompressed )
		{
			reader.SwitchStream( FileStream );
			ZipStream?.Dispose();
			ZipStream = new GZipStream( FileStream, CompressionMode.Decompress, true );
			reader.SwitchStream( ZipStream );
		}
	}

	/// <summary>
	///    Read JSON file header
	/// </summary>
	[MemberNotNull( nameof( DeSerializeFileReader.DS ) )]
	private void ReadJsonFileHeader()
	{
		DeSerializeJsonReader reader = new( FileStream );
		DS = new DeSerializer(
			new DeSerializeMemoryTypeProvider( TypeTable ), new DeSerializeEmptyWriter(), reader );

		_ = reader.ReadUInt16( DeSerializeConstants.FIELD_MAIN_VERSION );
		DeserializeTypeTable( reader );

		ushort dataStart = reader.ReadObjectStart( DeSerializeConstants.FIELD_MAIN_DATA, null );
		if( dataStart != DeSerializeConstants.TYPE_ID_MAIN_DATA )
		{
			throw new DeSerializeException( "Missing main Json data!" );
		}
	}

	/// <summary>
	///    Deserialization type table
	/// </summary>
	/// <param name="reader"></param>
	/// <exception cref="DeSerializeException"></exception>
	private void DeserializeTypeTable( IDeSerializeReader reader )
	{
		ushort shortTypeId = reader.ReadObjectStart( DeSerializeConstants.TYPE_TABLE_FIELD_TABLE, null );
		if( shortTypeId != DeSerializeConstants.TYPE_ID_TYPE_TABLE )
		{
			throw new DeSerializeException(
				$"Unexpected short type ID {shortTypeId} during deserialization of type table" );
		}

		_ = reader.ReadByte( DeSerializeConstants.TYPE_TABLE_FIELD_VERSION );

		int count = reader.ReadCollectionStart( DeSerializeConstants.TYPE_TABLE_FIELD_DATA );
		if( count > 0 )
		{
			for( int i = 0; i < count; i++ )
			{
				shortTypeId = reader.ReadObjectStart( string.Empty, i );
				Guid identifier =
					reader.ReadGuid( DeSerializeConstants.TYPE_TABLE_FIELD_IDENTIFIER );

				ushort? parentId =
					reader.ReadUInt16N( DeSerializeConstants.TYPE_TABLE_FIELD_PARENT_ID );

				ushort version = reader.ReadUInt16( DeSerializeConstants.TYPE_TABLE_FIELD_VERSION );
				string oldTypeName =
					reader.ReadString( DeSerializeConstants.TYPE_TABLE_FIELD_TYPENAME );

				DeSerializeType type = new()
				{
					Id = shortTypeId,
					Identifier = identifier,
					ParentId = parentId,
					Version = version,
					OriginalRuntimeTypeName = oldTypeName
				};

				TypeTable.Add( type );

				reader.ReadObjectEnd( $"#Item[{i}]", type.GetType() );
			}
		}

		reader.ReadCollectionEnd( DeSerializeConstants.TYPE_TABLE_FIELD_DATA, TypeTable.GetType() );
		reader.ReadObjectEnd( DeSerializeConstants.TYPE_TABLE_FIELD_TABLE, TypeTable.GetType() );
	}

	/// <summary>
	///    Release of all resources
	/// </summary>
	public void Dispose()
	{
		DS.Dispose();
		ZipStream?.Dispose();

		FileStream.Flush();
		FileStream.Dispose();
	}
}
