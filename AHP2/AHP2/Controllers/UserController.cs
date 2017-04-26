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
    public class UserController : BaseController
    {
        // GET: User
        public ActionResult Index()
        {
            var user = _ormContext.UsersContext
                .Where(u => u.Id == 1)
                .FirstOrDefault();
            var project = _ormContext.ProjectsContext
                .Where(p => p.UserId == user.Id)
                .FirstOrDefault();
            var objective = _ormContext.ObjectivesContext
                .Where(o => o.ProjectId == project.Id)
                .FirstOrDefault();
            var criterionsRate = _ormContext.CriterionsComparableContext
                .Where(c => c.ObjectiveId == objective.Id)
                .ToList();

            List<CriterionsComparableViewModels> criterionsComparableViewsModel 
                = new List<CriterionsComparableViewModels>();

            foreach(var c in criterionsRate)
            {
                var criterionsToCompare = _ormContext.CriterionsToCompareContext
                    .Where(model => model.CriterionComparable.Id == c.Id)
                    .ToList();

                criterionsComparableViewsModel.Add(new CriterionsComparableViewModels
                {
                    Criterion1 = _ormContext.CriterionsContext
                    .Where(model => model.Id == criterionsToCompare[0].Id)
                    .FirstOrDefault(),
                    Criterion2 = _ormContext.CriterionsContext
                    .Where(model => model.Id == criterionsToCompare[1].Id)
                    .FirstOrDefault(),
                    Rate = c.Rate
                });
            }

            return View();
        }
    }
}