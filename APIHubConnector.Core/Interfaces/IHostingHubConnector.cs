using System.Threading.Tasks;

namespace APIHUbConnector.Core.Interfaces
{
    public interface IHostingHubConnector
    {
        Task<string> CreateHub(string netlifySiteName, string repositoryName, string repositoryId, string deployKeyId, string accesToken);
    }
}