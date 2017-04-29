using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHP2.Controllers;
using AHP2.Models;

namespace AHP2.Auth
{
    public class MyAuth: AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {

            var sessionUser = httpContext.Session["User"] as User;
            if (httpContext.Session["User"] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(
                    new
                    {
                        controller = "User",
                        action = "LogIn"
                    }));
        }
    }
}