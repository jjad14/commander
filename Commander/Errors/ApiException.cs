namespace Commander.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null, string details = null) 
            : base(statusCode, message)
        {
            Details = details;
        }

        // Contains the stack trace
        public string Details { get; set; }
    }
}