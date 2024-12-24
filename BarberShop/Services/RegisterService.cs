using BarberShop.Models;
using global::BarberShop.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BarberShop.Services
{
    public interface IRegisterService
    {
        Task<IdentityResult> RegisterUserAsync(string fullName, string email, string password, string phoneNumber);
    }

    public class RegisterService : IRegisterService
    {
        private readonly UserManager<User> _userManager;

        public RegisterService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> RegisterUserAsync(string fullName, string email, string password, string phoneNumber)
        {
            var user = new User
            {
                FullName = fullName,
                Email = email,
                UserName = email,
                PhoneNumber = phoneNumber
            };

            // Kullanıcıyı oluştur
            var result = await _userManager.CreateAsync(user, password);

            // Eğer kullanıcı başarıyla oluşturulursa, rol eklenebilir
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User"); // Varsayılan rol
            }

            return result;
        }
    }
}
