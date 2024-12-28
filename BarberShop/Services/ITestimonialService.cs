using BarberShop.Models;

namespace BarberShop.Services
{
    public interface ITestimonialService
    {
        Task<IEnumerable<Testimonial>> GetAllTestimonialsAsync();
        Task<Testimonial> GetTestimonialByIdAsync(int id);
        Task AddTestimonialAsync(Testimonial testimonial);
        Task UpdateTestimonialAsync(Testimonial testimonial);
        Task DeleteTestimonialAsync(int id);
    }
}
