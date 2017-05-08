using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHP2.Models
{
    public class CriterionViewModel
    {
        public List<Criterion> Criterions { set; get; }
        public PartialMenuViewModels PartialMenuViewModels { set; get; }
        public Objective Objective { set; get; }
    }
}