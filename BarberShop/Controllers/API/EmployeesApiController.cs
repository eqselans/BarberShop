using Microsoft.AspNetCore.Mvc;
using BarberShop.Data;

namespace BarberShop.Controllers.API
{
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesApiController : ControllerBase
	{
		private readonly ApplicationDbContext _context;

		public EmployeesApiController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult GetEmployees()
		{
			var employees = _context.Employees
				.Select(e => new
				{
					e.Id,
					e.Name,
					e.Specialization
				})
				.ToList();

			return Ok(employees);
		}
	}
}
