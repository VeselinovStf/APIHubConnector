using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace APIHubConnector.Core.Abstraction
{
    public interface IHttpContextCreateor
    {
        HttpContent CreateHttpContent<T>(T content);
    }
}
