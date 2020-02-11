using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IHubKeyMaker<T>
    {
        Task<T> CreateKey(string accesToken);
    }
}