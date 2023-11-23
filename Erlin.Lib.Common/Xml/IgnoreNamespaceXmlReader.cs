using System.Xml;

namespace Erlin.Lib.Common.Xml;

/// <summary>
///    Xml reader that allows read xml without checking the namespace
/// </summary>
public class IgnoreNamespaceXmlReader : XmlTextReader
{
	public IgnoreNamespaceXmlReader( TextReader reader ) : base( reader )
	{
	}

	/// <summary>
	///    Gets the namespace URI (as defined in the W3C Namespace specification) of the node on
	///    which the reader is positioned.
	/// </summary>
	/// <returns>The namespace URI of the current node; otherwise an empty string.</returns>
	public override string NamespaceURI
	{
		get { return string.Empty; }
	}
}
