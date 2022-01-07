using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BlogsDemoWebApi.Models
{
    public class ChangePassword
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm new password is required")]
        public string ConfirmNewPassword { get; set; }
    }
}
