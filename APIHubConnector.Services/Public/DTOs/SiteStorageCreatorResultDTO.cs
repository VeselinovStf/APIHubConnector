using APIHubConnector.Services.Models;
using System.Collections.Generic;

namespace APIHubConnector.Services.Public.DTOs
{
    public class SiteStorageCreatorResultDTO : BaseResponse
    {
        public SiteStorageCreatorResultDTO()
        {

        }
        public SiteStorageCreatorResultDTO(bool success) : base(success)
        {
        }

        public SiteStorageCreatorResultDTO(bool success, IList<string> message) : base(success, message)
        {
        }
    }
}
