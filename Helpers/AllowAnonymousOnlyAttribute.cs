using System.Web.Mvc;

namespace Wall.Helpers
{
    public class AllowAnonymousOnlyAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
        }
    }
}
