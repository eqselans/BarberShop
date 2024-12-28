    using BarberShop.Models;
using BarberShop.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly SignInManager<User> _logInManager; // Remove this line

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı email ile bul
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user == null)
                {
                    // Kullanıcı bulunamazsa hata ekle
                    ModelState.AddModelError("", "Geçersiz email adresi veya şifre.");
                    return View(model);
                }

                // Kullanıcı mail ve şifre doğrulama
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

                if (isAdmin)
                {
                    return RedirectToAction("Index", "Admin");
                }

                if (result.Succeeded)
                {
                    // Başarılı giriş -> Anasayfaya yönlendir
                    return RedirectToAction("Index", "Home");
                }

                // Giriş başarısız -> Hata mesajı ekle
                ModelState.AddModelError("", "Geçersiz email adresi veya şifre.");
            }

            // ModelState hataları varsa veya giriş başarısızsa sayfayı yeniden yükle
            return View(model);
        }


        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Email kontrolü
                var emailExists = await _userManager.FindByEmailAsync(model.Email);
                if (emailExists != null)
                {
                    ModelState.AddModelError(string.Empty, "Bu e-posta adresi zaten kullanılıyor.");
                    return View(model);
                }

                // Telefon numarası kontrolü
                var phoneExists = _userManager.Users.Any(u => u.PhoneNumber == model.PhoneNumber);
                if (phoneExists)
                {
                    ModelState.AddModelError(string.Empty, "Bu telefon numarası zaten kullanılıyor.");
                    return View(model);
                }
                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
