using APIHUbConnector.Core.Interfaces;

namespace APIHubConnector.Services.Interfaces
{
    public interface INetlifyApiClientService<T> : IAPIHostClientService<T>, IHostDeployToken<T>
    {
    }
}
