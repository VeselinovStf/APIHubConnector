using System.Runtime.Serialization;

namespace APIHUbConnector.Core.DTOs
{
    [DataContract]
    public class DeploySiteDTO
    {
        [DataMember(Name = "repo")]
        public DeployRepoDTO Repo { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}