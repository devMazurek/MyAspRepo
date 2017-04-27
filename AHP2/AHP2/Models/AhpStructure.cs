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
        public virtual IEnumerable<CriterionsComparable> Comparables { set; get; }
        public virtual IEnumerable<Alternativ> Alternatives { set; get; }

        [NotMapped]
        public int ProjectId { set; get; }
    }

    public class Criterion: AhpStructure
    {
        [Required]
        public virtual Objective Objective { set; get; }
        public virtual IEnumerable<SubCriterion> SubCriterions { set; get; }
        public virtual IEnumerable<SubCriterionsComparable> Comparables { set; get; }

        [NotMapped]
        public int ObjectiveId { set; get; }

    }

    public class SubCriterion: AhpStructure
    {
        [Required]
        public virtual Criterion Criterion { set; get; }
        public virtual IEnumerable<AlternativesComparable> Comparables { set; get; }

        [NotMapped]
        public int CriterionId { set; get; }
    }

    public class Alternativ: AhpStructure
    {
        [Required]
        public virtual Objective Objective { set; get; }

        [NotMapped]
        public int SubCriterionId { set; get; }
    }

    public abstract class Comparable
    {
        public int Id { set; get; }
        public string Rate { set; get; }
    }

    public class CriterionsComparable: Comparable
    {
        [Required]
        public virtual Objective Objective { set; get; }
        public virtual IEnumerable<CriterionToCompare> CriterionsToCompare { set; get; }

        [NotMapped]
        public int ObjectiveId { set; get; }
    }

    public class SubCriterionsComparable : Comparable
    {
        [Required]
        public virtual Criterion Criterion { set; get; }
        public virtual IEnumerable<SubCriterionToCompare> SubCriterionsToCompare { set; get; }

        [NotMapped]
        public int CriterionId { set; get; }
    }

    public class AlternativesComparable : Comparable
    {
        [Required]
        public virtual SubCriterion SubCriterion { set; get; }
        public virtual IEnumerable<AlternativToCompare> AlternativesToComprae { set; get; }

        [NotMapped]
        public int SubCriterionId { set; get; }
    }

    public abstract class AhpStructureToCompare
    {
        public int Id { set; get; }
    }

    public class CriterionToCompare:AhpStructureToCompare
    {
        [Required]
        public CriterionsComparable CriterionComparable { set; get; }
        public int CriterionId { set; get; }
    }

    public class SubCriterionToCompare:AhpStructureToCompare
    {
        [Required]
        public SubCriterionsComparable SubCriterionComparable { set; get; }
        public int SubCriterionId { set; get; }
    }

    public class AlternativToCompare:AhpStructureToCompare
    {
        [Required]
        public AlternativesComparable AlternativesComparable { set; get; }
        public int AlternativId { set; get; }
    }


}