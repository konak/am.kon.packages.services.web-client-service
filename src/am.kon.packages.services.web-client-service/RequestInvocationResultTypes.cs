using System;
namespace am.kon.packages.services.WebClientService
{
    /// <summary>
    /// Enum describing result type of HTTP request invocation.
    /// </summary>
	public enum RequestInvocationResultTypes
	{
        /// <summary>
        /// Unknown result type.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Invocation completed successful.
        /// </summary>
        Ok = 1,

        /// <summary>
        /// There was an error during the invocation process.
        /// </summary>
        ResponseError = 2,

        /// <summary>
        /// There was a communication issue during the invocation process.
        /// </summary>
        ConnectionError = 3
    }
}

