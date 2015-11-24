using System.Web.Http.Controllers;

namespace Rabbit.WebApi
{
    public interface IValidationHandler
    {
        void Validate(HttpActionContext actionContext);
    }
}
