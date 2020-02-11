using System.Runtime.Serialization;

namespace APIHUbConnector.Core.DTOs
{
    [DataContract]
    public class CreateHubDTO
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}