using Microsoft.AspNetCore.Mvc;
using BarberShop.Data; // ApplicationDbContext için
using BarberShop.Models; // Service modeli için
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    public class ServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor ile DbContext'i enjekte ediyoruz
        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tüm Hizmetleri Listeleme
        public IActionResult Index()
        {
            // Veritabanından tüm hizmetleri çekiyoruz
            var services = _context.Services.ToList();
            return View(services);
        }

        // GET: Tek Bir Hizmetin Detaylarını Gösterme
        public async Task<IActionResult> Details(int id)
        {
            // Veritabanından ID'ye göre hizmet çekiyoruz
            var service = await _context.Services.FindAsync(id);

            // Eğer hizmet bulunamazsa 404 hatası döndür
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }
    }
}
