using BarberShop.Models;
using BarberShop.Services;
using BarberShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers
{
    [Authorize(Roles = "Admin")] // Sadece Admin yetkisi olanlar erişebilir
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;
        private readonly IServiceService _serviceService;
        private readonly IEmployeeService _employeeService;

        public AdminController(IUserService userService, IAppointmentService appointmentService, IServiceService serviceService, IEmployeeService employeeService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
            _serviceService = serviceService;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Services Management
        public async Task<IActionResult> ManageServices()
        {
            var services = await _serviceService.GetAllServicesAsync();
            return View(services);
        }

        public IActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddService(Service service)
        {
            if (ModelState.IsValid)
            {
                await _serviceService.AddServiceAsync(service);
                return RedirectToAction(nameof(ManageServices));
            }
            return View(service);
        }

        public async Task<IActionResult> EditService(int id)
        {
            var service = await _serviceService.GetServiceByIdAsync(id);
            if (service == null) return NotFound();
            return View(service);
        }

        [HttpPost]
        public async Task<IActionResult> EditService(Service service)
        {
            if (ModelState.IsValid)
            {
                await _serviceService.UpdateServiceAsync(service);
                return RedirectToAction(nameof(ManageServices));
            }
            return View(service);
        }

        public async Task<IActionResult> DeleteService(int id)
        {
            await _serviceService.DeleteServiceAsync(id);
            return RedirectToAction(nameof(ManageServices));
        }
        #endregion

        #region Appointments Management
        public async Task<IActionResult> ManageAppointments()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        public async Task<IActionResult> EditAppointment(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();
            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> EditAppointment(Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                var success = await _appointmentService.UpdateAppointmentAsync(appointment);
                if (success) return RedirectToAction(nameof(ManageAppointments));
            }
            return View(appointment);
        }

        public async Task<IActionResult> DeleteAppointment(int id)
        {
            var success = await _appointmentService.DeleteAppointmentAsync(id);
            if (!success) return NotFound();
            return RedirectToAction(nameof(ManageAppointments));
        }
        #endregion

        #region Users Management
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var role = await _userService.GetUserRoleAsync(user);
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id.ToString(),
                    FullName = user.FullName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Role = role
                });
            }

            return View(userViewModels);
        }
        #endregion

        #region Employees Management
        public async Task<IActionResult> ManageEmployee()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees);
        }

        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.AddEmployeeAsync(employee);
                return RedirectToAction(nameof(ManageEmployee));
            }
            return View(employee);
        }

        public async Task<IActionResult> EditEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null) return NotFound();
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _employeeService.UpdateEmployeeAsync(employee);
                return RedirectToAction(nameof(ManageEmployee));
            }
            return View(employee);
        }

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return RedirectToAction(nameof(ManageEmployee));
        }
        #endregion

        public IActionResult Reports()
        {
            return View();
        }
    }
}
