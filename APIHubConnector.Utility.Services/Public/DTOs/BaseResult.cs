namespace APIHubConnector.Utility.Services.Public.DTOs
{
    public class BaseResult
    {
        public string Message { get; set; }

        public bool Success { get; set; }

        public BaseResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
