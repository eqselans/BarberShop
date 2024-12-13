using BarberShop.Models;
using Microsoft.AspNetCore.Mvc;
using BarberShop.Data;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _context.Appointments
                .Include(a => a.Service)
                .ToListAsync();
            return View(appointments);
        }

        public IActionResult Create()
        {
            var services = _context.Services.ToList(); // Bu satır artık hatasız çalışmalıdır
            ViewBag.Services = services;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Appointments.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }
    }
}
