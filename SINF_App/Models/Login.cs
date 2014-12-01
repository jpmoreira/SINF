using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SINF_App.Models
{
    public class Login
    {
        [Required(ErrorMessage = "Username is required")] // make the field required
        [Display(Name = "Username")]  // Set the display name of the field
        public string Username{ get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        public string Password{ get; set; }

        [Required(ErrorMessage = "Company is required")]
        [Display(Name = "Company")]
        public string Company{ get; set; }
   
    }
}