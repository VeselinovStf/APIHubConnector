using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IRepoUserKey
    {
        Task<bool> AddKey(string accesToken, string key, string title);
    }
}