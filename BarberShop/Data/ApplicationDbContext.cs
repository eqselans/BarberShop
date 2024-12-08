using Microsoft.EntityFrameworkCore;
using BarberShop.Models; // Modellerinizi içeren namespace'i ekleyin.

namespace BarberShop.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet tanımları (Hangi modellerin veritabanına bağlanacağını belirtin)
        public DbSet<Service> Services { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
    }
}

