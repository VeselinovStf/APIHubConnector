using APIHubConnector.Core.Interfaces;
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
            services.AddScoped<IGitLabAPIClientService<BaseResponse>, GitLabAPIClientService>();
            services.AddScoped<INetlifyApiClientService<BaseResponse>, NetlifyApiClientService>();
            

        }
    }
}
