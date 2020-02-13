using APIHUbConnector.Utility.Services.FileRead.DTOs;
using System.Collections.Generic;

namespace APIHUbConnector.Utility.Services.FileTransfer.DTOs
{
    public sealed class FileTransfererResult : FileReaderResult
    {

        public IList<ConvertedFileElement> Results { get; set; }


        public FileTransfererResult(bool success, string result) : base(success, result)
        {
            Results = new List<ConvertedFileElement>();

        }

        public FileTransfererResult(bool success, string message, IList<ConvertedFileElement> results)
            : this(success, message)
        {
            Results = results;
        }
    }
}
