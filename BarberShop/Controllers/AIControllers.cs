using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers
{
	public class AIController : Controller
	{
		public IActionResult Recommendation()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Recommendation(IFormFile uploadedImage)
		{
			if (uploadedImage != null)
			{
				// Yapay zeka işlemi
				string recommendation = "Kısa ve modern bir saç modeli öneriyoruz!";
				return View("Result", recommendation);
			}

			ViewBag.Error = "Lütfen bir fotoğraf yükleyin.";
			return View();
		}
	}
}
