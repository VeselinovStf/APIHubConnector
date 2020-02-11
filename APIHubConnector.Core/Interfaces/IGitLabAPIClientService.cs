using APIHUbConnector.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHubConnector.Core.Interfaces
{
    public interface IGitLabAPIClientService<T> : IAPIRepoClientService<T>, IRepoUserKey<T>
    {
    }
}
