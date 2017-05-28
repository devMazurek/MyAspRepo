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
            if(criterionVM.Criterions != null)
            {
                DeleteCriterions(criterionVM.Objective, criterionVM.Criterions);
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

                UpdateRatings(_ormContext.CriterionsContext
                    .Where(c => c.Objective.Id == criterionVM.Objective.Id)
                    .ToList());

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

        /*
        [HttpPost, ActionName("Delete")]
        public ActionResult Delete(int? id)
        {
            if (id != null || id != 0)
            {
                var criterion = _ormContext.CriterionsContext.Where(c => c.Id == id).FirstOrDefault();
                _ormContext.CriterionsContext.Remove(criterion);
                _ormContext.SaveChanges();
            }
            return new ViewResult
            {
                ViewName = "~/Views/Errors/Error.cshtml",
            };

        }*/

        private bool IsExisting(int id)
        {
            if (_ormContext.CriterionsContext.Where(c => c.Id == id).FirstOrDefault() != null)
                return true;
            else return false;
            
        }

        private void DeleteCriterions(Objective objective, List<Criterion> criterions)
        {
            var objectiveId = objective.Id;
            var oryginalCriterions = _ormContext.CriterionsContext
                .Where(c => c.Objective.Id == objectiveId)
                .ToList();
            foreach(var c in oryginalCriterions)
            {
                if(IsToDelete(criterions, c))
                {
                    var toDelete = oryginalCriterions
                        .Where(cr => cr.Id == c.Id)
                        .FirstOrDefault();
                    DeleteRatingCriterion(toDelete);
                    _ormContext.CriterionsContext.Remove(toDelete);
                    _ormContext.SaveChanges();
                }
            }
        }

        private bool IsToDelete(List<Criterion> criterions, Criterion originalCriterion)
        {
            foreach(var c in criterions)
            {
                if (c.Id == originalCriterion.Id || originalCriterion.Id == 0)
                    return false;
            }

            return true;
        }

        private void DeleteRatingCriterion(Criterion criterion)
        {
            var criterionsRating = _ormContext.CriterionRatingContext.Where(cr => cr.CriterionComparable.Id == criterion.Id).ToList();
            _ormContext.CriterionRatingContext.RemoveRange(criterionsRating);
            _ormContext.SaveChanges();
        }

        private void UpdateRatings(List<Criterion> criterions)
        {
            AhpAlgorithm.AhpAlgorithm ahp = new AhpAlgorithm.AhpAlgorithm();

            var comparableList = ahp.SelectComparable(criterions);

            foreach(var comparable in comparableList)
            {
                
            }
        }
    }
}