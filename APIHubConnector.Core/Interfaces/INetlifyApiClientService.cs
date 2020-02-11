using APIHubConnector.Core.Models;
using APIHUbConnector.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHubConnector.Core.Interfaces
{
    public interface INetlifyApiClientService<T> : IAPIHostClientService<T> , IHostDeployToken<T>   
    {
    }
}
