using System;
using System.Net;

namespace EventManagement.Server.Application.Errors
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode code, object error = null)
        {
            Code = code;
            Errors = error;
        }

        public HttpStatusCode Code { get; }

        public object Errors { get; }
    }
}
