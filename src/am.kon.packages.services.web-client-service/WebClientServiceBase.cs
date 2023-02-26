using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using am.kon.packages.services.WebClientService.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace am.kon.packages.services.WebClientService
{
    /// <summary>
    /// Abstract base class for services to be used for requests to web endpoints
    /// </summary>
	public abstract class WebClientServiceBase<TData>
	{
        protected readonly ILogger<WebClientServiceBase<TData>> _logger;
        private readonly IConfiguration _configuration;

        private readonly IHttpClientFactory _clientFactory;

        private readonly CancellationTokenSource _cancellationTokenSource;
        protected readonly CancellationToken _cancellationToken;

        public WebClientServiceBase(
            ILogger<WebClientServiceBase<TData>> logger,
            IConfiguration configuration,
            IHttpClientFactory clientFactory
            )
        {
            _logger = logger;
            _configuration = configuration;

            _clientFactory = clientFactory;

            _cancellationTokenSource = new CancellationTokenSource();
            _cancellationToken = _cancellationTokenSource.Token;
        }

        /// <summary>
        /// Start service
        /// </summary>
        /// <returns></returns>
        public Task Start()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stop Service
        /// </summary>
        /// <returns></returns>
        public Task Stop()
        {
            _cancellationTokenSource.Cancel();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Invoke http request and return resulting data
        /// </summary>
        /// <param name="requestUri">URI to make request to</param>
        /// <param name="httpMethod">Http request method to be used during invocation</param>
        /// <param name="dataToSend">data to be send in request</param>
        /// <param name="httpClientName">Name of the configured client to be used for http request invocation</param>
        /// <returns>Object describing invocation result</returns>
        public async Task<RequestInvocationResult<TData>> InvokeRequest(Uri requestUri, HttpMethod httpMethod = null, string dataToSend = null, string metiaType = HttpContentMediaTypes.ApplicationJson, string bearerToken = null, string httpClientName = HttpClientNames.Default, Encoding encoding = null)
        {
            if (httpMethod == null)
                httpMethod = HttpMethod.Get;

            if (encoding == null)
                encoding = Encoding.UTF8;

            RequestInvocationResult<TData> result = new RequestInvocationResult<TData>();

            try
            {
                using (StringContent requestContent = new StringContent(dataToSend == null ? string.Empty : dataToSend, encoding, metiaType))
                {
                    using (HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, requestUri))
                    {
                        if (dataToSend != null)
                            requestMessage.Content = requestContent;

                        if (bearerToken != null)
                            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(HttpAuthenticationScheme.Bearer, bearerToken);

                        using (HttpClient client = _clientFactory.CreateClient(httpClientName))
                        {
                            using (HttpResponseMessage resp = await client.SendAsync(requestMessage, _cancellationToken))
                            {
                                if (resp.IsSuccessStatusCode)
                                {
                                    //result.Data = await resp.Content.ReadAsStringAsync();
                                    result.Data = await ReadDataAsync(resp.Content);
                                    result.Result = RequestInvocationResultTypes.Ok;
                                }
                                else
                                {
                                    result.Result = RequestInvocationResultTypes.ResponseError;
                                    result.Message = $"Status code: {resp.StatusCode} Page Data: {await resp.Content.ReadAsStringAsync()}";
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Result = RequestInvocationResultTypes.ConnectionError;
                result.Message = ex.ToString() + "\r\nHttp client: " + httpClientName;
            }

            return result;
        }

        protected abstract Task<TData> ReadDataAsync(HttpContent content);
    }
}

