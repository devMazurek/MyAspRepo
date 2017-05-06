using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHP2.Models;

namespace AHP2.Controllers
{
    public class ObjectiveController : Controller
    {
        // GET: Objective
        public ActionResult Update(int? id)
        {
            if (id != null)
            {
                var objectiveVM = new ObjectiveViewModels
                {
                    Objective = new Objective()
                    {
                        ProjectId = (int)id
                    },
                    PartialMenuViewModels = new PartialMenuViewModels
                    {
                        MenuItem = MenuItem.Objective,
                        ObjectRoueting = (int)id
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
            return View();
        }
    }
}