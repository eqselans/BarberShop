using BarberShop.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers
{
    public class AIController : Controller
    {
        private readonly IAIRecommendationService _aiRecommendationService;

        public AIController(IAIRecommendationService aiRecommendationService)
        {
            _aiRecommendationService = aiRecommendationService;
        }

        [HttpGet]
        public IActionResult Recommendation()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Recommendation(IFormFile photo, string hair_options)
        {
            if (photo == null || photo.Length == 0)
            {
                ModelState.AddModelError("", "Lütfen bir fotoğraf yükleyin.");
                return View();
            }

            if (string.IsNullOrEmpty(hair_options))
            {
                ModelState.AddModelError("", "Lütfen bir saç modeli seçin.");
                return View();
            }

            try
            {
                var base64Image = await _aiRecommendationService.GetHairstyleRecommendationAsync(photo, hair_options);
                ViewBag.RecommendedImage = base64Image;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"API ile iletişim sırasında bir hata oluştu: {ex.Message}");
            }

            return View();
        }
    }
}
