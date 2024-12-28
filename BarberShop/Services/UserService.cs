using BarberShop.Models;
using BarberShop.ViewModels;
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
        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> UpdateUserAsync(UserViewModel userViewModel)
        {
            var user = await _userManager.FindByIdAsync(userViewModel.Id);
            if (user == null) return false;

            user.FullName = userViewModel.FullName;
            user.PhoneNumber = userViewModel.PhoneNumber;
            user.Email = userViewModel.Email;
            user.UserName = userViewModel.Email;

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUserAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

    }
}
