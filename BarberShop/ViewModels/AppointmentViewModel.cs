using System.ComponentModel.DataAnnotations;

namespace BarberShop.ViewModels
{
	public class AppointmentViewModel
	{
		[Required]
		public int ServiceId { get; set; }

		[Required]
		public int EmployeeId { get; set; }

		[Required]
		[DataType(DataType.DateTime)]
		[Display(Name = "Randevu Tarihi")]
		public DateTime AppointmentDate { get; set; }

		public string Status { get; set; }

		public string UserFullName { get; set; }
		public string EmployeeName { get; set; }
		public string ServiceName { get; set; }
		public decimal ServicePrice { get; set; }
	}
}
