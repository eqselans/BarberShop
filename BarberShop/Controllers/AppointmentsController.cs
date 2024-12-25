using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BarberShop.Data;
using BarberShop.Models;
using BarberShop.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BarberShop.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AppointmentsController(ApplicationDbContext context,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(AppointmentViewModel viewModel)
        {
            var service = _context.Services.FirstOrDefault(s => s.Id == viewModel.ServiceId);
            var employee = _context.Employees.FirstOrDefault(e => e.Id == viewModel.EmployeeId);
            var user = await _userManager.GetUserAsync(User);


            if (!string.IsNullOrEmpty(employee?.Availability))
            {
                var availabilityParts = employee.Availability.Split('-'); // Örn: "9:00-17:00"

                if (availabilityParts.Length == 2 &&
                    int.TryParse(availabilityParts[0], out int startHour) &&
                    int.TryParse(availabilityParts[1], out int endHour))
                {
                    // Çalışma saatlerini TimeSpan'e çevir
                    TimeSpan startWorkingHour = TimeSpan.FromHours(startHour); // Örn: 9 -> 09:00
                    TimeSpan endWorkingHour = TimeSpan.FromHours(endHour);     // Örn: 19 -> 19:00

                    // Randevu başlangıç ve bitiş saatlerini belirle
                    TimeSpan appointmentStartTime = new TimeSpan(viewModel.AppointmentDate.Hour, viewModel.AppointmentDate.Minute, 0);
                    TimeSpan appointmentEndTime = appointmentStartTime.Add(TimeSpan.FromMinutes(service.DurationInMinutes));

                    // Çalışma saatleri kontrolü
                    if (appointmentStartTime < startWorkingHour || appointmentEndTime > endWorkingHour)
                    {
                        ModelState.AddModelError("", $"Randevu saati seçilen çalışanın çalışma saatleri ({startWorkingHour:hh\\:mm} - {endWorkingHour:hh\\:mm}) arasında olmalıdır.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Çalışan çalışma saatleri bilgisi geçerli bir formatta değil.");
                }
            }
            TimeSpan barberWorkingHour = new TimeSpan(9, 0, 0); // 9:00
            TimeSpan barberEndWorkingHour = new TimeSpan(19, 0, 0); // 19:00

            if (viewModel.AppointmentDate.TimeOfDay < barberWorkingHour || viewModel.AppointmentDate.TimeOfDay > barberEndWorkingHour)
            {
                ModelState.AddModelError("", "İş yeri çalışma saatleri dışında bir saat seçtiniz. Lütfen çalışma saatleri içinde bir saat seçin.");
            }


            if (ModelState.IsValid)
            {
                var appointment = new Appointment
                {
                    ServiceId = viewModel.ServiceId,
                    Service = service,
                    EmployeeId = viewModel.EmployeeId,
                    Employee = employee,
                    PhoneNumber = user.PhoneNumber,
                    AppointmentDate = viewModel.AppointmentDate,
                    Status = "Beklemede",
                    UserName = user.UserName,
                    CreatedAt = DateTime.Now,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) // Oturumdaki kullanıcı ID'si
                };

                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            // Hata durumunda dropdownların tekrar doldurulması
            ViewBag.Services = new SelectList(_context.Services.ToList(), "Id", "Name");
            ViewBag.Employees = new SelectList(_context.Employees.ToList(), "Id", "Name");

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            // Randevuyu veritabanından getir
            var appointment = _context.Appointments
                .FirstOrDefault(a => a.Id == id);

            if (appointment == null)
            {
                return NotFound(); // Randevu bulunamazsa 404 döndür
            }

            // Edit için ViewModel oluştur
            var viewModel = new AppointmentViewModel
            {
                Id = appointment.Id,
                ServiceId = appointment.ServiceId,
                EmployeeId = appointment.EmployeeId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status
            };

            // Dropdown verilerini doldur
            ViewBag.Services = new SelectList(_context.Services.ToList(), "Id", "Name", appointment.ServiceId);
            ViewBag.Employees = new SelectList(_context.Employees.ToList(), "Id", "Name", appointment.EmployeeId);

            return View(viewModel);
        }

        // POST: Randevuyu Güncelle
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult EditAppointment(AppointmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Mevcut randevuyu getir
                var appointment = _context.Appointments
                    .FirstOrDefault(a => a.Id == viewModel.Id);

                if (appointment == null)
                {
                    return NotFound(); // Randevu bulunamazsa 404 döndür
                }

                // Randevu bilgilerini güncelle
                appointment.AppointmentDate = viewModel.AppointmentDate;
                appointment.Status = viewModel.IsConfirmed ? "Onaylandı" : "Bekliyor";

                _context.SaveChanges(); // Veritabanına kaydet

                return RedirectToAction("ManageAppointments", "Admin"); // Admin yönetim sayfasına dön
            }

            // Hata durumunda dropdownların tekrar doldurulması
            ViewBag.Services = new SelectList(_context.Services.ToList(), "Id", "Name", viewModel.ServiceId);
            ViewBag.Employees = new SelectList(_context.Employees.ToList(), "Id", "Name", viewModel.EmployeeId);

            return View(viewModel);
        }
    }
}
