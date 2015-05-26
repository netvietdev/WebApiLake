using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace Rabbit.WebApi.Filters.ExceptionHandlers
{
    /// <summary>
    /// Returns an InternalServerError containing the exception message
    /// </summary>
    public sealed class CatchAllExceptionHandler : IExceptionHandler
    {
        public bool Handle(HttpActionExecutedContext actionExecutedContext)
        {
            var errorMessages = GetErrorMessages(actionExecutedContext.Exception);

            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new ObjectContent(errorMessages.GetType(), errorMessages, new JsonMediaTypeFormatter())
            };

            return true;
        }

        private IList<KeyValuePair<string, string>> GetErrorMessages(Exception exception)
        {
            return new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("", exception.Message)
            };
        }
    }
}
