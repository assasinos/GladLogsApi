using GladLogsApi.Configuration.ConfigTypes;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace GladLogsApi.Attributes
{
    public class ValidateAuthKeyAttribute : ActionFilterAttribute
    {
        private readonly IOptions<AuthConfig> _authKey;

        public ValidateAuthKeyAttribute(IOptions<AuthConfig> authKey)
        {
            _authKey = authKey;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("AuthKey", out var authKey))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            if (authKey != _authKey.Value.AuthKey)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
