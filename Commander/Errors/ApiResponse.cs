namespace Commander.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        // determine message by the status code
        private string GetDefaultMessage(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request has occurred",
                401 => "You are not authorized",
                404 => "Resource was not found",
                409 => "Resource could not be modified",
                500 => "A server error has occurred",
                _ => null
            };
        }

    }
}