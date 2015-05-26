using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Controllers;

namespace Rabbit.WebApi.Filters.ValidationHandlers
{
    /// <summary>
    /// Parses model state and set all errors on response
    /// </summary>
    public sealed class DefaultValidationHandler : IValidationHandler
    {
        public void Validate(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid)
            {
                return;
            }

            var errorMessages = GetAllErrors(actionContext);
            actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new ObjectContent(errorMessages.GetType(), errorMessages, new JsonMediaTypeFormatter())
            };
        }

        private IList<KeyValuePair<string, string>> GetAllErrors(HttpActionContext actionContext)
        {
            var errorMessages = new List<KeyValuePair<string, string>>();

            foreach (var kvp in actionContext.ModelState)
            {
                var propertyName = GetPropertyName(kvp.Key);
                var errors =
                    kvp.Value.Errors.Select(
                        modelError => new KeyValuePair<string, string>(propertyName, modelError.ErrorMessage));
                errorMessages.AddRange(errors);
            }

            return errorMessages;
        }

        private string GetPropertyName(string source)
        {
            const string keyword = ".";
            return source.Contains(keyword) ? source.Substring(source.LastIndexOf(keyword, StringComparison.Ordinal) + 1) : source;
        }
    }
}
