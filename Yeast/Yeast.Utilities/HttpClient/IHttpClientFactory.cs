using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Yeast.Utilities.HttpClient
{
    public interface IHttpClientFactory : IDisposable
    {
        System.Net.Http.HttpClient GetOrCreate(
            Uri baseAddress,
            IDictionary<string, string> defaultRequestHeaders = null,
            TimeSpan? timeout = null,
            long? maxResponseContentBufferSize = null,
            HttpMessageHandler handler = null);
    }
}
