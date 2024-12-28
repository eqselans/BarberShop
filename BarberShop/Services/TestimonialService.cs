using BarberShop.Data;
using BarberShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services
{
    public class TestimonialService : ITestimonialService
    {
        private readonly ApplicationDbContext _context;

        public TestimonialService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Testimonial>> GetAllTestimonialsAsync()
        {
            return await _context.Testimonials.ToListAsync();
        }

        public async Task<Testimonial> GetTestimonialByIdAsync(int id)
        {
            return await _context.Testimonials.FindAsync(id);
        }

        public async Task AddTestimonialAsync(Testimonial testimonial)
        {
            _context.Testimonials.Add(testimonial);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTestimonialAsync(Testimonial testimonial)
        {
            _context.Testimonials.Update(testimonial);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTestimonialAsync(int id)
        {
            var testimonial = await _context.Testimonials.FindAsync(id);
            if (testimonial != null)
            {
                _context.Testimonials.Remove(testimonial);
                await _context.SaveChangesAsync();
            }
        }
    }
}
