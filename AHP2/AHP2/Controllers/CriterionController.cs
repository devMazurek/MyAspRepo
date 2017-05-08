using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHP2.Models;

namespace AHP2.Controllers
{
    public class CriterionController : BaseController
    {
        // GET: Criterion
        public ActionResult Index(int? id)
        {
            if(id != null)
            {
                var criterionVM = new CriterionViewModel
                {
                    PartialMenuViewModels = new PartialMenuViewModels
                    {
                        MenuItem = MenuItem.Criterions,
                        ObjectRouting = (int)id
                    },

                    Criterions = _ormContext.CriterionsContext.Where(c => c.Objective.Id == id).ToList(),
                    Objective = _ormContext.ObjectivesContext.Where(o => o.Id == id).FirstOrDefault()
                };

                return View(criterionVM);
            }
            return new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
            };
        }

        [HttpPost]
        public ActionResult Index(CriterionViewModel criterionVM)
        {
            if(criterionVM != null)
            {
                foreach (var criterion in criterionVM.Criterions)
                {
                    if (IsExisting(criterion.Id))
                        Update(criterion);
                    else
                    {
                        criterion.Objective = _ormContext.ObjectivesContext
                            .Where(o => o.Id == criterionVM.Objective.Id)
                            .FirstOrDefault();
                        Create(criterion);
                    }      
                }

                return RedirectToAction("Index", "SubCriterion");
            }
            return new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
            };

        }

        private bool Create(Criterion criterion)
        {
            try
            { 
                _ormContext.CriterionsContext.Add(criterion);
                _ormContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            } 
        }

        private bool Update(Criterion criterion)
        {
            try
            {
                var c = _ormContext.CriterionsContext.Where(cr => cr.Id == criterion.Id).FirstOrDefault();
                c.Name = criterion.Name;
                _ormContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ActionResult Delete(Criterion criterion)
        {
            if (criterion != null)
            {
                var c = _ormContext.CriterionsContext.Where(cr => cr.Id == criterion.Id).FirstOrDefault();
                _ormContext.CriterionsContext.Remove(c);
                _ormContext.SaveChanges();
            }
            return new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
            };

        }

        private bool IsExisting(int id)
        {
            if (_ormContext.CriterionsContext.Where(c => c.Id == id).FirstOrDefault() != null)
                return true;
            else return false;
            
        }
    }
}