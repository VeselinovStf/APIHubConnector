using System;

namespace APIHUbConnector.Service.Exceptions
{
    public class GitHubClientGetCreateException : Exception
    {


        public GitHubClientGetCreateException(string message) : base(message)
        {
        }


    }
}
