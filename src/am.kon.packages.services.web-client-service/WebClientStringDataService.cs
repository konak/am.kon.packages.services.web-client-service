using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Xml.Linq;

namespace am.kon.packages.services.WebClientService
{
    /// <summary>
    /// Service to be used for requests to web endpoints returning string data (HTML pages, JSON objects, XML documents, etc.).
    /// </summary>
	public class WebClientStringDataService : WebClientServiceBase<string>
    {
		public WebClientStringDataService(
            ILogger<WebClientStringDataService> logger,
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

