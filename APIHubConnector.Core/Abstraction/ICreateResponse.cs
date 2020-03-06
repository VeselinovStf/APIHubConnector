using System;
using System.Collections.Generic;
using System.Text;

namespace APIHubConnector.Core.Abstraction
{
    public interface ICreateResponse
    {
        T GetCreatedResponse<T>(string responseMessage);
    }
}
