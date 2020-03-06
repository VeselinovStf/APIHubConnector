using System.Runtime.Serialization;

namespace APIHUbConnector.Service.DTOs
{
    [DataContract]
    public class CreateHubDTO
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}