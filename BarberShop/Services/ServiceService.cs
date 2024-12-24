using BarberShop.Data;
using BarberShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberShop.Services
{
    public class ServiceService : IServiceService
    {
        private readonly ApplicationDbContext _context;

        public ServiceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Service>> GetAllServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetServiceByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<bool> AddServiceAsync(Service service)
        {
            _context.Services.Add(service);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateServiceAsync(Service service)
        {
            _context.Services.Update(service);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteServiceAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}
