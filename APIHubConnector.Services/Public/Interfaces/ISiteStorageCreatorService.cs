using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIHubConnector.Services.Public.Interfaces
{
    public interface ISiteStorageCreatorService<T>
    {
        Task<T> ExecuteAsync(string hostingAccesToken, string repositoryAccesToken,
            string repositoryName, string projectName, string repositoryClientName,
            string projectCmdCommand, string projectBuildDirName,
            IList<string> filePaths, IList<string> fileContents);
    }
}
