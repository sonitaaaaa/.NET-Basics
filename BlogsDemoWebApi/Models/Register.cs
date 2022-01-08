using System;
using System.ComponentModel.DataAnnotations;

namespace BlogsDemoWebApi.Models
{
    public class Register
    {
        [Required(ErrorMessage ="Username is required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "First Name is required!")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required!")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Gender is required!")]
        [Display(Name = "Gender")]
        public bool Gender { get; set; }
    }
}
