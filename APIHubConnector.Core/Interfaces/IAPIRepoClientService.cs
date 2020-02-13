using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IAPIRepoClientService<T>
    {
        Task<T> CreateHubAsync(string name, string accesTokken);

        Task<T> PushDataToHubAsync(string hubId, string accesTokken, IList<string> filePaths, IList<string> fileContents);
    }


}