using APIHubConnector.Core.Abstraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace APIHubConnector.Service.Calls
{
    public class ContextCreator : IHttpContextCreateor
    {
        private readonly ISerializationFormatter<JsonSerializerSettings> formatSetting;

        public ContextCreator(ISerializationFormatter<JsonSerializerSettings> formatSetting)
        {
            this.formatSetting = formatSetting;
        }
        public HttpContent CreateHttpContent<T>(T content)
        {
            var json = JsonConvert.SerializeObject(content, this.formatSetting.FormatSettings);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
