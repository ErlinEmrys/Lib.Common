namespace System.Net.Http;

/// <summary>
///    Simple HTTP multiple retry handler
/// </summary>
public class HttpRetryHandler
(
	int _maxRetries,
	HttpMessageHandler innerHandler
)
	: DelegatingHandler( innerHandler )
{
	/// <summary>
	///    Retry implementation
	/// </summary>
	protected override async Task< HttpResponseMessage > SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
	{
		HttpResponseMessage? response = null;
		for( int i = 0; i < _maxRetries; i++ )
		{
			response = await base.SendAsync( request, cancellationToken );
			if( response.IsSuccessStatusCode )
			{
				return response;
			}
		}

		ArgumentNullException.ThrowIfNull( response );

		return response;
	}
}
