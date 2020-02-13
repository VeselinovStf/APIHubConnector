using APIHubConnector.Core.Models;
using System.Collections.Generic;

namespace APIHubConnector.Services.Models
{
    public class BaseResponse : BaseResponseCoreModel
    {
        public BaseResponse()
        {

        }

        public BaseResponse(bool success) : base(success)
        {
        }

        public BaseResponse(bool success, IList<string> message) : base(success, message)
        {


        }


    }
}
