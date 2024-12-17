using Microsoft.AspNetCore.Mvc;
using BarberShop.Models;

namespace BarberShop.Controllers
{
	public class TestimonialsController : Controller
	{
		public IActionResult Index()
		{
			var testimonials = new List<Testimonial>
			{
				new Testimonial { Id = 1, UserId = "user1", Text = "Hizmet çok iyiydi, teşekkür ederim!" },
				new Testimonial { Id = 2, UserId = "user2", Text = "Harika bir deneyim yaşadım." }
			};

			return View(testimonials);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Testimonial testimonial)
		{
			if (ModelState.IsValid)
			{
				// Yorum kaydı yapılabilir.
				return RedirectToAction("Index");
			}
			return View(testimonial);
		}
	}
}
