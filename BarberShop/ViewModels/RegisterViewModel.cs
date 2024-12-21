using System.ComponentModel.DataAnnotations;

namespace BarberShop.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ad ve soyad zorunludur.")]
        [StringLength(50, ErrorMessage = "Ad ve soyad en fazla 50 karakter olabilir.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifreyi doğrulamanız gerekiyor.")]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Telefon numarası zorunludur.")]
        [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string PhoneNumber { get; set; }
    }
}
