using System.Runtime.Serialization;

namespace APIHUbConnector.Service.DTOs
{
    [DataContract]
    public class RepoHubVariablesDTO
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "variable_type")]
        public string VariableType { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        [DataMember(Name = "protected")]
        public bool Protected { get; set; }

        [DataMember(Name = "masked")]
        public bool Masked { get; set; }

        [DataMember(Name = "environment_scope")]
        public string Environment_scope { get; set; }
    }
}