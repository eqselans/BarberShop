using System.ComponentModel.DataAnnotations;

namespace BarberShop.ViewModels
{
	public class RegisterViewModel
	{
		[Required]
		[Display(Name = "Ad Soyad")]
		public string FullName { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		[StringLength(100, ErrorMessage = "Şifre en az {2} karakter uzunluğunda olmalıdır.", MinimumLength = 6)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "Şifre Tekrar")]
		[Compare("Password", ErrorMessage = "Şifre ve şifre tekrarı eşleşmiyor.")]
		public string ConfirmPassword { get; set; }
	}
}
