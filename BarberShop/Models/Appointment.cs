namespace BarberShop.Models
{
	public class Appointment
	{
		public int Id { get; set; }
		public string UserId { get; set; }
		public int EmployeeId { get; set; }
		public int ServiceId { get; set; }
		public DateTime AppointmentDate { get; set; }
		public string Status { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
