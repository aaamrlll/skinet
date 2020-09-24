using System;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessage(StatusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessage(int code)
        {
           return code switch 
           {
             400 => "A bad request, you have made",
             401 => "Authorized you are not",
             404 => "Resource found, it was not",
             500 => "Error message is a place where user is informed about some critical scenario, so using upper case text can give him a feeling of discouragement.",
             _ => null
           };
        }
    }
}