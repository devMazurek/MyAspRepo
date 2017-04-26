using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AHP2.Models
{
    public class Project
    {
        public int Id { set; get; }
        public string Name { set; get; }

        public DateTime CreateAt { set; get; }
        public DateTime EditAt { set; get; }

        [Required]
        public virtual User User { set; get; }

        [NotMapped]
        public int UserId { set; get; }
    }
}