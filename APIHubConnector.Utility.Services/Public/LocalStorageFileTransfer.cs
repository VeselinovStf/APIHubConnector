using APIHubConnector.Utility.Services.Public.DTOs;
using APIHubConnector.Utility.Services.Public.Interfaces;
using APIHUbConnector.Utility.Services.FileTransfer;
using APIHUbConnector.Utility.Services.FileTransfer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIHubConnector.Utility.Services.Public
{
    public class LocalStorageFileTransfer : ILocalStorageFileTransfer<LocalStorageFileTransferResultDTO>
    {
        private readonly IFileTransferrer<FileTransfererResult> _fileTransferrer;

        public LocalStorageFileTransfer(IFileTransferrer<FileTransfererResult> fileTransferrer)
        {
            this._fileTransferrer = fileTransferrer;
        }
        public async Task<LocalStorageFileTransferResultDTO> TransferAsync(string localStorageFullPath)
        {

            if (string.IsNullOrWhiteSpace(localStorageFullPath))
            {
                return new LocalStorageFileTransferResultDTO(
                    false, $"{nameof(LocalStorageFileTransfer)} : {nameof(TransferAsync)} : {nameof(localStorageFullPath)} - is null/empty");
            }

            try
            {
                var defaultStoreTypeSiteFileRead = await this._fileTransferrer.FilesToListAsync(localStorageFullPath);

                if (defaultStoreTypeSiteFileRead.Success)
                {
                    return new LocalStorageFileTransferResultDTO(true, "local_files_transfered",
                        new List<string>(defaultStoreTypeSiteFileRead.Results.Select(p => p.FilePath)),
                        new List<string>(defaultStoreTypeSiteFileRead.Results.Select(p => p.FileContent)));
                }

                return new LocalStorageFileTransferResultDTO(false, defaultStoreTypeSiteFileRead.Result);

            }
            catch (Exception ex)
            {
                return new LocalStorageFileTransferResultDTO(false, ex.Message);
            }


        }
    }
}
