using APIHubConnector.Services.Interfaces;
using APIHubConnector.Services.Models;
using APIHubConnector.Services.Public.DTOs;
using APIHubConnector.Services.Public.Interfaces;
using APIHUbConnector.Services.FileTransfer;
using APIHUbConnector.Services.FileTransfer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIHubConnector.Services.Public
{
    public class SiteStorageCreatorService : ISiteStorageCreatorService<SiteStorageCreatorResultDTO>
    {
        private readonly INetlifyApiClientService<BaseResponse> _hostingService;
        private readonly IGitLabAPIClientService<BaseResponse> _repoService;
        private readonly IFileTransferrer<FileTransfererResult> _fileTransferrer;
     

        public SiteStorageCreatorService(
            INetlifyApiClientService<BaseResponse> hostingService,
            IGitLabAPIClientService<BaseResponse> repoService,
            IFileTransferrer<FileTransfererResult> fileTransferrer)
          
        {
            this._hostingService = hostingService;
            this._repoService = repoService;
            this._fileTransferrer = fileTransferrer;           
        }

        public async Task<SiteStorageCreatorResultDTO> ExecuteAsync(
            string hostingAccesToken, string repositoryAccesToken,
            string repositoryName, string projectName, string repositoryClientName,
            string projectCmdCommand, string projectBuildDirName, string localPathToProjectTemplate)
        {
            var result = new SiteStorageCreatorResultDTO();

            // Add custom business validations here

            try
            {
                var hostingDeployKey = await this._hostingService.CreateDeployKey(hostingAccesToken);

                var hostingKey = hostingDeployKey.Message[1];
                var hostingKeyId = hostingDeployKey.Message[0];

                if (hostingDeployKey.Success)
                {
                    //2- Create repo and get id
                    var createRepoHubId = await this._repoService.CreateHubAsync(repositoryName, repositoryAccesToken);
                    var repositoryId = createRepoHubId.Message[0];

                    if (createRepoHubId.Success)
                    {
                        //3- add deploy key to repository
                        var repoUserKey = await this._repoService.AddKeyAsync(repositoryAccesToken, hostingKey, projectName);

                        if (repoUserKey.Success)
                        {
                            //4- get project files
                            var filePaths = new List<string>();
                            var fileContents = new List<string>();

                            var defaultStoreTypeSiteFileRead = await this._fileTransferrer.FilesToListAsync(localPathToProjectTemplate);

                            filePaths = new List<string>(defaultStoreTypeSiteFileRead.Results.Select(p => p.FilePath));
                            fileContents = new List<string>(defaultStoreTypeSiteFileRead.Results.Select(p => p.FileContent));

                            //5- Push all files to repository
                            var pushToRepo = await this._repoService.PushDataToHubAsync(repositoryId, repositoryAccesToken, filePaths, fileContents);

                            if (pushToRepo.Success)
                            {
                                //6- update Hosting repository name 
                                var pushRepositoryName = repositoryClientName + "/" + repositoryName;

                                //7- deploy project try gitlab to netlify
                                var deployCall = await this._hostingService.CreateHubAsync(
                                    projectName, pushRepositoryName, repositoryId, hostingKeyId, hostingAccesToken,
                                    projectCmdCommand, projectBuildDirName);

                                if (deployCall.Success)
                                {
                                    result.Success = true;
                                    result.Message.Add(projectName);
                                }
                                else
                                {
                                    result.Success = false;
                                    result.Message.Add(deployCall.Message[0]);
                                }
                            }
                            else
                            {
                                result.Success = false;
                                result.Message.Add(pushToRepo.Message[0]);
                            }
                        }
                        else
                        {
                            result.Success = false;
                            result.Message.Add(repoUserKey.Message[0]);
                        }

                    }
                    else
                    {
                        result.Success = false;
                        result.Message.Add(createRepoHubId.Message[0]);
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message.Add(hostingDeployKey.Message[0]);
                }
                
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message.Add(ex.Message);
            }

            return result;

        }
    }
}
