using BarberShop.Models;
using BarberShop.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BarberShop.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<string> GetUserRoleAsync(User user);
        Task<User> GetUserByIdAsync(string id);
        Task<bool> UpdateUserAsync(UserViewModel userViewModel);
        Task<bool> DeleteUserAsync(string id);

    }
}

