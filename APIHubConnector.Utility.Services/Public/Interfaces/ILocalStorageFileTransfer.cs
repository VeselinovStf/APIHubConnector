using System.Threading.Tasks;

namespace APIHubConnector.Utility.Services.Public.Interfaces
{
    public interface ILocalStorageFileTransfer<T>
    {
        Task<T> TransferAsync(string localStorageFullPath);
    }
}
