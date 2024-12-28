using BarberShop.Data;
using BarberShop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BarberShop.Controllers
{
    [Authorize] // Yorum yapmak için giriş zorunluluğu
    public class TestimonialsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestimonialsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var testimonials = await _context.Testimonials.ToListAsync();
            return View(testimonials);
        }

        public IActionResult Create()
        {
            // Kullanıcı giriş yapmamışsa yönlendirme yapılır
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                // Oturumdaki kullanıcı bilgilerini al
                testimonial.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Kullanıcı ID'si
                testimonial.UserName = User.Identity.Name; // Kullanıcı Adı

                _context.Add(testimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testimonial);
        }
    }
}
