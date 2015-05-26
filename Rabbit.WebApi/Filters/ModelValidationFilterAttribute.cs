using Rabbit.WebApi.Filters.ValidationHandlers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Rabbit.WebApi.Filters
{
    public class ModelValidationFilterAttribute : ActionFilterAttribute
    {
        private readonly IValidationHandler _handler;

        public ModelValidationFilterAttribute(IValidationHandler handler)
        {
            _handler = handler;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _handler.Validate(actionContext);
        }
    }
}
