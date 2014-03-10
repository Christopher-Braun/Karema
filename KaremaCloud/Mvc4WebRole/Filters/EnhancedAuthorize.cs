using System.Web.Mvc;

namespace Mvc4WebRole.Filters
{
    public class EnhancedAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if ( filterContext.HttpContext.User.Identity.IsAuthenticated )
            {
                filterContext.Result = new RedirectResult("~/Account/AccessDenied");
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}