using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using am.kon.packages.services.WebClientService.Constants;
using am.kon.packages.services.WebClientService.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace am.kon.packages.services.WebClientService
{
    /// <summary>
    /// Abstract base class for services to be used for requests to web endpoints.
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
        /// Start the service.
        /// </summary>
        /// <returns>A task representing the start operation.</returns>
        public Task Start()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stop the service.
        /// </summary>
        /// <returns>A task representing the stop operation.</returns>
        public Task Stop()
        {
            _cancellationTokenSource.Cancel();

            return Task.CompletedTask;
        }

        /// <summary>
        /// Invoke an HTTP request and return the resulting data.
        /// </summary>
        /// <param name="requestUri">The URI to make the request to.</param>
        /// <param name="httpMethod">The HTTP request method to be used during invocation. Default is GET.</param>
        /// <param name="dataToSend">The data to be sent in the request.</param>
        /// <param name="mediaType">The media type of the request content. Default is application/json.</param>
        /// <param name="bearerToken">The bearer token for authorization.</param>
        /// <param name="httpClientName">The name of the configured client to be used for the HTTP request invocation.</param>
        /// <param name="encoding">The encoding of the request content. Default is UTF-8.</param>
        /// <param name="acceptEncodings">List of accepted encodings.</param>
        /// <returns>An object describing the invocation result.</returns>
        public async Task<RequestInvocationResult<TData>> InvokeRequest(Uri requestUri, HttpMethod httpMethod = null, string dataToSend = null, string mediaType = HttpContentMediaTypesConstants.ApplicationJson, string bearerToken = null, string httpClientName = HttpClientNames.Default, Encoding encoding = null, string[] acceptEncodings = null)
        {
            if (requestUri == null)
                throw new ArgumentNullException(nameof(requestUri));
            
            if (httpMethod == null)
                httpMethod = HttpMethod.Get;

            RequestInvocationResult<TData> result = new RequestInvocationResult<TData>();

            try
            {
                using (StringContent requestContent = new StringContent(dataToSend ?? string.Empty, encoding, mediaType))
                {
                    using (HttpRequestMessage requestMessage = new HttpRequestMessage(httpMethod, requestUri))
                    {
                        if (dataToSend != null)
                            requestMessage.Content = requestContent;

                        if (bearerToken != null)
                            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(HttpAuthenticationScheme.Bearer, bearerToken);

                        if (acceptEncodings != null)
                            foreach (string acceptEncoding in acceptEncodings)
                                requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue(acceptEncoding));

                        using (HttpClient client = _clientFactory.CreateClient(httpClientName))
                        {
                            using (HttpResponseMessage resp = await client.SendAsync(requestMessage, _cancellationToken))
                            {
                                if (resp.IsSuccessStatusCode)
                                {
                                    result.Data = await ReadDataAsync(resp.Content);
                                    result.Result = RequestInvocationResultTypes.Ok;
                                }
                                else
                                {
                                    result.Result = RequestInvocationResultTypes.ResponseError;
                                    result.Message = $"Status code: {resp.StatusCode} Page Data: {await resp.Content.ReadAsStringAsync()}";
                                }
                                
                                resp.Headers.ToWebClientResponseHeaders(result.Headers);
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

        /// <summary>
        /// Reads data from the HTTP content.
        /// </summary>
        /// <param name="content">The HTTP content.</param>
        /// <returns>The data read from the content.</returns>
        protected abstract Task<TData> ReadDataAsync(HttpContent content);
    }
}

