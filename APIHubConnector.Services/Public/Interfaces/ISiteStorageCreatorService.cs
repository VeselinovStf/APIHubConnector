using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIHubConnector.Services.Public.Interfaces
{
    public interface ISiteStorageCreatorService<T>
    {
        Task<T> ExecuteAsync(string hostingAccesToken, string repositoryAccesToken,
            string repositoryName, string projectName, string repositoryClientName,
            string projectCmdCommand, string projectBuildDirName, string localPathToProjectTemplate);
    }
}
