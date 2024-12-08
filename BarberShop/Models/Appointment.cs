using web_pr_project.Models;

namespace BarberShop.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string CustomerName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } = "Pending";

        public Service Service { get; set; }
    }
}
