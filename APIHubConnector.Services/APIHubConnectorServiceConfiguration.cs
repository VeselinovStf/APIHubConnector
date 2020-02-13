
using APIHubConnector.Services.Interfaces;
using APIHubConnector.Services.Models;
using APIHubConnector.Services.Public;
using APIHubConnector.Services.Public.DTOs;
using APIHubConnector.Services.Public.Interfaces;

using APIHUbConnector.Services.GitLab;
using APIHUbConnector.Services.Netlify;
using Microsoft.Extensions.DependencyInjection;

namespace APIHubConnector.Services
{
    public static class APIHubConnectorServiceConfiguration
    {
        public static void ConfigureAPIConnector(this IServiceCollection services)
        {
            //Add clients
            services.AddScoped<IGitLabAPIClientService<BaseResponse>, GitLabAPIClientService>();
            services.AddScoped<INetlifyApiClientService<BaseResponse>, NetlifyApiClientService>();
            services.AddScoped<ISiteStorageCreatorService<SiteStorageCreatorResultDTO>, SiteStorageCreatorService>();


        }
    }
}
