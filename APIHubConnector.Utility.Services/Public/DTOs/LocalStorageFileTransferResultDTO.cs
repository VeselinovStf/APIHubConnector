using System.Collections.Generic;

namespace APIHubConnector.Utility.Services.Public.DTOs
{
    public class LocalStorageFileTransferResultDTO : BaseResult
    {
        public LocalStorageFileTransferResultDTO(bool success, string message) : base(success, message)
        {

        }

        public IList<string> FilePaths { get; }
        public IList<string> FileContents { get; }

        public LocalStorageFileTransferResultDTO(bool success, string message, IList<string> filePaths, IList<string> fileContents) : base(success, message)
        {
            FilePaths = new List<string>(filePaths);
            FileContents = new List<string>(fileContents);
        }

       
    }
}
