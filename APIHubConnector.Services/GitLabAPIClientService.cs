using APIHUbConnector.Core.Clients;
using APIHUbConnector.Core.DTOs;
using APIHUbConnector.Core.Interfaces;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIHUbConnector.Core.APIClientService
{
    public class GitLabAPIClientService :
        IAPIRepoClientService<RepoPullTemplateDTO>,
        IRepoUserKey
    {
        private readonly GitLabHubClient client;

        public GitLabAPIClientService(
            GitLabHubClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<bool> AddKey(string accesToken, string key, string title)
        {
            return await this.client.AddRepoKey(accesToken, key, title);
        }

        public async Task<string> CreateHubAsync(string name, string accesTokken)
        {
            return await client.PostCreateAsync(name, accesTokken);
        }



        public async Task<bool> PushDataToHub(string hubId, string accesTokken, List<string> filePaths, List<string> fileContents)
        {
            return await client.PushToHubAsync(hubId, accesTokken, filePaths, fileContents);
        }
    }
}