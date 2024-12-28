using BarberShop.Models;
using BarberShop.Services;
using BarberShop.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Controllers
{
    [Authorize(Roles = "Admin")] // Sadece Admin yetkisi olanlar erişebilir
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;
        private readonly IServiceService _serviceService;
        private readonly IEmployeeService _employeeService;
        private readonly ITestimonialService _testimonialService;

        public AdminController(
            IUserService userService,
            IAppointmentService appointmentService,
            IServiceService serviceService,
            IEmployeeService employeeService,
            ITestimonialService testimonialService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
            _serviceService = serviceService;
            _employeeService = employeeService;
            _testimonialService = testimonialService;
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

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var userViewModel = new UserViewModel
            {
                Id = user.Id.ToString(),
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = await _userService.GetUserRoleAsync(user)
            };

            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                var success = await _userService.UpdateUserAsync(userViewModel);
                if (success)
                {
                    return RedirectToAction(nameof(ManageUsers));
                }
            }

            return View(userViewModel);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            var success = await _userService.DeleteUserAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(ManageUsers));
        }
        #endregion

        public async Task<IActionResult> ManageTestimonials()
        {
            var testimonials = await _testimonialService.GetAllTestimonialsAsync();
            return View(testimonials);
        }

        public async Task<IActionResult> EditTestimonial(int id)
        {
            var testimonial = await _testimonialService.GetTestimonialByIdAsync(id);
            if (testimonial == null)
            {
                return NotFound();
            }

            return View(testimonial);
        }

        [HttpPost]
        public async Task<IActionResult> EditTestimonial(Testimonial testimonial)
        {
            if (ModelState.IsValid)
            {
                await _testimonialService.UpdateTestimonialAsync(testimonial);
                return RedirectToAction(nameof(ManageTestimonials));
            }

            return View(testimonial);
        }

        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            await _testimonialService.DeleteTestimonialAsync(id);
            return RedirectToAction(nameof(ManageTestimonials));
        }



    }
}
