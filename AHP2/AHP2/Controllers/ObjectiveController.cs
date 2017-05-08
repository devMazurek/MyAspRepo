using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHP2.Models;

namespace AHP2.Controllers
{
    public class ObjectiveController : BaseController
    {
        // GET: Objective
        public ActionResult Update(int? id)
        {
            if (id != null)
            {
                var objectiveVM = new ObjectiveViewModels
                {
                    Objective = _ormContext.ObjectivesContext.Where(o => o.Project.Id == (int)id).FirstOrDefault(),
                    PartialMenuViewModels = new PartialMenuViewModels
                    {
                        MenuItem = MenuItem.Objective,
                        ObjectRouting = (int)id
                    }
                };

                return View(objectiveVM);
            }

            return new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
            };
        }

        [HttpPost]
        public ActionResult Update(ObjectiveViewModels objectiveVM)
        {

            if(objectiveVM != null)
            {
                var objective = _ormContext.ObjectivesContext.Where(o => o.Id == objectiveVM.Objective.Id)
                    .FirstOrDefault();
                objective.Name = objectiveVM.Objective.Name;
                _ormContext.SaveChanges();
                return RedirectToAction("Index", "Criterion", new { id = objectiveVM.Objective.Id});
            }

            return new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
            };
        }
    }
}