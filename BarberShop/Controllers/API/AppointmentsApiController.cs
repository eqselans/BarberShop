using Microsoft.AspNetCore.Mvc;
using BarberShop.Data;

namespace BarberShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class AppointmentsApiController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public AppointmentsApiController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetAppointments()
		{
			var appointments = _context.Appointments
				.Select(a => new
				{
					a.Id,
					a.AppointmentDate,
					a.Status
				})
				.ToList();

			return Ok(appointments);
		}
	}
}
