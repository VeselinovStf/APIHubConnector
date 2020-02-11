using System.Collections.Generic;

namespace APIHubConnector.Core.Models
{
    public class BaseResponseCoreModel
    {
        public IList<string> Message { get; }
        public bool Success { get; }

        public BaseResponseCoreModel()
        {
            Message = new List<string>();
        }
        public BaseResponseCoreModel(bool success)
        {
            Success = success;
        }
        public BaseResponseCoreModel(bool success, IList<string> message) : this(success)
        {

            Message = message;
        }
    }
}
