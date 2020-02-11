using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IRepoHubKeyMaker
    {
        Task<bool> CreateKey(string accesToken, string key, string title);
    }
}