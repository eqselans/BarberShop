using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BarberShop.Models;

namespace BarberShop.Data
{
	public class ApplicationDbContext : IdentityDbContext<User>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options) { }

		public DbSet<Service> Services { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Testimonial> Testimonials { get; set; }
		public DbSet<ContactForm> ContactForms { get; set; }
		public DbSet<AIRecommendation> AIRecommendations { get; set; }
		public DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			// Fluent API ile ek konfigürasyonlar
			builder.Entity<Service>().Property(s => s.Price).HasColumnType("decimal(10,2)");
		}
	}
}
