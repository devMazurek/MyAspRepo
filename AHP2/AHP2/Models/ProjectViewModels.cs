using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHP2.Models
{
    public class ProjectViewModels
    {
        public IEnumerable<Project> Projects { set; get; }
        public User User { set; get; }
    }
}