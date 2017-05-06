using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHP2.Models
{
    public abstract class AhpStructure
    {
        public int Id { set; get; }
        public string Name { set; get; }
    }

    public class Objective: AhpStructure
    {
        [Required]
        public virtual Project Project { set; get; }
        public virtual IEnumerable<Criterion> Criterions { set; get; }

        [NotMapped]
        public int ProjectId { set; get; }
    }

    public class Criterion: AhpStructure
    {
        [Required]
        public virtual Objective Objective { set; get; }
        public virtual IEnumerable<SubCriterion> SubCriterions { set; get; }
        
        public virtual IEnumerable<CriterionRating> CriterionRatings { set; get; } 

        [NotMapped]
        public int ObjectiveId { set; get; }

    }

    public class SubCriterion: AhpStructure
    {
        [Required]
        public virtual Criterion Criterion { set; get; }

        public virtual IEnumerable<SubCriterionRating> SubCriterionRatings { set; get; }

        [NotMapped]
        public int CriterionId { set; get; }
    }

    public class Alternativ: AhpStructure
    {
        public int ObjectiveId { set; get; }// without reference

        public virtual IEnumerable<AlternativToCriterionRating> AlternativToCriterionRating { set; get; }
        public virtual IEnumerable<AlternativToCriterionRating> AlternativToSubCriterionRating { set; get; }
    }

    public abstract class Rating
    {
        public int Id { set; get; }
        public string Rate { set; get; }
    }

    public class CriterionRating: Rating
    {
        [Required]
        public Criterion Criterion { set; get; }

        public Criterion CriterionComparable { set; get; }

        [NotMapped]
        public int CriterionId { set; get; }
    }

    public class SubCriterionRating : Rating
    {
        [Required]
        public SubCriterion SubCriterion { set; get; }

        public SubCriterion SubCriterionComparable { set; get; }

        [NotMapped]
        public int SubCriterionId { set; get; }
    }

    public class AlternativToCriterionRating : Rating
    {
        [Required]
        public Alternativ Alternativ { set; get;}

        public Alternativ AlternativeComparable { set; get; }

        [Required]
        public Criterion Criterion { set; get; }

        [NotMapped]
        public int AlternativId { set; get; }

    }

    public class AlternativToSubCriterionRating : Rating
    {
        [Required]
        public Alternativ Alternativ { set; get; }

        public Alternativ AlternativeComparable { set; get; }

        [Required]
        public SubCriterion SubCriterion { set; get; }

        [NotMapped]
        public int AlternativId { set; get; }
    }
}