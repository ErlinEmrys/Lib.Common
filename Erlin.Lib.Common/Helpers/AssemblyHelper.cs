using System.Reflection;

namespace Erlin.Lib.Common;

/// <summary>
///    Helper class for working with assemblies
/// </summary>
public static class AssemblyHelper
{
	/// <summary>
	///    Return current Lib.Common assembly
	/// </summary>
	public static Assembly CommonBaseAssembly { get; } = Assembly.GetExecutingAssembly();

	/// <summary>
	///    Path to location of this base assembly
	/// </summary>
	public static string BaseLocation { get; } = Path.GetDirectoryName( AssemblyHelper.CommonBaseAssembly.Location ) ?? throw new InvalidOperationException();
}
