using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IFileReader
    {
        Task<string> ReadFileAsync(string file);
    }
}