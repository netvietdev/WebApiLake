using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;

namespace Rabbit.WebApi.Filters.ValidationHandlers
{
    public class ParameterRequiredValidationHandler : IValidationHandler
    {
        public void Validate(HttpActionContext actionContext)
        {
            var errorMessages = new List<string>();

            var requiredParameters = actionContext.ActionDescriptor.GetParameters()
                .Where(p => p.GetCustomAttributes<ParameterRequiredAttribute>().Any())
                .Select(p => p.ParameterName);

            foreach (var requiredParameter in requiredParameters)
            {
                if (actionContext.ActionArguments.All(arg => arg.Key != requiredParameter))
                {
                    errorMessages.Add(string.Format("Argument <{0}> is missing", requiredParameter));
                }
                else if (actionContext.ActionArguments[requiredParameter] == null)
                {
                    errorMessages.Add(string.Format("Argument <{0}> mustn't be null", requiredParameter));
                }
            }

            actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent(errorMessages.GetType(), errorMessages, new JsonMediaTypeFormatter())
            };
        }
    }
}
