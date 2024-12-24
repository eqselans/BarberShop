using BarberShop.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberShop.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return _userManager.Users.ToList();
        }


        public async Task<string> GetUserRoleAsync(User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? "Kullanıcı Rolü Yok";
        }

        public async Task<string> GetUserPhoneNumber(User user)
        {
            var PhoneNumber = await _userManager.GetPhoneNumberAsync(user);
            return PhoneNumber;
        }
    }
}
