using APIHUbConnector.Core.DTOs;
using APIHUbConnector.Core.Exceptions;
using System.Threading.Tasks;

namespace APIHUbConnector.Core.Clients
{
    public partial class NetlifyHubClient
    {
        public async Task<DeployKeyDTO> DeployKeys(string accesToken)
        {
            var response = await this.Client.PostAsync($"deploy_keys?access_token={accesToken}", base.CreateHttpContent<string>(""));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var resultIdModel = GetCreatedResponse<DeployKeyDTO>(responseBody);

                return resultIdModel;
            }

            throw new NetlifyClientDeployKeysException($"{nameof(NetlifyClientDeployKeysException)} : Can't create deploy key to host hub : {response.StatusCode}");
        }

        public async Task<string> PostCreateAsync(
            string netlifySiteName,
            string repositoryName,
            string repositoryId,
            string deployKeyId,
            string accesToken,
            string netlifyCMDCommand,
            string netlifyDirBuildName)
        {
            var model = new DeploySiteDTO()
            {
                Name = netlifySiteName,
                Repo = new DeployRepoDTO()
                {
                    Provider = "gitlab",
                    Id = repositoryId,
                    Repo = repositoryName,
                    Private = true,
                    Branch = "master",
                    CMD = netlifyCMDCommand,
                    Dir = netlifyDirBuildName,
                    DeployKeyId = deployKeyId
                }
            };

            var response = await this.Client.PostAsync($"sites?access_token={accesToken}", base.CreateHttpContent<DeploySiteDTO>(model));

            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                var resultIdModel = GetCreatedResponse<HubProjectDTO>(responseBody);

                return resultIdModel.id;
            }

            throw new NetlifyClientPostCreateException($"{nameof(NetlifyClientPostCreateException)} : Can't create post to host hub : {response.StatusCode}");
        }
    }
}