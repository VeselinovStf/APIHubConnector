using System;
using System.Collections.Generic;
using System.Text;

namespace APIHubConnector.Services.Models
{
    public class BaseResponse
    {
        public string Message { get; }
        public bool Success { get; }

        public BaseResponse(bool success)
        {
            Success = success;
        }
        public BaseResponse(bool success, string message) : this(success)
        {
          
            Message = message;
        }
    }
}
