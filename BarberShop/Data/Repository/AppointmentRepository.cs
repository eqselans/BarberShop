using BarberShop.Models;

namespace BarberShop.Data.Repository
{
    public class AppointmentRepository : GenericRepository<Appointment>
    {
        public AppointmentRepository(ApplicationDbContext context) : base(context) { }

        // Özel randevu sorgulamaları burada yapılabilir
    }
}
