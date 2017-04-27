using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace AHP2.Models
{
    public class User
    {
        [Key]
        public int Id { set; get; }

        [Display(Name = "Your email")]
        [EmailAddress(ErrorMessage = "Not valid email address")]
        [Required(ErrorMessage = "Email is required")]
        [Index("EmailAddressIndex", IsUnique = true)]
        [Column(TypeName = "VARCHAR")]
        [MaxLength(50)]
        public string EmailAdress { set; get; }

        [Required(ErrorMessage = "Password is required. Minimum 6 charakters."), MinLength(6, ErrorMessage = "Minimum 6 charakters")]
        [DataType(DataType.Password)]
        public string Password { set; get; }

        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "Please, confirm your password.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { set; get; }

        public virtual IEnumerable<Project> Projects { set; get; }
    }
}