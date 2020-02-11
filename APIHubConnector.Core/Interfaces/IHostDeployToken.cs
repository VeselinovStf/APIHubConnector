using APIHubConnector.Core.Models;
using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IHostDeployToken<T> 
    {
        Task<T> CreateDeployKey(string accesToken);
    }
}