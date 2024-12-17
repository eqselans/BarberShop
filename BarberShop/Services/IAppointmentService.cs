using BarberShop.Models;

namespace BarberShop.Services
{
	public interface IAppointmentService
	{
		Task<IEnumerable<Appointment>> GetAppointmentsAsync();
		Task<Appointment> GetAppointmentByIdAsync(int id);
		Task AddAppointmentAsync(Appointment appointment);
		Task UpdateAppointmentAsync(Appointment appointment);
		Task DeleteAppointmentAsync(int id);
		Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(string userId);
	}
}
