using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IAPIRepoClientService<T>
    {
        Task<T> CreateHubAsync(string name, string accesTokken);

  
    }


}