using System.ComponentModel.DataAnnotations;

namespace BarberShop.Models
{
	public class Appointment
	{
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public int EmployeeId { get; set; }
		public int ServiceId { get; set; }
        public Employee Employee { get; set; }
        public Service Service { get; set; }
        public DateTime AppointmentDate { get; set; }
		public string Status { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
