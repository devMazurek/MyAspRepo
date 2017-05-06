using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHP2.Controllers
{
    public class PartialMenuController : Controller
    {
        // GET: PartialMenu
        public ActionResult Diverter(string id)
        {
            var str = id.Split('-');

            switch(int.Parse(str[0].ToString()))
            {
                case 0: return RedirectToAction("Update", "Objective", new { id = str[1]});
                case 1: return RedirectToAction("Update", "Criterion");
                case 2: return RedirectToAction("UpdateRate", "Criterion");
                case 3: return RedirectToAction("Update", "SubCriterion");
                case 4: return RedirectToAction("UpdateRate", "SubCriterion");
                case 5: return RedirectToAction("Update", "Alternatives");
                case 6: return RedirectToAction("UpdateRateCriteria", "Alternatives");
                case 7: return RedirectToAction("UpdateRateSubCriteria", "Alternatives");
                case 8: return RedirectToAction("Show", "Summary");
                default:
                    return new ViewResult
                    {
                        ViewName = "~/Views/Errors/Error.cshtml",
                    };
            }
        }
    }
}