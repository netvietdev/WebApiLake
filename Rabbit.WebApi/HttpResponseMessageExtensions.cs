using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Rabbit.WebApi
{
    public static class HttpResponseMessageExtensions
    {
        /// <summary>
        /// Add or update headers of HttpResponseMessage, use ToString to convert Object to String
        /// </summary>
        public static HttpResponseMessage AddOrUpdateHeaders(this HttpResponseMessage responseMessage, IDictionary<string, object> headers)
        {
            return responseMessage.AddOrUpdateHeaders(headers.ToDictionary(x => x.Key, x => x.Value.ToString()));
        }

        public static HttpResponseMessage AddOrUpdateHeaders(this HttpResponseMessage responseMessage, IDictionary<string, string> headers)
        {
            foreach (var header in headers)
            {
                if (responseMessage.Headers.Contains(header.Key))
                {
                    responseMessage.Headers.Remove(header.Key);
                }
                responseMessage.Headers.Add(header.Key, header.Value);
            }

            return responseMessage;
        }
    }
}
