using APIHubConnector.Core.Abstraction;
using APIHUbConnector.Service.DTOs;
using APIHUbConnector.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIHubConnector.Core.Clients
{
    public class GitLabClient : BaseHubClient, IRepositoryHubClient
    {
        private readonly IHttpContextCreateor _httpContextCreateor;
        private readonly ICreateResponse _createResponse;
        private readonly IList<string> _imageExtensions;
      
        public GitLabClient(
            HttpClient client,
            IHttpContextCreateor httpContextCreateor,
            ICreateResponse createResponse,
            IList<string> imageExtensions            
            ) : base(client)
        {
            this._httpContextCreateor = httpContextCreateor;
            this._createResponse = createResponse;
            this._imageExtensions = imageExtensions;           
        }

        public async Task<string> PostCreateAsync(string newHubName, string credidentials)
        {
            var model = new CreateHubDTO()
            {
                Name = newHubName
            };

            var response = await this.Client.PostAsync($"projects?access_token={credidentials}", _httpContextCreateor.CreateHttpContent<CreateHubDTO>(model));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var resultIdModel = _createResponse.GetCreatedResponse<HubProjectDTO>(responseBody);

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

            var response = await this.Client.PostAsync($"user/keys?access_token={accesToken}", _httpContextCreateor.CreateHttpContent<RepoUserKeyDTO>(model));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            throw new GitHubClientPostCreateException($"{nameof(GitHubClientPostCreateException)} : Can't create post to repo hub : {response.StatusCode} : {response.RequestMessage}");
        }

        public async Task<bool> InitialPushToHubAsync(string hubId, string accesTokken, IList<string> filePaths, IList<string> fileContents)
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
                    Encoding = _imageExtensions.Any(e => fp.Contains(e)) ? "base64" : "text"
                }))
            };

            var response = await this.Client.PostAsync($"projects/{hubId}/repository/commits?access_token={accesTokken}", _httpContextCreateor.CreateHttpContent<PushCreateDTO>(pushModel));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
}
