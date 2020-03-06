using System;

namespace APIHUbConnector.Service.Exceptions
{
    public class GitHubClientPostCreateException : Exception
    {


        public GitHubClientPostCreateException(string message) : base(message)
        {
        }


    }
}