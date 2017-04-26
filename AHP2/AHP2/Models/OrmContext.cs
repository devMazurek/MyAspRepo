using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AHP2.Models
{
    public class OrmContext: DbContext
    {
        public DbSet<User> UsersContext { set; get; }
        public DbSet<Project> ProjectsContext { set; get; }
        public DbSet<Objective> ObjectivesContext { set; get; }
        public DbSet<Criterion> CriterionsContext { set; get; }
        public DbSet<SubCriterion> SubCriterionsContext { set; get; }
        public DbSet<Alternativ> AlternativesContext { set; get; }
        public DbSet<CriterionsComparable> CriterionsComparableContext { set; get; }
        public DbSet<SubCriterionsComparable> SubCriterionsComparableContext { set; get; }
        public DbSet<AlternativesComparable> AlternativesComparableContext { set; get; }
        public DbSet<CriterionToCompare> CriterionsToCompareContext { set; get; } //without multi-path on delete cascade
        public DbSet<SubCriterionToCompare> SubCriterionsToCompareContext { set; get; }
        public DbSet<AlternativToCompare> AlternativesToCompareContext { set; get; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Project>()
                .HasRequired(p => p.User)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Objective>()
                .HasRequired(o => o.Project)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Criterion>()
                .HasRequired(c => c.Objective)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<SubCriterion>()
                .HasRequired(s => s.Criterion)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Alternativ>()
                .HasRequired(a => a.SubCriterion)
                .WithMany()
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<CriterionsComparable>()
                .HasRequired(c => c.Criterion1)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CriterionsComparable>()
                .HasRequired(c => c.Criterion2)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubCriterionsComparable>()
                .HasRequired(s => s.SubCriterion1)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SubCriterionsComparable>()
                .HasRequired(s => s.SubCriterion2)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AlternativesComparable>()
                .HasRequired(a => a.Alternativ1)
                .WithMany()
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AlternativesComparable>()
                .HasRequired(a => a.Alternativ2)
                .WithMany()
                .WillCascadeOnDelete(false);*/
        }
    }
}