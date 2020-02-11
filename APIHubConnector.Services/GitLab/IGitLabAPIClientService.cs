using APIHUbConnector.Core.Interfaces;

namespace APIHubConnector.Services.Interfaces
{
    public interface IGitLabAPIClientService<T> : IAPIRepoClientService<T>, IRepoUserKey<T>
    {
    }
}
