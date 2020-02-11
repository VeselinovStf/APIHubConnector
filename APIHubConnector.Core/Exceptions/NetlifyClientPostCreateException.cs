using System;
using System.Runtime.Serialization;

namespace APIHUbConnector.Core.Exceptions
{
    public class NetlifyClientPostCreateException : Exception
    {
 

        public NetlifyClientPostCreateException(string message) : base(message)
        {
        }

     
    }
}