using System.Web.Http.Filters;

namespace Rabbit.WebApi.Filters.ExceptionHandlers
{
    public interface IExceptionHandler
    {
        bool Handle(HttpActionExecutedContext actionExecutedContext);
    }
}
