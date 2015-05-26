using System;
using Rabbit.WebApi.Filters.ExceptionHandlers;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Filters;

namespace Rabbit.WebApi.Filters
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IList<IExceptionHandler> _handlers;

        /// <summary>
        /// All handlers which handle the exception, only first one handler can be executed.
        /// </summary>
        public CustomExceptionFilterAttribute(params IExceptionHandler[] handlers)
        {
            if (handlers.Length < 1)
            {
                throw new ArgumentException("At least one handler must be provided");
            }

            _handlers = handlers;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (_handlers.Any(handler => handler.Handle(actionExecutedContext)) == false)
            {
                new CatchAllExceptionHandler().Handle(actionExecutedContext);
            }
        }
    }
}
