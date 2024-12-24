using BarberShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberShop.Services
{
    public interface IServiceService
    {
        Task<List<Service>> GetAllServicesAsync();
        Task<Service> GetServiceByIdAsync(int id);
        Task<bool> AddServiceAsync(Service service);
        Task<bool> UpdateServiceAsync(Service service);
        Task<bool> DeleteServiceAsync(int id);
    }
}
