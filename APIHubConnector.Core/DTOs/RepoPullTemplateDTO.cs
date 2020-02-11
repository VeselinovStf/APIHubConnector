using System.Collections.Generic;

namespace APIHUbConnector.Core.DTOs
{
    public class RepoPullTemplateDTO
    {
        public IList<ConvertedFileElementDTO> Elements { get; set; }
    }
}
