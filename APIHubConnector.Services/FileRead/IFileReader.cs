using System.Threading.Tasks;

namespace APIHUbConnector.Services.FileRead
{
    public interface IFileReader<T>
    {
        Task<T> ReadFileAsync(string file);
    }
}
