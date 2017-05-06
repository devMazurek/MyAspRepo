using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHP2.Models
{
    public enum MenuItem
    {
        Objective,
        Criterions,
        CriterionsRating,
        SubCriterions,
        SubCriterionsRating,
        Alternatives,
        AlternativesToCriterionsRating,
        AlternativesToSubCriterionsRating,
        Summary
    }

    public class PartialMenuViewModels
    {
        public MenuItem MenuItem { set; get; }  
        public int  ObjectRoueting { set; get; }
        
        public PartialMenuViewModels()
        {

        }  

        public PartialMenuViewModels(MenuItem menuItem)
        {
            this.MenuItem = menuItem;
        }

        public string GetNameMenuItem(MenuItem menuItem)
        {
            switch(menuItem)
            {
                case MenuItem.Objective: return "Objective";
                case MenuItem.Criterions: return "Criteria";
                case MenuItem.CriterionsRating: return "Comparision of criteria";
                case MenuItem.SubCriterions: return "Sub-criteria";
                case MenuItem.SubCriterionsRating: return "Comparision of sub-criteria";
                case MenuItem.Alternatives: return "Alternatives";
                case MenuItem.AlternativesToCriterionsRating: return "Comparision of criteria - alternatives";
                case MenuItem.AlternativesToSubCriterionsRating: return "Comparision of sub-criteria - alternatives";
                case MenuItem.Summary: return "Summamry";
                default: return "";
            }
        }

        public List<MenuItem> GetAllMenuItems
        {
            get
            {
                return Enum.GetValues(typeof(MenuItem)).Cast<MenuItem>().ToList();
            }
        }
    }
}