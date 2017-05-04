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
                CreateSampleAhpStructure(lastId);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id)
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
        }

        private void CreateSampleAhpStructure(int id)
        {
            var project = _ormContext.ProjectsContext.Where(p => p.Id == id).FirstOrDefault();

            var objective = _ormContext.ObjectivesContext.Add(new Objective
            {
                Project = project,
                Name = "My objectiv"
            });

            _ormContext.SaveChanges();

            var criterions = _ormContext.CriterionsContext.AddRange(new List<Criterion>
            {
                new Criterion
                {
                    Objective = objective,
                    Name = "Criteerion 1"
                },
                new Criterion
                {
                    Objective = objective,
                    Name = "Criteerion 2"
                }
            }).ToList();

            var subcriterions = _ormContext.SubCriterionsContext.AddRange(new List<SubCriterion>
            {
                new SubCriterion
                {
                    Criterion = criterions[0],
                    Name = "Subcriterion 1.1"
                },
                new SubCriterion
                {
                    Criterion = criterions[0],
                    Name = "Subcriterion 1.2"
                },
                new SubCriterion
                {
                    Criterion = criterions[1],
                    Name = "Subcriterion 2.1"
                },
                new SubCriterion
                {
                    Criterion = criterions[1],
                    Name = "Subcriterion 2.2"
                }
            }).ToList();

            var criterionComparable = _ormContext.CriterionRatingContext.AddRange(new List<CriterionRating>
            {
                 new CriterionRating
                {
                    Criterion = criterions[0],
                    CriterionComparable = criterions[1],
                    Rate = "1"
                }
        }).ToList();

            var subCriterionComparables = _ormContext.SubCriterionRatingContext.AddRange(new List<SubCriterionRating>
            {
                new SubCriterionRating
                {
                    SubCriterion = subcriterions[0],
                    SubCriterionComparable = subcriterions[1],
                    Rate = "1"
                },
                new SubCriterionRating
                {
                    SubCriterion = subcriterions[2],
                    SubCriterionComparable = subcriterions[3],
                    Rate = "1"
                }
            }).ToList();

            var alternatives = _ormContext.AlternativesContext.AddRange(new List<Alternativ>
            {
                new Alternativ
                {
                    ObjectiveId = _ormContext.ObjectivesContext.ToList().LastOrDefault().Id,
                    Name = "Alternative 1"
                },
                new Alternativ
                {
                    ObjectiveId = _ormContext.ObjectivesContext.ToList().LastOrDefault().Id,
                    Name = "Alternative 2"
                }
            }).ToList();

            var alternativesToCriterion = _ormContext.AlternativToCriterionRatingContext.AddRange(new List<AlternativToCriterionRating>
            {
                new AlternativToCriterionRating
                {
                    Alternativ = alternatives[0],
                    AlternativeComparable = alternatives[1],
                    Criterion = criterions[0],
                    Rate = "1"
                },
                new AlternativToCriterionRating
                {
                    Alternativ = alternatives[0],
                    AlternativeComparable = alternatives[1],
                    Criterion = criterions[1],
                    Rate = "1"
                }
            }).ToList();

            var alternativesToSubCriterion = _ormContext.AlternativToSubCriterionRatingContext.AddRange(new List<AlternativToSubCriterionRating> {
                new AlternativToSubCriterionRating
                {
                    Alternativ = alternatives[0],
                    AlternativeComparable = alternatives[1],
                    SubCriterion = subcriterions[0],
                    Rate = "1"
                },
                new AlternativToSubCriterionRating
                {
                    Alternativ = alternatives[0],
                    AlternativeComparable = alternatives[1],
                    SubCriterion = subcriterions[1],
                    Rate = "1"
                },
                new AlternativToSubCriterionRating
                {
                    Alternativ = alternatives[0],
                    AlternativeComparable = alternatives[1],
                    SubCriterion = subcriterions[2],
                    Rate = "1"
                },
                new AlternativToSubCriterionRating
                {
                    Alternativ = alternatives[0],
                    AlternativeComparable = alternatives[1],
                    SubCriterion = subcriterions[3],
                    Rate = "1"
                }
            });

            _ormContext.SaveChanges();
        }
    }
}