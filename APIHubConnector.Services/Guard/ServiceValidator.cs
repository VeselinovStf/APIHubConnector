namespace APIHubConnector.Services.Guard
{
    public static class ServiceValidator
    {
        public static bool StringIsNullOrEmpty(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return true;
            }

            return false;
        }

        public static bool ObjectIsNull(object obj)
        {
            if (obj == null)
            {
                return true;
            }

            return false;
        }

        public static string MessageCreator(string className, string methodName, string parammeterName, string message)
        {
            return $"ERROR: {className} : {methodName} : {parammeterName} : --- {message}";
        }
    }
}
