using System;
using System.Net;

namespace SignUpAndAuthentication.Exceptions
{
    public class BusinessException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public BusinessException(HttpStatusCode statusCode, string message) : base(message)
        {
            this.StatusCode = statusCode;
        }

        public BusinessException(String message) : base (message)
        {
        }
    }
}
