using System.ComponentModel.DataAnnotations;

namespace BlogsDemoWebApi.Models
{
    public class ResetPassword
    {
        [Required(ErrorMessage = "Username is required!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "New password is required!")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please Confirm new password!")]
        public string ConfirmNewPassword { get; set; }

        public string Token { get; set; }
    }
}
