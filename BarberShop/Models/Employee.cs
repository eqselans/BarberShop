namespace BarberShop.Models
{
    public class Employee
    {
        public int Id { get; set; }

        // Çalışanın adı ve soyadı
        public string Name { get; set; }

        // Uzmanlık alanı (örneğin: Berber, Kuaför)
        public string Specialization { get; set; }

        // Çalışanın uygunluk saatleri (örneğin: "09:00 - 18:00")
        public string Availability { get; set; }

        // Çalışanın profil fotoğrafı (isteğe bağlı)
        public string? ProfileImageUrl { get; set; }

        // Ek açıklamalar (örneğin: tecrübe, beceriler)
        public string? Notes { get; set; }
    }
}
