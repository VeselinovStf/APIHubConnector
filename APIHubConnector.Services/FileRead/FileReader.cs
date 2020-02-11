using APIHUbConnector.Services.FileRead.DTOs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace APIHUbConnector.Services.FileRead
{
    public class FileReader : IFileReader<FileReaderResult>
    {
        public async Task<FileReaderResult> ReadFileAsync(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    string line = await sr.ReadToEndAsync();

                    return new FileReaderResult(true, line);
                }
            }
            catch (Exception ex)
            {
                return new FileReaderResult(false, $"{nameof(FileReader)} : {nameof(ReadFileAsync)} : Can't read file : {ex.Message}");
            }
        }
    }
}
