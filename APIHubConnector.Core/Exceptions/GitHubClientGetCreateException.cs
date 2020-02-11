using System;
using System.Runtime.Serialization;

namespace APIHUbConnector.Core.Exceptions
{
    public class GitHubClientGetCreateException : Exception
    {
 

        public GitHubClientGetCreateException(string message) : base(message)
        {
        }


    }
}
