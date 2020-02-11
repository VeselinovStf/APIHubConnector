using System;
using System.Runtime.Serialization;

namespace APIHUbConnector.Core.Exceptions
{
    public class GitHubClientPostCreateException : Exception
    {
  

        public GitHubClientPostCreateException(string message) : base(message)
        {
        }


    }
}