using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIHubConnector.Core.Abstraction
{
    public interface IRepositoryHubClient
    {
        Task<string> PostCreateAsync(string newHubName, string credidentials);

        Task<bool> AddRepoKey(string accesToken, string key, string title);

        Task<bool> InitialPushToHubAsync(string hubId, string accesTokken, IList<string> filePaths, IList<string> fileContents);
    }
}
