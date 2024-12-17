using System.ComponentModel.DataAnnotations;

namespace BarberShop.ViewModels
{
	public class LoginViewModel
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Display(Name = "Beni Hatırla")]
		public bool RememberMe { get; set; }
	}
}
