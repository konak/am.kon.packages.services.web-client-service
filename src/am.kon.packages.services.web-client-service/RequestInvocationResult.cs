using System;
namespace am.kon.packages.services.WebClientService
{
    /// <summary>
    /// Object describing the result of an HTTP invocation.
    /// </summary>
	public class RequestInvocationResult<TData>
	{
        /// <summary>
        /// Result of the invocation.
        /// </summary>
        public RequestInvocationResultTypes Result { get; set; }

        /// <summary>
        /// Data returned by the invocation process.
        /// In case of an error during invocation, this property will contain the error message and additional data if available.
        /// </summary>
        public TData Data { get; set; }

        /// <summary>
        /// Message describing the request invocation result.
        /// </summary>
        /// <remarks>
        /// If the request has completed successfully, Message will be an empty string.
        /// In case of an error during communication with the server,
        /// or if the server returns an error code, Message will contain data about that error.
        /// </remarks>
        public string Message { get; set; }
        
        /// <summary>
        /// Collection of headers returned from the server.
        /// </summary>
        public WebClientResponseHeaders Headers { get; set; }

        public RequestInvocationResult()
        {
            Result = RequestInvocationResultTypes.Unknown;
            Data = default(TData);
            Message = string.Empty;
            Headers = new WebClientResponseHeaders();
        }
    }
}

