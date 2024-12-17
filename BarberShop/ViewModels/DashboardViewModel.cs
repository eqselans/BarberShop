namespace BarberShop.ViewModels
{
	public class DashboardViewModel
	{
		public int TotalAppointments { get; set; }
		public int TotalEmployees { get; set; }
		public int TotalServices { get; set; }
		public decimal TotalRevenue { get; set; }

		public IEnumerable<AppointmentViewModel> RecentAppointments { get; set; }
		public IEnumerable<EmployeeViewModel> TopEmployees { get; set; }
	}
}
