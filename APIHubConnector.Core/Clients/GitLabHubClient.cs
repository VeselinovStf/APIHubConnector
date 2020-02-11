using APIHUbConnector.Core.DTOs;
using APIHUbConnector.Core.Interfaces;
using System.Net.Http;

namespace APIHUbConnector.Core.Clients
{
    /// <summary>
    ///
    /// </summary>
    public partial class GitLabHubClient : BaseHubClient
    {
        private HttpClient Client { get; }

       

        public GitLabHubClient(
            HttpClient client)
            
        {
            this.Client = client;
           
        }
    }
}