using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHP2.Models
{
    public class ObjectiveViewModels
    {
        public Objective Objective { set; get; }
        public Project Project { set; get; }
        public PartialMenuViewModels PartialMenuViewModels { set; get; }
    }
}