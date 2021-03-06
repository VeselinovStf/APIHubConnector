﻿
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

        public async Task<BaseResponse> AddKeyAsync(string accesToken, string key, string title)
        {
            if (ServiceValidator.StringIsNullOrEmpty(accesToken))
            {
                return new BaseResponse(false,
                     new List<string>(){ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(AddKeyAsync),
                        nameof(accesToken),
                        "invalid_parameter_null_or_empty") });
            }

            if (ServiceValidator.StringIsNullOrEmpty(key))
            {
                return new BaseResponse(false,
                     new List<string>(){ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(AddKeyAsync),
                        nameof(key),
                        "invalid_parameter_null_or_empty") });
            }

            if (ServiceValidator.StringIsNullOrEmpty(title))
            {
                return new BaseResponse(false,
                     new List<string>(){ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(AddKeyAsync),
                        nameof(title),
                        "invalid_parameter_null_or_empty") });
            }

            try
            {
                var result = await this.client.AddRepoKey(accesToken, key, title);

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {

                return new BaseResponse(false, new List<string>() { ex.Message });
            }

        }

        public async Task<BaseResponse> CreateHubAsync(string name, string accesTokken)
        {
            if (ServiceValidator.StringIsNullOrEmpty(name))
            {
                return new BaseResponse(false,
                     new List<string>(){ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(CreateHubAsync),
                        nameof(name),
                        "invalid_parameter_null_or_empty") });
            }

            if (ServiceValidator.StringIsNullOrEmpty(accesTokken))
            {
                return new BaseResponse(false,
                     new List<string>(){ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(CreateHubAsync),
                        nameof(accesTokken),
                        "invalid_parameter_null_or_empty") });
            }

            try
            {
                var result = await client.PostCreateAsync(name, accesTokken);

                return new BaseResponse(true, new List<string>() { result });
            }
            catch (Exception ex)
            {

                return new BaseResponse(false, new List<string>() { ex.Message });
            }

        }



        public async Task<BaseResponse> PushDataToHubAsync(
            string hubId, string accesTokken, IList<string> filePaths, IList<string> fileContents)
        {
            if (ServiceValidator.StringIsNullOrEmpty(hubId))
            {
                return new BaseResponse(false,
                     new List<string>(){ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(PushDataToHubAsync),
                        nameof(hubId),
                        "invalid_parameter_null_or_empty") });
            }

            if (ServiceValidator.StringIsNullOrEmpty(accesTokken))
            {
                return new BaseResponse(false,
                     new List<string>(){ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(PushDataToHubAsync),
                        nameof(accesTokken),
                        "invalid_parameter_null_or_empty") });
            }

            if (ServiceValidator.ObjectIsNull(filePaths))
            {
                return new BaseResponse(false,
                     new List<string>(){ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(PushDataToHubAsync),
                        nameof(filePaths),
                        "invalid_parameter_is_null") });
            }

            if (ServiceValidator.ObjectIsNull(fileContents))
            {
                return new BaseResponse(false,
                     new List<string>(){ServiceValidator.MessageCreator(
                        nameof(GitLabAPIClientService),
                        nameof(PushDataToHubAsync),
                        nameof(fileContents),
                        "invalid_parameter_is_null") });
            }

            try
            {
                var result = await client.PushToHubAsync(hubId, accesTokken, filePaths, fileContents);

                return new BaseResponse(true);
            }
            catch (Exception ex)
            {

                return new BaseResponse(false, new List<string>() { ex.Message });
            }

        }
    }
}