using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace APIHubConnector.Core.Abstraction
{
    public abstract class BaseHubClient
    {
        protected HttpClient Client { get; }

        public BaseHubClient(HttpClient client)
        {
            Client = client;
        }
    }
}
