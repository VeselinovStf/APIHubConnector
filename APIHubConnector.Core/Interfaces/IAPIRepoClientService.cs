using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IAPIRepoClientService<T>
    {
        Task<string> CreateHubAsync(string name, string accesTokken);

  
    }


}