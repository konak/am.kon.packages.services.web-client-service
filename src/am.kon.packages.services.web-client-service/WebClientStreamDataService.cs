using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace am.kon.packages.services.WebClientService
{
    /// <summary>
    /// Service to be used for requests to web endpoints returning stream data
    /// </summary>
	public class WebClientStreamDataService : WebClientServiceBase<Stream>
    {
		public WebClientStreamDataService(
            ILogger<WebClientStreamDataService> logger,
            IConfiguration configuration,
            IHttpClientFactory clientFactory
            ) : base(logger, configuration, clientFactory)
        {
		}

        protected override Task<Stream> ReadDataAsync(HttpContent content)
        {
            return content.ReadAsStreamAsync();
        }
    }
}

