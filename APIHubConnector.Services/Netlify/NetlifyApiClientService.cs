using APIHubConnector.Services.Models;
using APIHubConnector.Services.Netlify.DTOs;
using APIHUbConnector.Core.Clients;
using APIHUbConnector.Core.DTOs;
using APIHUbConnector.Core.Interfaces;

using System;
using System.Threading.Tasks;

namespace APIHUbConnector.Services.Netlify
{
    public class NetlifyApiClientService :
        IAPIHostClientService<BaseResponse>,
        IHostDeployToken<DeplayKeyResponseDTO>
    {
        private readonly NetlifyHubClient client;

        public NetlifyApiClientService(
            NetlifyHubClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<BaseResponse> CreateHubAsync(string netlifySiteName, string repositoryName, string repositoryId, string deployKeyId, string accesToken,string netlifyCMDCommand, string netlifyDirBuildName)
        {
           
            try
            {
                var result =  await client.PostCreateAsync(netlifySiteName, repositoryName, repositoryId, deployKeyId, accesToken, netlifyCMDCommand, netlifyDirBuildName);

                return new BaseResponse(true, result);
            }
            catch (Exception ex)
            {

                return new BaseResponse(false, ex.Message);
            }
        }                                                                                                              

        public async Task<DeplayKeyResponseDTO> CreateDeployKey(string accesToken)
        {
           
            try
            {
                var result = await client.DeployKeys(accesToken);

                return new DeplayKeyResponseDTO(true, result.Id, result.PublicKey, result.CreatedAt);
                
            }
            catch (Exception ex)
            {
                return new DeplayKeyResponseDTO(false, ex.Message);
            }
            
        }
    }
}