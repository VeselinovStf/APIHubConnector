using APIHubConnector.Services.Models;
using APIHUbConnector.Core.Clients;
using APIHUbConnector.Core.DTOs;
using APIHUbConnector.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIHUbConnector.Services.GitLab
{
    public class GitLabAPIClientService :
        IAPIRepoClientService<BaseResponse>,
        IRepoUserKey<BaseResponse>
    {
        private readonly GitLabHubClient client;

        public GitLabAPIClientService(
            GitLabHubClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<BaseResponse> AddKey(string accesToken, string key, string title)
        {
            try
            {
                var result = await this.client.AddRepoKey(accesToken, key, title);

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {

                return new BaseResponse(false, ex.Message);
            }
            
        }

        public async Task<BaseResponse> CreateHubAsync(string name, string accesTokken)
        {
           

            try
            {
                var result =  await client.PostCreateAsync(name, accesTokken);

                return new BaseResponse(true, result);
            }
            catch (Exception ex)
            {

                return new BaseResponse(false, ex.Message);
            }
           
        }



        public async Task<BaseResponse> PushDataToHub(string hubId, string accesTokken, List<string> filePaths, List<string> fileContents)
        {
            try
            {
                var result = await client.PushToHubAsync(hubId, accesTokken, filePaths, fileContents);

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {

                return new BaseResponse(false, ex.Message);
            }
            
        }
    }
}