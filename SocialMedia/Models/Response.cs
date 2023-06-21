using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class ErrorResponse
    {

        public ErrorType error { get; set; }
        public ErrorResponse(int ErrorCode, string Message)
        {
            this.error = new ErrorType
            {
                ErrorCode = ErrorCode,
                message = Message
            };
        }
    }

    public class ErrorType
    {
        public int ErrorCode { get; set; }
        public string message { get; set; }
    }


}