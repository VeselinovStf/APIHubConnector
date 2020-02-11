using APIHubConnector.Services.Models;
using APIHubConnector.Services.Netlify.DTOs;
using APIHUbConnector.Core.Interfaces;
using APIHUbConnector.Services.GitLab;
using APIHUbConnector.Services.Netlify;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHubConnector.Services
{
    public static class APIHubConnectorServiceConfiguration
    {
        public static void ConfigureAPIConnector(this IServiceCollection services)
        {
            services.AddScoped<IAPIRepoClientService<BaseResponse>, GitLabAPIClientService>();
            services.AddScoped<IRepoUserKey<BaseResponse>, GitLabAPIClientService>();
            services.AddScoped<IAPIHostClientService<BaseResponse>, NetlifyApiClientService>();
            services.AddScoped<IHostDeployToken<DeplayKeyResponseDTO>, NetlifyApiClientService > ();

        }
    }
}
