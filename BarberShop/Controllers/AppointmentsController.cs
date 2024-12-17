using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarberShop.Data;
using BarberShop.Models;
using BarberShop.ViewModels;

namespace BarberShop.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Create Randevu Sayfası
        public IActionResult Create()
        {
            // Service ve Employee verilerini alıp SelectList'e dönüştürüyoruz
            ViewBag.Services = new SelectList(_context.Services.ToList(), "Id", "Name");
            ViewBag.Employees = new SelectList(_context.Employees.ToList(), "Id", "Name");

            var viewModel = new AppointmentViewModel
            {
                AppointmentDate = DateTime.Now
            };

            return View(viewModel);
        }

        // POST: Yeni Randevu Kaydetme
        [HttpPost]
        public IActionResult Create(AppointmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    ServiceId = viewModel.ServiceId,
                    EmployeeId = viewModel.EmployeeId,
                    AppointmentDate = viewModel.AppointmentDate,
                    Status = "Beklemede",
                    UserId = "12345" // Örnek kullanıcı ID, gerçek oturumdan alınabilir
                };

                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                return RedirectToAction("Index", "Appointments");
            }

            // Hata durumunda dropdownların tekrar doldurulması
            ViewBag.Services = new SelectList(_context.Services.ToList(), "Id", "Name");
            ViewBag.Employees = new SelectList(_context.Employees.ToList(), "Id", "Name");

            return View(viewModel);
        }
    }
}
