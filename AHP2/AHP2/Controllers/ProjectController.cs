using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHP2.Models;
using AHP2.Auth;
using AHP2.AhpAlgorithm;

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

        public ActionResult Create(int? id)
        {
            if(id != null)
            {
                var project = new Project();
                project.UserId = (int)id;
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
                project.CreateAt = project.EditAt = DateTime.Now.Date;
                _ormContext.ProjectsContext.Add(project);
                _ormContext.SaveChanges();
                var lastId = _ormContext.ProjectsContext.ToList().LastOrDefault().Id;
                return RedirectToAction("Index");
            }
            return View();
        }

        /*public ActionResult Delete(int? id)
        {
            if(id != null)
            {
                var project = _ormContext.ProjectsContext.Where(p => p.Id == id).FirstOrDefault();
                var objectiv = _ormContext.ObjectivesContext.Where(o => o.Project.Id == project.Id).FirstOrDefault();
                var alternatives = _ormContext.AlternativesContext.Where(a => a.ObjectiveId == objectiv.Id).ToList();
                _ormContext.ProjectsContext.Remove(project);
                if(alternatives.Count > 0)
                    _ormContext.AlternativesContext.RemoveRange(alternatives);
                _ormContext.SaveChanges();
                return RedirectToAction("Index");
            }
            HandleErrorInfo handleErrorInfo = new HandleErrorInfo(new Exception("Bad"), "Controller", "Action");
            return new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
                ViewData = new ViewDataDictionary(handleErrorInfo)
            };
        }   */
    }
}