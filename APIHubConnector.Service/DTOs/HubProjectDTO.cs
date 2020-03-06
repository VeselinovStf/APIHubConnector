using System.Runtime.Serialization;

namespace APIHUbConnector.Service.DTOs
{
    [DataContract]
    public class HubProjectDTO
    {
        [DataMember(Name = "id")]
        public string id { get; set; }
    }
}