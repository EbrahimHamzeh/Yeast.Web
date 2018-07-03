using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Yeast.Utilities
{
    public sealed class LazySingletonHttpClient
    {
        private static readonly Lazy<HttpClient.HttpClientFactory> _instance =
            new Lazy<HttpClient.HttpClientFactory>(() => new HttpClient.HttpClientFactory(), LazyThreadSafetyMode.ExecutionAndPublication);

        private LazySingletonHttpClient()
        {
        }

        public static HttpClient.HttpClientFactory Instance
        {
            get { return _instance.Value; }
        }
    }
}
