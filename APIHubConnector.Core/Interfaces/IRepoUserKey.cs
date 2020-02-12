using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IRepoUserKey<T>
    {
        Task<T> AddKeyAsync(string accesToken, string key, string title);
    }
}