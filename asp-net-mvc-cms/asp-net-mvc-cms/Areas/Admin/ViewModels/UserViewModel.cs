using System.ComponentModel.DataAnnotations;

namespace asp_net_mvc_cms.Areas.Admin.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string DisplayName { get; set; }

        public string CurrentPassword { get; set; }

        [Compare("ConfirmPassword", ErrorMessage = "The new password and confirmation password don't match.")]
        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}