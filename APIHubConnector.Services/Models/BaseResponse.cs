using APIHubConnector.Core.Models;

namespace APIHubConnector.Services.Models
{
    public class BaseResponse : BaseResponseCoreModel
    {
        public string PublicKey { get; set; }
        public BaseResponse(bool success) : base(success)
        {
        }

        public BaseResponse(bool success, string message) : base(success, message)
        {


        }

        public BaseResponse(bool succes, string message, string publicKey) : base(succes, message)
        {
            PublicKey = publicKey;
        }
    }
}
