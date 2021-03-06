﻿using APIHUbConnector.Core.DTOs;
using APIHUbConnector.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIHUbConnector.Core.Clients
{
    public partial class GitLabHubClient
    {
        private readonly List<string> ImageExtensions = new List<string> { ".img", ".jpg", ".png", ".otf", ".eot", ".ttf", ".woff", ".woff2" };




        public async Task<string> PostCreateAsync(string newHubName, string credidentials)
        {
            var model = new CreateHubDTO()
            {
                Name = newHubName
            };

            var response = await this.Client.PostAsync($"projects?access_token={credidentials}", base.CreateHttpContent<CreateHubDTO>(model));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var resultIdModel = GetCreatedResponse<HubProjectDTO>(responseBody);

                return resultIdModel.id;
            }

            throw new GitHubClientPostCreateException($"{nameof(GitHubClientPostCreateException)} : Can't create post to repo hub : {response.StatusCode}");
        }

        public async Task<bool> AddRepoKey(string accesToken, string key, string title)
        {
            var model = new RepoUserKeyDTO()
            {
                Key = key,
                Title = title
            };

            var response = await this.Client.PostAsync($"user/keys?access_token={accesToken}", base.CreateHttpContent<RepoUserKeyDTO>(model));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new GitHubClientPostCreateException($"{nameof(GitHubClientPostCreateException)} : Can't create post to repo hub : {response.StatusCode} : {response.RequestMessage}");
        }

        public async Task<bool> PushToHubAsync(string hubId, string accesTokken, IList<string> filePaths, IList<string> fileContents)
        {
            var branch = "master";
            var commitMessage = "Initial";
            var actions = "create";

            var pushModel = new PushCreateDTO()
            {
                Branch = branch,
                CommitMessage = commitMessage,
                Actions = new List<HubFileDTO>(filePaths.Zip(fileContents, (fp, fc) => new HubFileDTO()
                {
                    Action = actions,
                    FilePath = fp,
                    Content = fc,
                    Encoding = this.ImageExtensions.Any(e => fp.Contains(e)) ? "base64" : "text"
                }))
            };

            var response = await this.Client.PostAsync($"projects/{hubId}/repository/commits?access_token={accesTokken}", base.CreateHttpContent<PushCreateDTO>(pushModel));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}