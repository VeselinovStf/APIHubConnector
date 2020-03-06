﻿using System.Runtime.Serialization;

namespace APIHUbConnector.Service.DTOs
{
    [DataContract]
    public class RepoUserKeyDTO
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }
    }
}