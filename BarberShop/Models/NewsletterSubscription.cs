namespace BarberShop.Models
{
	public class NewsletterSubscription
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public DateTime SubscribedAt { get; set; } = DateTime.Now;
	}
}
