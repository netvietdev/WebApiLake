using System.Web.Http.Controllers;

namespace Rabbit.WebApi.Filters.ValidationHandlers
{
    public interface IValidationHandler
    {
        void Validate(HttpActionContext actionContext);
    }
}
