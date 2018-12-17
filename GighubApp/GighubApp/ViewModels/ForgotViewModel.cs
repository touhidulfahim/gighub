using System.ComponentModel.DataAnnotations;

namespace GighubApp.ViewModels
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}