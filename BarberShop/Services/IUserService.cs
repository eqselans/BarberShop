using BarberShop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberShop.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<string> GetUserRoleAsync(User user);
    }
}

