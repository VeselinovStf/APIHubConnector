
using APIHubConnector.Services.Interfaces;
using APIHubConnector.Services.Models;
using APIHUbConnector.Services.FileRead;
using APIHUbConnector.Services.FileRead.DTOs;
using APIHUbConnector.Services.FileTransfer;
using APIHUbConnector.Services.FileTransfer.DTOs;
using APIHUbConnector.Services.GitLab;
using APIHUbConnector.Services.Netlify;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace APIHubConnector.Services
{
    public static class APIHubConnectorServiceConfiguration
    {
        public static void ConfigureAPIConnector(this IServiceCollection services)
        {
            //Add clients
            services.AddScoped<IGitLabAPIClientService<BaseResponse>, GitLabAPIClientService>();
            services.AddScoped<INetlifyApiClientService<BaseResponse>, NetlifyApiClientService>();

            //Add project reading service
            services.AddScoped<IFileReader<FileReaderResult>, FileReader>();            
            services.AddTransient<IFileTransferrer<FileTransfererResult>>(r => new FileTransferrer(
                r.GetRequiredService<IFileReader<FileReaderResult>>(),
                new List<string> { ".img", ".jpg", ".png", ".otf", ".eot", ".ttf", ".woff", ".woff2" }
                ));
        }
    }
}
