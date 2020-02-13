using APIHubConnector.Utility.Services.Public;
using APIHubConnector.Utility.Services.Public.DTOs;
using APIHubConnector.Utility.Services.Public.Interfaces;
using APIHUbConnector.Utility.Services.FileRead;
using APIHUbConnector.Utility.Services.FileRead.DTOs;
using APIHUbConnector.Utility.Services.FileTransfer;
using APIHUbConnector.Utility.Services.FileTransfer.DTOs;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace APIHubConnector.Utility.Services
{
    public static class APIHubConnectorUtilityServiceConfiguration
    {
        public static void ConfigureAPIConnectorUtility(this IServiceCollection services)
        {
            //Add clients
            services.AddScoped<IFileReader<FileReaderResult>, FileReader>();

            services.AddTransient<IFileTransferrer<FileTransfererResult>>(r => new FileTransferrer(
                r.GetRequiredService<IFileReader<FileReaderResult>>(),
                new List<string> { ".img", ".jpg", ".png", ".otf", ".eot", ".ttf", ".woff", ".woff2" }
                ));

            services.AddScoped<ILocalStorageFileTransfer<LocalStorageFileTransferResultDTO>, LocalStorageFileTransfer>();


        }
    }
}
