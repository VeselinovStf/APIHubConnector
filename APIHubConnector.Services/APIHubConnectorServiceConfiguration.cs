
using APIHubConnector.Services.Interfaces;
using APIHubConnector.Services.Models;
using APIHUbConnector.Services.GitLab;
using APIHUbConnector.Services.Netlify;
using Microsoft.Extensions.DependencyInjection;

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
