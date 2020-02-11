using APIHubConnector.Core.Models;

namespace APIHubConnector.Services.Models
{
    public class BaseResponse : BaseResponseCoreModel
    {

        public BaseResponse(bool success) : base(success)
        {
        }

        public BaseResponse(bool success, string message) : base(success, message)
        {


        }
    }
}
