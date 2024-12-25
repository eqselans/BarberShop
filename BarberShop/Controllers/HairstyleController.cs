using BarberShop.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BarberShop.Models;
using BarberShop.Services;

namespace BarberShop.Controllers
{
    public class HairstyleController : Controller
    {
        private readonly HairstyleApiService _hairstyleApiService;

        public HairstyleController(HairstyleApiService hairstyleApiService)
        {
            _hairstyleApiService = hairstyleApiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Suggest([FromForm] string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                ModelState.AddModelError("ImageUrl", "Image URL cannot be empty.");
                return View("Index");
            }

            var result = await _hairstyleApiService.GetHairstyleSuggestionsAsync(imageUrl);
            ViewBag.Result = result;
            return View("Index");
        }
    }
}
