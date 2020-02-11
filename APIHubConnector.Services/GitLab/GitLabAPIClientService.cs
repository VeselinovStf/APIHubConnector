
using APIHubConnector.Services.Guard;
using APIHubConnector.Services.Interfaces;
using APIHubConnector.Services.Models;
using APIHUbConnector.Core.Clients;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIHUbConnector.Services.GitLab
{
    public class GitLabAPIClientService : IGitLabAPIClientService<BaseResponse>

    {
        private readonly GitLabHubClient client;

        public GitLabAPIClientService(
            GitLabHubClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<BaseResponse> AddKey(string accesToken, string key, string title)
        {
            if (ServiceValidator.StringIsNullOrEmpty(accesToken))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(AddKey),
                        nameof(accesToken),
                        "invalid_parameter_null_or_empty"));
            }

            if (ServiceValidator.StringIsNullOrEmpty(key))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(AddKey),
                        nameof(key),
                        "invalid_parameter_null_or_empty"));
            }

            if (ServiceValidator.StringIsNullOrEmpty(title))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(AddKey),
                        nameof(title),
                        "invalid_parameter_null_or_empty"));
            }

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
            if (ServiceValidator.StringIsNullOrEmpty(name))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(CreateHubAsync),
                        nameof(name),
                        "invalid_parameter_null_or_empty"));
            }

            if (ServiceValidator.StringIsNullOrEmpty(accesTokken))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(CreateHubAsync),
                        nameof(accesTokken),
                        "invalid_parameter_null_or_empty"));
            }

            try
            {
                var result = await client.PostCreateAsync(name, accesTokken);

                return new BaseResponse(true, result);
            }
            catch (Exception ex)
            {

                return new BaseResponse(false, ex.Message);
            }

        }



        public async Task<BaseResponse> PushDataToHub(string hubId, string accesTokken, List<string> filePaths, List<string> fileContents)
        {
            if (ServiceValidator.StringIsNullOrEmpty(hubId))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(PushDataToHub),
                        nameof(hubId),
                        "invalid_parameter_null_or_empty"));
            }

            if (ServiceValidator.StringIsNullOrEmpty(accesTokken))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(PushDataToHub),
                        nameof(accesTokken),
                        "invalid_parameter_null_or_empty"));
            }

            if (ServiceValidator.ObjectIsNull(filePaths))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(PushDataToHub),
                        nameof(filePaths),
                        "invalid_parameter_is_null"));
            }

            if (ServiceValidator.ObjectIsNull(fileContents))
            {
                return new BaseResponse(false,
                    ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(PushDataToHub),
                        nameof(fileContents),
                        "invalid_parameter_is_null"));
            }

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