using Microsoft.AspNetCore.Identity;

namespace BarberShop.Models
{
	public class Role : IdentityRole<Guid>
	{
		public string Description {  get; set; }

		public DateTime CreatedDate { get; set; } = DateTime.Now;
	}
}
