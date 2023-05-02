using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TO_DO_List.Attributes
{
    public class AuthenticateAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isUserAuthenticated = context.HttpContext.Session.GetInt32("UserID").HasValue;

            if (!isUserAuthenticated)
            {
                context.Result = new RedirectResult("~/User/Login");
            }
        }
    }
}
