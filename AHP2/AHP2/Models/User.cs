using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace AHP2.Models
{
    public class User
    {
        public int Id { set; get; }

        [Display(Name = "Your emai")]
        [EmailAddress, Required(ErrorMessage = "Email is required")]
        public string EmailAdress { set; get; }

        [Required(ErrorMessage = "Password is required. Minimum 6 charakters."), MinLength(6)]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Please, confirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { set; get; }

        public virtual IEnumerable<Project> Projects { set; get; }
    }
}