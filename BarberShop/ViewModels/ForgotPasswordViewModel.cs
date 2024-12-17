using System.ComponentModel.DataAnnotations;

namespace BarberShop.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-posta")]
        public string Email { get; set; }
    }
}
