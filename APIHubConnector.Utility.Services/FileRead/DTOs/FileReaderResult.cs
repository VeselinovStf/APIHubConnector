

namespace APIHUbConnector.Utility.Services.FileRead.DTOs
{
    public class FileReaderResult
    {


        public FileReaderResult(bool success, string result)
        {
            Success = success;
            Result = result;
        }

        public bool Success { get; }
        public string Result { get; }
    }
}
