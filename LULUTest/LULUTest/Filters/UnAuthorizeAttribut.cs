using System.Web.Mvc;

namespace LULUTest.Filters
{
    public class UnAuthorizeAttribut : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            bool authSeller = filterContext.HttpContext.User.IsInRole("seller");
            bool authUser = filterContext.HttpContext.User.IsInRole("user");
            if (authSeller || authUser)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                { "controller", "Book" }, { "action", "Index" }
                });
            }
        }
    }
}