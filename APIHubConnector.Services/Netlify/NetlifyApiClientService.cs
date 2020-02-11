using APIHubConnector.Services.Guard;
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

        public async Task<BaseResponse> CreateHubAsync(string netlifySiteName, string repositoryName, string repositoryId,
            string deployKeyId, string accesToken,string netlifyCMDCommand, string netlifyDirBuildName)
        {
            if (ServiceValidator.ObjectIsNull(netlifySiteName))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(netlifySiteName),
                        "invalid_parameter_is_null"));
            }

            if (ServiceValidator.ObjectIsNull(repositoryName))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(repositoryName),
                        "invalid_parameter_is_null"));
            }

            if (ServiceValidator.ObjectIsNull(repositoryId))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(repositoryId),
                        "invalid_parameter_is_null"));
            }

            if (ServiceValidator.ObjectIsNull(deployKeyId))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(deployKeyId),
                        "invalid_parameter_is_null"));
            }

            if (ServiceValidator.ObjectIsNull(accesToken))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(accesToken),
                        "invalid_parameter_is_null"));
            }

            if (ServiceValidator.ObjectIsNull(netlifyCMDCommand))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(netlifyCMDCommand),
                        "invalid_parameter_is_null"));
            }

            if (ServiceValidator.ObjectIsNull(netlifyDirBuildName))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(netlifyDirBuildName),
                        "invalid_parameter_is_null"));
            }

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
            if (ServiceValidator.StringIsNullOrEmpty(accesToken))
            {
                return new DeplayKeyResponseDTO(false,
                    ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateDeployKey),
                        nameof(accesToken),
                        "invalid_parameter_null_or_empty"));
            }

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