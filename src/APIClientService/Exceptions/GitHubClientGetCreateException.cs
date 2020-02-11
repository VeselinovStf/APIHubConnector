﻿using System;
using System.Runtime.Serialization;

namespace Infrastructure.Services.APIClientService.Exceptions
{
    public class GitHubClientGetCreateException : Exception
    {
        public GitHubClientGetCreateException()
        {
        }

        public GitHubClientGetCreateException(string message) : base(message)
        {
        }

        public GitHubClientGetCreateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GitHubClientGetCreateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
