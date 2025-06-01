using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace am.kon.packages.services.WebClientService
{
    /// <summary>
    /// A service class for making HTTP requests and processing responses as byte arrays.
    /// Inherits from WebClientServiceBase with a byte array as the data type.
    /// </summary>
    public class WebClientByteArrayDataService : WebClientServiceBase<byte[]>
    {
        
        /// <summary>
        /// Initializes a new instance of the WebClientByteArrayDataService class.
        /// </summary>
        /// <param name="logger">The logger instance used for logging.</param>
        /// <param name="configuration">The configuration instance containing application settings.</param>
        /// <param name="clientFactory">The factory for creating HTTP clients.</param>
        public WebClientByteArrayDataService(
            ILogger<WebClientByteArrayDataService> logger,
            IConfiguration configuration,
            IHttpClientFactory clientFactory
        ) : base(logger, configuration, clientFactory)
        {
            
        }

        protected override Task<byte[]> ReadDataAsync(HttpContent content)
        {
            return content.ReadAsByteArrayAsync();
        }
    }
}