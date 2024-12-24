using BarberShop.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BarberShop.Services
{
    public interface ILoginService
    {
        Task<SignInResult> LoginUserAsync(string email, string password);
        Task LogoutUserAsync();
    }

    public class LoginService : ILoginService
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<SignInResult> LoginUserAsync(string email, string password)
        {
            // Kullanıcıyı email üzerinden bul
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Kullanıcı bulunamadıysa geçersiz giriş
                return SignInResult.Failed;
            }

            // Kullanıcı adı ve şifre ile giriş yap
            return await _signInManager.PasswordSignInAsync(user.UserName, password, isPersistent: false, lockoutOnFailure: false);
        }

        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
