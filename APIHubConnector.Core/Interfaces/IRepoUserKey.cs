using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IRepoUserKey<T>
    {
        Task<T> AddKey(string accesToken, string key, string title);
    }
}