using Microsoft.AspNetCore.Identity;

namespace BarberShop.Models
{
    public class User : IdentityUser<Guid>
    {
        // Ek özellikler buraya eklenebilir (örnek: AdSoyad, ProfilFotoğrafı vs.)
        public string? FullName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }
}
