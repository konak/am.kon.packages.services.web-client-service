using System.Collections.Generic;
using System.Net.Http.Headers;

namespace am.kon.packages.services.WebClientService.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="HttpHeaders"/>
    /// </summary>
    public static class HttpHeadersExtensions
    {
        /// <summary>
        /// Copy all available headers from instance of <see cref="HttpHeaders"/> to new instance of <see cref="WebClientResponseHeaders"/>.
        /// </summary>
        /// <param name="headers">Instance of <see cref="HttpHeaders"/> to copy headers from.</param>
        /// <returns>New instance of <see cref="WebClientResponseHeaders"/> with all available headers of instance of <see cref="HttpHeaders"/>.</returns>
        public static WebClientResponseHeaders ToWebClientResponseHeaders(this HttpHeaders headers)
        {
            WebClientResponseHeaders newInstance = new WebClientResponseHeaders();

            return ToWebClientResponseHeaders(headers, newInstance);
        }
        
        /// <summary>
        /// Copy all available headers from instance of <see cref="HttpHeaders"/> to new instance of <see cref="WebClientResponseHeaders"/>.
        /// </summary>
        /// <param name="srcHeaders">Instance of <see cref="HttpHeaders"/> to copy headers from.</param>
        /// <param name="dstHeaders">Instance of <see cref="WebClientResponseHeaders"/> to copy headers to.</param>
        /// <returns>Instance of <see cref="WebClientResponseHeaders"/> with all available headers of instance of <see cref="HttpHeaders"/>.</returns>
        public static WebClientResponseHeaders ToWebClientResponseHeaders(this HttpHeaders srcHeaders, WebClientResponseHeaders dstHeaders)
        {
            foreach (KeyValuePair<string,IEnumerable<string>> header in srcHeaders)
            {
                dstHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }

            return dstHeaders;
        }
    }
}