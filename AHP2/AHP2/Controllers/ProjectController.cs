using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHP2.Models;
using AHP2.Auth;

namespace AHP2.Controllers
{
    [MyAuth]
    public class ProjectController : BaseController
    {
        // GET: Project
        public ActionResult Index()
        {
            var user = Session["User"] as User;

            if (user != null)
            {    
                var projects = _ormContext.ProjectsContext.Where(p => p.User.Id == user.Id).ToList();
                return View(projects);
            }

            return new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
            };
        }
    }
}