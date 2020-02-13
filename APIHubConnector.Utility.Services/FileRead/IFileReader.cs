using System.Threading.Tasks;

namespace APIHUbConnector.Utility.Services.FileRead
{
    public interface IFileReader<T>
    {
        Task<T> ReadFileAsync(string file);
    }
}
