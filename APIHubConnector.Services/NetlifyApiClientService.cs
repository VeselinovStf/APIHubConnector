using APIHUbConnector.Core.Clients;
using APIHUbConnector.Core.DTOs;
using APIHUbConnector.Core.Interfaces;

using System;
using System.Threading.Tasks;

namespace APIHUbConnector.Core.APIClientService
{
    public class NetlifyApiClientService :
        IAPIHostClientService<NetlifyHubClient>,
        IHostDeployToken<DeployKeyDTO>
    {
        private readonly NetlifyHubClient client;

        public NetlifyApiClientService(
            NetlifyHubClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> CreateHubAsync(string netlifySiteName, string repositoryName, string repositoryId, string deployKeyId, string accesToken)
        {
            return await client.PostCreateAsync(netlifySiteName, repositoryName, repositoryId, deployKeyId, accesToken);
        }

        public async Task<DeployKeyDTO> CreateDeployKey(string accesToken)
        {
            return await client.DeployKeys(accesToken);
        }
    }
}