using System;
namespace am.kon.packages.services.WebClientService
{
    /// <summary>
    /// Object describing the result of http invocation
    /// </summary>
	public class RequestInvocationResult<TData>
	{
        /// <summary>
        /// Result of invocation
        /// </summary>
        public RequestInvocationResultTypes Result { get; set; }

        /// <summary>
        /// Data returned by invocation process.
        /// In case of error during invocation this property will contain error message and additional data if there is some.
        /// </summary>
        public TData Data { get; set; }

        /// <summary>
        /// Message describing request invocation result.
        /// </summary>
        /// <remarks>
        /// If request has completed successfuly Message will be ampty string.
        /// In case if some error will happen during communication with the server,
        /// or server will return an error code, Message will contain data about that error.
        /// </remarks>
        public string Message { get; set; }

        public RequestInvocationResult()
        {
            Result = RequestInvocationResultTypes.Unknown;
            Data = default(TData);
            Message = string.Empty;
        }
    }
}

