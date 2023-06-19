using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialMedia.Models
{
    public class ErrorResponse
    {
        public int ErrorCode { get; }
        public string Message { get; }

        public ErrorResponse(int ErrorCode, string Message)
        {
            this.ErrorCode = ErrorCode;
            this.Message = Message;
        }
    }


}