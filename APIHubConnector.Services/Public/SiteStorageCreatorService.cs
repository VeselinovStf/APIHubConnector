using APIHubConnector.Services.Interfaces;
using APIHubConnector.Services.Models;
using APIHubConnector.Services.Public.DTOs;
using APIHubConnector.Services.Public.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIHubConnector.Services.Public
{
    public class SiteStorageCreatorService : ISiteStorageCreatorService<SiteStorageCreatorResultDTO>
    {
        private readonly INetlifyApiClientService<BaseResponse> _hostingService;
        private readonly IGitLabAPIClientService<BaseResponse> _repoService;



        public SiteStorageCreatorService(
            INetlifyApiClientService<BaseResponse> hostingService,
            IGitLabAPIClientService<BaseResponse> repoService)


        {
            this._hostingService = hostingService;
            this._repoService = repoService;

        }

        public async Task<SiteStorageCreatorResultDTO> ExecuteAsync(
            string hostingAccesToken, string repositoryAccesToken,
            string repositoryName, string projectName, string repositoryClientName,
            string projectCmdCommand, string projectBuildDirName,
            IList<string> filePaths, IList<string> fileContents)
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
                          
                            //4- Push all files to repository
                            var pushToRepo = await this._repoService.PushDataToHubAsync(repositoryId, repositoryAccesToken, filePaths, fileContents);

                            if (pushToRepo.Success)
                            {
                                //5- update Hosting repository name 
                                var pushRepositoryName = repositoryClientName + "/" + repositoryName;

                                //6- deploy project try gitlab to netlify
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
