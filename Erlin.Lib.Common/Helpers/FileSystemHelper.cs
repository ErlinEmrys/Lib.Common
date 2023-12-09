using System.Text;

namespace Erlin.Lib.Common;

/// <summary>
///    Helper for file system
/// </summary>
public static class FileSystemHelper
{
	/// <summary>
	///    Ensure that directory exist, if not, create it
	/// </summary>
	/// <param name="path">Directory path to ensure</param>
	/// <returns>Ensured directory path</returns>
	public static string DirectoryEnsure( string path )
	{
		if( !FileSystemHelper.CheckIfPathIsDirectory( path ) )
		{
			path = FileSystemHelper.GetDirectoryPath( path );
		}

		if( !Directory.Exists( path ) )
		{
			Directory.CreateDirectory( path );
		}

		return path;
	}

	/// <summary>
	///    Returns true if path is pointing only to directory, False if its file
	/// </summary>
	/// <param name="path">Path</param>
	/// <returns>True - is directory only</returns>
	public static bool CheckIfPathIsDirectory( string? path )
	{
		return Path.GetExtension( path ).IsEmpty();
	}

	/// <summary>
	///    Returns directory path from file path
	/// </summary>
	/// <param name="filePath">File path</param>
	/// <returns>Directory path</returns>
	public static string GetDirectoryPath( string filePath )
	{
		FileInfo info = new( filePath );
		return info.Directory?.FullName
			?? throw new InvalidOperationException( $"Could not get directory from file path: {filePath}" );
	}

	/// <summary>
	///    Write all text to single file
	/// </summary>
	/// <param name="path">Path to file</param>
	/// <param name="contents">Content to be written</param>
	public static Task WriteAllText( string path, string contents )
	{
		return FileSystemHelper.WriteAllText( path, contents, Encoding.UTF8 );
	}

	/// <summary>
	///    Write all text to single file
	/// </summary>
	/// <param name="path">Path to file</param>
	/// <param name="contents">Content to be written</param>
	/// <param name="encoding">Encoding of file</param>
	public static Task WriteAllText( string path, string contents, Encoding encoding )
	{
		FileSystemHelper.DirectoryEnsure( path );
		return File.WriteAllTextAsync( path, contents, encoding );
	}

	/// <summary>
	///    Write all binary data to single file
	/// </summary>
	/// <param name="path">Path to file</param>
	/// <param name="content">Content to be written</param>
	public static Task WriteAllBytes( string path, byte[] content )
	{
		FileSystemHelper.DirectoryEnsure( path );
		return File.WriteAllBytesAsync( path, content );
	}

	/// <summary>
	///    Deletes the specified file if exist
	/// </summary>
	/// <param name="filePath">Path to file</param>
	/// <param name="cancellationToken"></param>
	public static async Task FileDelete( string filePath, CancellationToken cancellationToken = default )
	{
		FileInfo info = new( filePath );
		if( info.Exists )
		{
			info.Delete();
			info.Refresh();
			while( info.Exists && !cancellationToken.IsCancellationRequested )
			{
				await Task.Delay( 10, cancellationToken );
				info.Refresh();
			}
		}
	}

	/// <summary>
	///    Deletes the specified directory if exist (with all under)
	/// </summary>
	/// <param name="directoryPath">Path to directory</param>
	/// <param name="cancellationToken"></param>
	public static async Task DirectoryDelete(
		string directoryPath, CancellationToken cancellationToken = default )
	{
		DirectoryInfo info = new( directoryPath );
		if( info.Exists )
		{
			info.Delete( true );
			info.Refresh();
			while( info.Exists && !cancellationToken.IsCancellationRequested )
			{
				await Task.Delay( 10, cancellationToken );
				info.Refresh();
			}
		}
	}
}
