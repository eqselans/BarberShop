using BarberShop.Models;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.ViewModels
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Hizmet seçmek zorunludur.")]
        public int ServiceId { get; set; }



        [Required(ErrorMessage = "Çalışan seçmek zorunludur.")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Randevu tarihi zorunludur.")]
        [DataType(DataType.DateTime)]
        public DateTime AppointmentDate { get; set; }

        public string? Status { get; set; }

        public bool IsConfirmed { get; set; }


    }
}
