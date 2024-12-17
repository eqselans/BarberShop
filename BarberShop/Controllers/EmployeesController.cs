using Microsoft.AspNetCore.Mvc;
using BarberShop.Data; // DbContext için
using BarberShop.Models; // Model için
using System.Linq; // LINQ için
using System.Threading.Tasks;

namespace BarberShop.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor ile DbContext'i enjekte ediyoruz
        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tüm çalışanları listeleme (Index)
        public async Task<IActionResult> Index()
        {
            // Veritabanından çalışanları çekiyoruz
            var employees = _context.Employees.ToList();
            return View(employees);
        }

        // Tek bir çalışanın detaylarını gösterme (Details)
        public async Task<IActionResult> Details(int id)
        {
            // Veritabanından ID'ye göre çalışanın verisini çekiyoruz
            var employee = await _context.Employees.FindAsync(id);

            // Eğer çalışan bulunamazsa 404 hatası döndür
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }
    }
}
