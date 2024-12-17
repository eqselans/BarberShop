using Microsoft.AspNetCore.Mvc;
using BarberShop.Models;

namespace BarberShop.Controllers
{
	public class EmployeesController : Controller
	{
		public IActionResult Index()
		{
			var employees = new List<Employee>
			{
				new Employee { Id = 1, Name = "Ahmet Yılmaz", Specialization = "Berber" },
				new Employee { Id = 2, Name = "Mehmet Kaya", Specialization = "Kuaför" }
			};

			return View(employees);
		}

		public IActionResult Details(int id)
		{
			var employee = new Employee
			{
				Id = id,
				Name = "Ahmet Yılmaz",
				Specialization = "Berber"
			};

			return View(employee);
		}
	}
}
