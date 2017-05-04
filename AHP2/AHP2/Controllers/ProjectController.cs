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
                _ormContext.ProjectsContext.Remove(project);
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
            Objective objective = _ormContext.ObjectivesContext.Add(new Objective
            {
                Project = _ormContext.ProjectsContext.Where(p => p.Id == id).FirstOrDefault(),
                Name = "My objective",
            });

            Criterion criterion_1 = _ormContext.CriterionsContext.Add(new Criterion
            {
                Name = "Criterion 1",
                Objective = objective
            });

            Criterion criterion_2 = _ormContext.CriterionsContext.Add(new Criterion
            {
                Name = "Criterion 2",
                Objective = objective
            });

            objective.Criterions = new List<Criterion>() { criterion_1, criterion_2 };

            SubCriterion subCriterion_1_1 = _ormContext.SubCriterionsContext.Add(new SubCriterion
            {
                Name = "Subcriterion 1.1",
                Criterion = criterion_1
            });

            SubCriterion subCriterion_1_2 = _ormContext.SubCriterionsContext.Add(new SubCriterion
            {
                Name = "Subcriterion 1.2",
                Criterion = criterion_1
            });

            SubCriterion subCriterion_2_1 = _ormContext.SubCriterionsContext.Add(new SubCriterion
            {
                Name = "Subcriterion 2.1",
                Criterion = criterion_2
            });

            SubCriterion subCriterion_2_2 = _ormContext.SubCriterionsContext.Add(new SubCriterion
            {
                Name = "Subcriterion 2.2",
                Criterion = criterion_2
            });

            criterion_1.SubCriterions = new List<SubCriterion>() { subCriterion_1_1, subCriterion_1_2
            , subCriterion_2_1, subCriterion_2_2};
            criterion_2.SubCriterions = new List<SubCriterion>() { subCriterion_1_1, subCriterion_1_2
            , subCriterion_2_1, subCriterion_2_2};

            Alternativ alternative_1 = _ormContext.AlternativesContext.Add(new Alternativ
            {
                Name = "Alternativ 1",
                Criterions = new List<Criterion>() { criterion_1, criterion_2 },
                SubCriterions = new List<SubCriterion>() { subCriterion_1_1, subCriterion_1_2
                , subCriterion_2_1, subCriterion_2_2}
            });

            Alternativ alternative_2 = _ormContext.AlternativesContext.Add(new Alternativ
            {
                Name = "Alternativ 2",
                Criterions = new List<Criterion>() { criterion_1, criterion_2 },
                SubCriterions = new List<SubCriterion>() { subCriterion_1_1, subCriterion_1_2
                , subCriterion_2_1, subCriterion_2_2}
            });

            criterion_1.Alternatives = new List<Alternativ>() { alternative_1, alternative_2 };
            criterion_2.Alternatives = new List<Alternativ>() { alternative_1, alternative_2 };

            subCriterion_1_1.Alternatives = new List<Alternativ>() { alternative_1, alternative_2 };
            subCriterion_1_2.Alternatives = new List<Alternativ>() { alternative_1, alternative_2 };
            subCriterion_2_1.Alternatives = new List<Alternativ>() { alternative_1, alternative_2 };
            subCriterion_2_2.Alternatives = new List<Alternativ>() { alternative_1, alternative_2 };

            

            AhpAlgorithm.AhpAlgorithm ahp = new AhpAlgorithm.AhpAlgorithm();

            List<CriterionsComparableViewModels> criterionsComparableVM = ahp.SelectComparable(objective.Criterions.ToList());
            List<CriterionsComparable> criterionsComparable = new List<CriterionsComparable>();
            /*foreach (var c in criterionsComparableVM)
            {
                CriterionsComparable cc = new CriterionsComparable
                {
                    Rate = c.Rate,
                    CriterionsToCompare = new List<CriterionToCompare>
                    {
                        new CriterionToCompare
                        {
                            CriterionId = c.Criterion1.Id
                        }
                    }
                };
            }*/

            for(int i = 0; i < criterionsComparableVM.Count; i++)
            {
                CriterionsComparable cC = new CriterionsComparable
                {
                    Objective = objective,
                    Rate = criterionsComparableVM[i].Rate
                };

                var criterionToCompare_1 = new CriterionToCompare
                {
                    Criterion = criterion_1,
                    CriterionComparable = cC
                };
                var criterionToCompare_2 = new CriterionToCompare
                {
                    Criterion = criterion_2,
                    CriterionComparable = cC
                };


                cC.CriterionsToCompare = new List<CriterionToCompare>() { criterionToCompare_1, criterionToCompare_2};

                _ormContext.CriterionsComparableContext.Add(cC);
                _ormContext.CriterionsToCompareContext.AddRange(new List<CriterionToCompare>() { criterionToCompare_1, criterionToCompare_2});


            }

            _ormContext.SaveChanges();
        }
    }
}