using APIHubConnector.Core.Abstraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHubConnector.Service.Calls
{
    public class ResponseDeserializator : ICreateResponse
    {
        public T GetCreatedResponse<T>(string responseMessage)
        {
            var model = JsonConvert.DeserializeObject<T>(responseMessage);

            return model;
        }
    }
}
