using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHP2.Models;
using AHP2.Auth;

namespace AHP2.Controllers
{
    
    public class BaseController : Controller
    {
        protected OrmContext _ormContext = new OrmContext(); 

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            filterContext.ExceptionHandled = true;

            var model = new HandleErrorInfo(ex, "Controller", "Action");

            filterContext.Result = new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
                ViewData = new ViewDataDictionary(model)
            };
        }
    }
}