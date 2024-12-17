using Microsoft.AspNetCore.Mvc;
using BarberShop.Models;

namespace BarberShop.Controllers
{
	public class ServicesController : Controller
	{
		public IActionResult Index()
		{
			var services = new List<Service>
			{
				new Service { Id = 1, Name = "Geleneksel Kesim", Description = "Alışılagelmiş tıraş yöntemleri", DurationInMinutes = 30, Price = 400, ImageUrl = "/images/service-icon-1.png" },
				new Service { Id = 2, Name = "Bıyık Kesimi", Description = "Şık ve modern bıyık kesimi", DurationInMinutes = 15, Price = 150, ImageUrl = "/images/service-icon-2.png" },
				new Service { Id = 3, Name = "Sakal Kesimi", Description = "Farklı ve gelişmiş sakal kesim yöntemleri", DurationInMinutes = 20, Price = 150, ImageUrl = "/images/service-icon-3.png" }
			};

			return View(services);
		}

		public IActionResult Details(int id)
		{
			var service = new Service
			{
				Id = id,
				Name = "Geleneksel Kesim",
				Description = "Detaylı açıklama",
				DurationInMinutes = 30,
				Price = 400,
				ImageUrl = "/images/service-icon-1.png"
			};

			return View(service);
		}
	}
}
