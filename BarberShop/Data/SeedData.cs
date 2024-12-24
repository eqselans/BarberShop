using BarberShop.Models;

namespace BarberShop.Data
{
	public static class SeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
			{
				if (!context.Services.Any())
				{
					context.Services.AddRange(
						new Service { Name = "Geleneksel Kesim", Description = "Al���lagelmi� t�ra� y�ntemleri", DurationInMinutes = 30, Price = 400, ImageUrl = "/images/service-icon-1.png" },
						new Service { Name = "B�y�k Kesimi", Description = "��k ve modern b�y�k kesimi", DurationInMinutes = 15, Price = 150, ImageUrl = "/images/service-icon-2.png" },
						new Service { Name = "Sakal Kesimi", Description = "Farkl� ve geli�mi� sakal kesim y�ntemleri", DurationInMinutes = 20, Price = 150, ImageUrl = "/images/service-icon-3.png" }
					);
				}

				if (!context.Employees.Any())
				{
					context.Employees.AddRange(
						new Employee { Name = "Ahmet Y�lmaz", Specialization = "Berber" },
						new Employee { Name = "Mehmet Kaya", Specialization = "Kuaf�r" }
					);
				}

				if (!context.Testimonials.Any())
				{
					context.Testimonials.AddRange(
						new Testimonial { UserId = "user1", Text = "Harika bir hizmet ald�m, te�ekk�rler!" },
						new Testimonial { UserId = "user2", Text = "Sakal kesimi �ok iyiydi, tavsiye ederim." }
					);
				}

				context.SaveChanges();
			}
		}
	}
}
