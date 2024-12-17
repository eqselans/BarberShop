using Microsoft.AspNetCore.Mvc;
using BarberShop.Models;

namespace BarberShop.Controllers
{
	public class AppointmentsController : Controller
	{
		public IActionResult Index()
		{
			var appointments = new List<Appointment>
			{
				new Appointment { Id = 1, UserId = "user1", EmployeeId = 1, ServiceId = 1, AppointmentDate = DateTime.Now.AddDays(1), Status = "Onaylandı" },
				new Appointment { Id = 2, UserId = "user2", EmployeeId = 2, ServiceId = 2, AppointmentDate = DateTime.Now.AddDays(2), Status = "Beklemede" }
			};

			return View(appointments);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Appointment appointment)
		{
			if (ModelState.IsValid)
			{
				// Randevu kaydı yapılabilir.
				return RedirectToAction("Index");
			}
			return View(appointment);
		}
	}
}
