using System.Collections.Generic;

namespace Infrastructure.Services.APIClientService.DTOs
{
    public class RepoPullTemplateDTO
    {
        public IList<ConvertedFileElementDTO> Elements { get; set; }
    }
}
