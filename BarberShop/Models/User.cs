using Microsoft.AspNetCore.Identity;

namespace BarberShop.Models
{
	public class User : IdentityUser
	{
		public string FullName { get; set; }
	}
}
