using System.Web.Http.Filters;

namespace Rabbit.WebApi
{
    public interface IExceptionHandler
    {
        bool Handle(HttpActionExecutedContext actionExecutedContext);
    }
}
