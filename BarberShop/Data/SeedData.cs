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
						new Service { Name = "Geleneksel Kesim", Description = "Alýþýlagelmiþ týraþ yöntemleri", DurationInMinutes = 30, Price = 400, ImageUrl = "/images/service-icon-1.png" },
						new Service { Name = "Býyýk Kesimi", Description = "Þýk ve modern býyýk kesimi", DurationInMinutes = 15, Price = 150, ImageUrl = "/images/service-icon-2.png" },
						new Service { Name = "Sakal Kesimi", Description = "Farklý ve geliþmiþ sakal kesim yöntemleri", DurationInMinutes = 20, Price = 150, ImageUrl = "/images/service-icon-3.png" }
					);
				}

				if (!context.Employees.Any())
				{
					context.Employees.AddRange(
						new Employee { Name = "Ahmet Yýlmaz", Specialization = "Berber" },
						new Employee { Name = "Mehmet Kaya", Specialization = "Kuaför" }
					);
				}

				if (!context.Testimonials.Any())
				{
					context.Testimonials.AddRange(
						new Testimonial { UserId = "user1", Text = "Harika bir hizmet aldým, teþekkürler!" },
						new Testimonial { UserId = "user2", Text = "Sakal kesimi çok iyiydi, tavsiye ederim." }
					);
				}

				context.SaveChanges();
			}
		}
	}
}
