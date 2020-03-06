using APIHubConnector.Core.Abstraction;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIHubConnector.Service.Calls
{
    public class SerializationFormatter : ISerializationFormatter<JsonSerializerSettings>
    {
        public JsonSerializerSettings FormatSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                };
            }
        }
    }
}
