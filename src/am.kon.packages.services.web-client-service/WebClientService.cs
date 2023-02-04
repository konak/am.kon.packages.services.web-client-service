using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace am.kon.packages.services.WebClientService
{
    /// <summary>
    /// Service to be used for requests to web endpoints
    /// </summary>
	public class WebClientService : WebClientServiceBase<string>
    {
		public WebClientService(
            ILogger<WebClientService> logger,
            IConfiguration configuration,
            IHttpClientFactory clientFactory
            ) : base(logger, configuration, clientFactory)
        {
		}

        protected override Task<string> ReadDataAsync(HttpContent content)
        {
            return content.ReadAsStringAsync();
        }
    }
}

