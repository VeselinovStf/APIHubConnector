using System.Collections.Generic;

namespace APIHUbConnector.Service.DTOs
{
    public class RepoPullTemplateDTO
    {
        public IList<ConvertedFileElementDTO> Elements { get; set; }
    }
}
