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
                ProjectViewModels projectVM = new ProjectViewModels
                {
                    Projects = projects,
                    User = user
                };

                return View(projectVM);
            }

            return new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
            };
        }

        public ActionResult Create(int? userId)
        {
            if(userId != null)
            {
                var project = new Project();
                project.UserId = (int)userId;
                return View(project);
            }

            return new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
            };
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if(project != null)
            {
                project.User = _ormContext.UsersContext.Where(u => u.Id == project.UserId).FirstOrDefault();
                _ormContext.ProjectsContext.Add(project);
                _ormContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}