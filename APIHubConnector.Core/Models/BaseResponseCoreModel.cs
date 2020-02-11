using System;
using System.Collections.Generic;
using System.Text;

namespace APIHubConnector.Core.Models
{
    public class BaseResponseCoreModel
    {
        public string Message { get; }
        public bool Success { get; }

     
        public BaseResponseCoreModel(bool success)
        {
            Success = success;
        }
        public BaseResponseCoreModel(bool success, string message) : this(success)
        {

            Message = message;
        }
    }
}
