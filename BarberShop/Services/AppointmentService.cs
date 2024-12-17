using BarberShop.Data.Repository;
using BarberShop.Models;

namespace BarberShop.Services
{
	public class AppointmentService : IAppointmentService
	{
		private readonly AppointmentRepository _appointmentRepository;

		public AppointmentService(AppointmentRepository appointmentRepository)
		{
			_appointmentRepository = appointmentRepository;
		}

		public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
		{
			return await _appointmentRepository.GetAllAsync();
		}

		public async Task<Appointment> GetAppointmentByIdAsync(int id)
		{
			return await _appointmentRepository.GetByIdAsync(id);
		}

		public async Task AddAppointmentAsync(Appointment appointment)
		{
			await _appointmentRepository.AddAsync(appointment);
		}

		public async Task UpdateAppointmentAsync(Appointment appointment)
		{
			await _appointmentRepository.UpdateAsync(appointment);
		}

		public async Task DeleteAppointmentAsync(int id)
		{
			await _appointmentRepository.DeleteAsync(id);
		}

		public async Task<IEnumerable<Appointment>> GetAppointmentsByUserIdAsync(string userId)
		{
			return await _appointmentRepository.FindAsync(a => a.UserId == userId);
		}
	}
}
