using System;

namespace APIHUbConnector.Core.Exceptions
{
    public class GitHubClientPostCreateException : Exception
    {


        public GitHubClientPostCreateException(string message) : base(message)
        {
        }


    }
}