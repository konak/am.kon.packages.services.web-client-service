using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace am.kon.packages.services.WebClientService
{
    /// <summary>
    /// Service to be used for requests to web endpoints returning stream data.
    /// </summary>
	public class WebClientStreamDataService : WebClientServiceBase<Stream>
    {
        
		/// <summary>
		/// Initializes a new instance of the WebClientStreamDataService class.
		/// </summary>
		/// <param name="logger">The logger instance used for logging.</param>
		/// <param name="configuration">The configuration instance containing application settings.</param>
		/// <param name="clientFactory">The factory for creating HTTP clients.</param>
		public WebClientStreamDataService(
            ILogger<WebClientStreamDataService> logger,
            IConfiguration configuration,
            IHttpClientFactory clientFactory
            ) : base(logger, configuration, clientFactory)
        {
		}

        /// <summary>
        /// Reads the incoming HTTP content as a stream asynchronously.
        /// </summary>
        /// <param name="content">The HTTP content to read the stream data from.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the stream data read from the HTTP content.</returns>
        protected override Task<Stream> ReadDataAsync(HttpContent content)
        {
            return content.ReadAsStreamAsync();
        }
    }
}

