namespace BarberShop.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string? UserId { get; set; } // Kullanıcı ID'si
        public string? UserName { get; set; } // Kullanıcı Adı
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Yorum tarihi
    }
}
