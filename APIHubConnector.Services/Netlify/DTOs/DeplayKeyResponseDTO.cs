using APIHubConnector.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHubConnector.Services.Netlify.DTOs
{
    public class DeplayKeyResponseDTO : BaseResponse
    {

        public string Id { get; }


        public string PublicKey { get; }


        public string CreatedAt { get; }

        public DeplayKeyResponseDTO(bool success, string id, string publicKey, string createdAt)
            : base(success)
        {
            Id = id;
            PublicKey = publicKey;
            CreatedAt = createdAt;
        }

        public DeplayKeyResponseDTO(bool success, string message) : base(success, message)
        {
        }
    }
}
