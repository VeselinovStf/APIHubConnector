﻿
using APIHubConnector.Services.Guard;
using APIHubConnector.Services.Interfaces;
using APIHubConnector.Services.Models;

using APIHUbConnector.Core.Clients;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIHUbConnector.Services.Netlify
{
    public class NetlifyApiClientService : INetlifyApiClientService<BaseResponse>
    {
        private readonly NetlifyHubClient client;

        public NetlifyApiClientService(
            NetlifyHubClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }


        public async Task<BaseResponse> CreateHubAsync(string netlifySiteName, string repositoryName, string repositoryId,
            string deployKeyId, string accesToken, string netlifyCMDCommand, string netlifyDirBuildName)
        {
            if (ServiceValidator.ObjectIsNull(netlifySiteName))
            {
                return new BaseResponse(false,
                    new List<string>(){ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(netlifySiteName),
                        "invalid_parameter_is_null") });
            }

            if (ServiceValidator.ObjectIsNull(repositoryName))
            {
                return new BaseResponse(false,
                    new List<string>(){ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(repositoryName),
                        "invalid_parameter_is_null") });
            }

            if (ServiceValidator.ObjectIsNull(repositoryId))
            {
                return new BaseResponse(false,
                    new List<string>(){ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(repositoryId),
                        "invalid_parameter_is_null") });
            }

            if (ServiceValidator.ObjectIsNull(deployKeyId))
            {
                return new BaseResponse(false,
                    new List<string>(){ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(deployKeyId),
                        "invalid_parameter_is_null") });
            }

            if (ServiceValidator.ObjectIsNull(accesToken))
            {
                return new BaseResponse(false,
                    new List<string>(){ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(accesToken),
                        "invalid_parameter_is_null") });
            }

            if (ServiceValidator.ObjectIsNull(netlifyCMDCommand))
            {
                return new BaseResponse(false,
                    new List<string>(){ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(netlifyCMDCommand),
                        "invalid_parameter_is_null") });
            }

            if (ServiceValidator.ObjectIsNull(netlifyDirBuildName))
            {
                return new BaseResponse(false,
                    new List<string>(){ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateHubAsync),
                        nameof(netlifyDirBuildName),
                        "invalid_parameter_is_null") });
            }

            try
            {
                var result = await client.PostCreateAsync(netlifySiteName, repositoryName, repositoryId, deployKeyId, accesToken, netlifyCMDCommand, netlifyDirBuildName);

                return new BaseResponse(true, new List<string>() { result });
            }
            catch (Exception ex)
            {

                return new BaseResponse(false, new List<string>() { ex.Message });
            }
        }

        public async Task<BaseResponse> CreateDeployKey(string accesToken)
        {
            if (ServiceValidator.StringIsNullOrEmpty(accesToken))
            {
                return new BaseResponse(false,
                    new List<string>(){ServiceValidator.MessageCreator(
                        nameof(NetlifyApiClientService),
                        nameof(CreateDeployKey),
                        nameof(accesToken),
                        "invalid_parameter_null_or_empty") });
            }

            try
            {
                var result = await client.DeployKeys(accesToken);

                var id = result.Id;
                var publicKey = result.PublicKey;

                var responseMessage = new List<string>();
                responseMessage.Add(id);
                responseMessage.Add(publicKey);

                return new BaseResponse(true, new List<string>(responseMessage));

            }
            catch (Exception ex)
            {
                return new BaseResponse(false, new List<string>() { ex.Message });
            }

        }


    }
}