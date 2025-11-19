using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;

using Erlin.Lib.Common.DeSerialization.ReadWrite;

namespace Erlin.Lib.Common.DeSerialization;

/// <summary>
///    File writer for DeSerialization
/// </summary>
public class DeSerializeFileWriter : IDisposable
{
	/// <summary>
	///    Position of address pointer for Type table position in stream
	/// </summary>
	private long BinaryTypeTablePosition { get; set; }

	/// <summary>
	///    Underlying file stream
	/// </summary>
	private FileStream FileStream { get; }

	/// <summary>
	///    Underlying ZIP compression stream
	/// </summary>
	private GZipStream? ZipStream { get; set; }

	/// <summary>
	///    Underlying writer
	/// </summary>
	private IDeSerializeWriter Writer { get; set; }

	/// <summary>
	///    Path to file that we are writing
	/// </summary>
	private string FilePath { get; }

	/// <summary>
	///    Path to temporary file that we are writing
	/// </summary>
	private string TmpFilePath { get; }

	/// <summary>
	///    Whether the writing process was successful
	/// </summary>
	private bool IsSuccess { get; set; }

	/// <summary>
	///    DeSerialization option
	/// </summary>
	private DeSerializeOptions Options { get; }

	/// <summary>
	///    Main serializer
	/// </summary>
	public IDeSerializer DS { get; }

	/// <summary>
	///    Type table
	/// </summary>
	private List< DeSerializeType > TypeTable { get; } = [ ];

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="filePath">File path to write</param>
	public DeSerializeFileWriter( string filePath ) : this( filePath, new DeSerializeOptions() )
	{
	}

	/// <summary>
	///    Ctor
	/// </summary>
	/// <param name="filePath">File path to write</param>
	/// <param name="options">Options of DeSerialization</param>
	public DeSerializeFileWriter( string filePath, DeSerializeOptions options )
	{
		Options = options;
		FilePath = filePath;

		FileSystemHelper.DirectoryEnsure( FilePath );

		TmpFilePath = FilePath + Options.TempFileExtension;
		if( File.Exists( TmpFilePath ) )
		{
			File.Delete( TmpFilePath );
		}

		FileStream = File.Open( TmpFilePath, FileMode.Create, FileAccess.Write, FileShare.None );

		WriteFileHeader();

		DS = new DeSerializer( new DeSerializeMemoryTypeProvider( TypeTable ), Writer, new DeSerializeEmptyReader() );
	}

	/// <summary>
	///    Writes file header
	/// </summary>
	[ MemberNotNull( nameof( DeSerializeFileWriter.Writer ) ) ]
	private void WriteFileHeader()
	{
		if( Options.IsBinaryFormat )
		{
			WriteBinaryFileHeader();
		}
		else
		{
			WriteJsonFileHeader();
		}
	}

	/// <summary>
	///    Writes binary file header
	/// </summary>
	[ MemberNotNull( nameof( DeSerializeFileWriter.Writer ) ) ]
	private void WriteBinaryFileHeader()
	{
		FileStream.WriteByte( DeSerializeConstants.FILE_HEADER_BINARY );
		FileStream.WriteByte( 0 ); // Version
		FileStream.WriteByte( 0 ); // Version
		FileStream.WriteByte( ( byte )( Options.BinaryZipCompress ? 1 : 0 ) ); //Compress flag
		FileStream.Flush();

		BinaryTypeTablePosition = FileStream.Position;
		byte[] typeTablePointer = new byte[8];
		FileStream.Write( typeTablePointer, 0, typeTablePointer.Length ); // Empty pointer to Type Table

		Stream toWrite = FileStream;
		if( Options.BinaryZipCompress )
		{
			toWrite = ZipStream = new GZipStream( FileStream, CompressionLevel.Optimal, true );
		}

		Writer = new DeSerializeBinaryWriter( toWrite );
	}

	/// <summary>
	///    Writes JSON file header
	/// </summary>
	[ MemberNotNull( nameof( DeSerializeFileWriter.Writer ) ) ]
	private void WriteJsonFileHeader()
	{
		FileStream.WriteByte( DeSerializeConstants.FILE_HEADER_JSON );
		DeSerializeJsonTextWriter writer = new( FileStream, Options );
		if( Options.JsonFormatted )
		{
			writer.WriteNewLine();
		}

		Writer = writer;
		Writer.WriteUInt16( DeSerializeConstants.FIELD_MAIN_VERSION, 0 );
		Writer.WriteObjectStart( DeSerializeConstants.FIELD_MAIN_DATA, DeSerializeConstants.TYPE_ID_MAIN_DATA );
	}

	/// <summary>
	///    Serialization of Type table
	/// </summary>
	public void WriteTypeTable()
	{
		if( Options.IsBinaryFormat )
		{
			WriteBinaryTypeTable();
		}
		else
		{
			WriteJsonTypeTable();
		}
	}

	/// <summary>
	///    Binary serialization of Type table
	/// </summary>
	private void WriteBinaryTypeTable()
	{
		if( Options.BinaryZipCompress )
		{
			Writer.SwitchStream( FileStream );
			ZipStream?.Dispose();
			ZipStream = new GZipStream( FileStream, CompressionLevel.Optimal, true );
			Writer.SwitchStream( ZipStream );
		}

		long typeTablePosition = FileStream.Position;
		SerializeTypeTable();

		if( Options.BinaryZipCompress )
		{
			Writer.SwitchStream( FileStream );
			ZipStream?.Dispose();
		}

		byte[] typeTablePointer = BitConverter.GetBytes( typeTablePosition );
		FileStream.Seek( BinaryTypeTablePosition, SeekOrigin.Begin );
		FileStream.Write( typeTablePointer, 0, typeTablePointer.Length ); // Pointer to Type Table
	}

	/// <summary>
	///    JSON serialization of Type table
	/// </summary>
	private void WriteJsonTypeTable()
	{
		Writer.WriteObjectEnd(); // MAIN_DATA_FIELD_NAME

		SerializeTypeTable();

		if( Options.JsonFormatted )
		{
			( ( DeSerializeJsonTextWriter )Writer ).WriteNewLine();
		}

		Writer.Flush();
		FileStream.WriteByte( DeSerializeConstants.FILE_CLOSURE_JSON );
	}

	/// <summary>
	///    Serialization of Type table
	/// </summary>
	private void SerializeTypeTable()
	{
		Writer.WriteObjectStart( DeSerializeConstants.TYPE_TABLE_FIELD_TABLE, DeSerializeConstants.TYPE_ID_TYPE_TABLE );

		Writer.WriteByte( DeSerializeConstants.TYPE_TABLE_FIELD_VERSION, 0 );
		Writer.WriteCollectionStart( DeSerializeConstants.TYPE_TABLE_FIELD_DATA, TypeTable.Count );

		foreach( DeSerializeType fType in TypeTable )
		{
			Writer.WriteObjectStart( null, fType.ShortId );
			Writer.WriteGuid( DeSerializeConstants.TYPE_TABLE_FIELD_IDENTIFIER, fType.Identifier );
			Writer.WriteUInt16N( DeSerializeConstants.TYPE_TABLE_FIELD_PARENT_ID, fType.ParentId );
			Writer.WriteUInt16( DeSerializeConstants.TYPE_TABLE_FIELD_VERSION, fType.Version );
			Writer.WriteString( DeSerializeConstants.TYPE_TABLE_FIELD_TYPENAME, fType.OriginalRuntimeTypeName );
			Writer.WriteObjectEnd();
		}

		Writer.WriteCollectionEnd();
		Writer.WriteObjectEnd();
	}

	/// <summary>
	///    All writing was success
	/// </summary>
	public void Success()
	{
		WriteTypeTable();
		IsSuccess = true;
		DS.Dispose();
	}

	/// <summary>
	///    Release of all resources
	/// </summary>
	public void Dispose()
	{
		FileStream.Flush();
		FileStream.Dispose();

		if( IsSuccess )
		{
			File.Move( TmpFilePath, FilePath, true );
			File.Delete( TmpFilePath );
		}
	}
}
