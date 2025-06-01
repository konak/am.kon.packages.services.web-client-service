using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace am.kon.packages.services.WebClientService
{
    /// <remarks>
    /// This service specializes in handling text-based responses from web endpoints,
    /// converting the HTTP content directly into string format for further processing.
    /// </remarks>
    	public class WebClientStringDataService : WebClientServiceBase<string>
    {
        /// <summary>
        /// Initializes a new instance of the WebClientStringDataService class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="configuration">The configuration instance containing application settings.</param>
        /// <param name="clientFactory">The factory for creating HTTP clients.</param>
		public WebClientStringDataService(
            ILogger<WebClientStringDataService> logger,
            IConfiguration configuration,
            IHttpClientFactory clientFactory
            ) : base(logger, configuration, clientFactory)
        {
		}

        /// <summary>
        /// Reads the incoming HTTP content as a string asynchronously.
        /// </summary>
        /// <param name="content">The HTTP content to read the string data from.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the string data read from the HTTP content.</returns>
        protected override Task<string> ReadDataAsync(HttpContent content)
        {
            return content.ReadAsStringAsync();
        }
    }
}

